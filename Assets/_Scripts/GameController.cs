using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [Header("Input Reader")]
    [SerializeField] private InputReader inputReader;

    [Header("Game Type SO")]
    [SerializeField] private GameTypeSO gameType;
    
    [Header("Tile Generator")]
    [SerializeField] private TileGenerator tileGenerator;

    [Header("Player Data References")] 
    [SerializeField] private PlayerSO playerOne;
    [SerializeField] private PlayerSO playerTwo;
    
    [Header("Game variables")] 
    [SerializeField] private byte tilesToWin = 30;
    [SerializeField] private Vector3 playerOneStartPosition;
    [SerializeField] private Vector3 playerTwoStartPosition;
    
    [Header("Chain references")]
    [SerializeField] private HingeJoint chainCentreHinge;

    private bool _isComputerOpponent;

    private bool _gameWon = false;
    private bool _gameLost = false;
    
    public UnityEvent OnWin;
    public UnityEvent OnLose;

    // Subscribe to the input events from the Input Reader.
    private void OnEnable()
    {
        inputReader.LeftPlayerRollEvent += LeftPlayerRoll;
        inputReader.LeftPlayerSwapEvent += LeftPlayerSwap;
        inputReader.RightPlayerRollEvent += RightPlayerRoll;
        inputReader.RightPlayerSwapEvent += RightPlayerSwap;
    }

    // Unsubscribe from the input events when this game object gets disabled. Failure to unsubscribe would cause the 
    // bugs and other issues.
    private void OnDisable()
    {
        inputReader.LeftPlayerRollEvent -= LeftPlayerRoll;
        inputReader.LeftPlayerSwapEvent -= LeftPlayerSwap;
        inputReader.RightPlayerRollEvent -= RightPlayerRoll;
        inputReader.RightPlayerSwapEvent -= RightPlayerSwap;
    }

    private void Start()
    {
        // Set the start positions for the players. This slowly moves them to the point from offscreen.
        playerOne.SetStartPosition(playerOneStartPosition);
        playerTwo.SetStartPosition(playerTwoStartPosition);
        
        // Generate the tiles along each player's path.
        tileGenerator.GenerateTiles(playerOneStartPosition, tilesToWin);
        tileGenerator.GenerateTiles(playerTwoStartPosition, tilesToWin);
        
        // Generate the base. 
        tileGenerator.GenerateBase(Vector3.forward * -1, tilesToWin);

        _isComputerOpponent = gameType.opponentIsAi;
    }

    // Main game loop.
    private void Update()
    {
        if (_gameWon || _gameLost) return;
        
        // The chain breaks! The '7.0f' is a magic number as the chain length cannot dynamically change. 
        if (Vector3.Distance(playerOne.Position, playerTwo.Position) >= 7.0f)
        {
            Destroy(chainCentreHinge);
            GameLost();
            return;
        }
        
        // Player one and two have both passed the win threshold. 
        if (playerOne.CurrentTilesMoved >= tilesToWin && playerTwo.CurrentTilesMoved >= tilesToWin)
        {
            GameWon();
            return;
        }
        
        // Player one and two have both rolled.
        if ((!playerOne.CanRoll && !playerTwo.CanRoll) && (playerOne.CurrentRollState == RollState.Rolled 
                                                           && playerTwo.CurrentRollState == RollState.Rolled))
        {
            playerOne.CanRoll = true;
            playerOne.CurrentRollState = RollState.Idle;
            playerTwo.CanRoll = true;
            playerTwo.CurrentRollState = RollState.Idle;
            
            Debug.Log("Round over");
        }

        // Both players want to swap their dice rolls.
        if (playerOne.WantsToSwap && playerTwo.WantsToSwap)
        {
            SwapDiceValues();
        }
    }

    private void GameWon()
    {
        if (_gameWon) return;
        _gameWon = true;
        OnWin?.Invoke();
    }

    private void GameLost()
    {
        if (_gameLost) return;
        _gameLost = true;
        OnLose?.Invoke();
    }
    
    private void SwapDiceValues()
    {
        byte temp = playerOne.CurrentRoll;
        playerOne.UpdateCurrentRoll(playerTwo.CurrentRoll);
        playerTwo.UpdateCurrentRoll(temp);

        playerOne.WantsToSwap = false;
        playerTwo.WantsToSwap = false;
    }

    private void LeftPlayerRoll()
    {
        PlayerRoll(playerOne);

        if (_isComputerOpponent)
        {
            PlayerRoll(playerTwo);
        }
    }

    private void LeftPlayerSwap()
    {
        PlayerSwap(playerOne);

        if (_isComputerOpponent)
        {
            PlayerSwap(playerTwo);
        }
    }

    private void RightPlayerRoll()
    {
        if (!_isComputerOpponent)
        {
            PlayerRoll(playerTwo);
        }
    }

    private void RightPlayerSwap()
    {
        if (!_isComputerOpponent)
        {
            PlayerSwap(playerTwo);
        }
    }

    private void PlayerRoll(PlayerSO player)
    {
        if (_gameWon || _gameLost) return;
        
        switch (player.CurrentRollState)
        {
            case RollState.Idle:
                // The roll state may be 'rolled', but the player isn't currently allowed to do anything (e.g. waiting
                // for the other player to roll).
                if (!player.CanRoll || player.CurrentTilesMoved > tilesToWin) break;

                byte roll = RollDice();
                player.UpdateCurrentRoll(roll);
                
                player.CurrentRollState = RollState.Rolled;
                
                // TODO: display the roll value and a message stating to press again to confirm.
                
                break;
            case RollState.Rolled:
                if (!player.CanRoll || player.CurrentTilesMoved > tilesToWin) break;
                
                player.MovePlayer(player.CurrentRoll);
                player.UpdateTilesMoved(player.CurrentRoll);
                
                player.CanRoll = false;
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void PlayerSwap(PlayerSO player)
    {
        if (_gameWon || _gameLost) return;
        
        if (!player.WantsToSwap && player.CurrentRollState == RollState.Rolled)
        {
            player.WantsToSwap = true;
        }
    }

    private static byte RollDice()
    {
        byte randomNumber = (byte) Random.Range(1, 6);
        return randomNumber;
    } 
}

