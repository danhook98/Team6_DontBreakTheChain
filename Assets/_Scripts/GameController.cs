using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [Header("Input Reader")]
    [SerializeField] private InputReader inputReader;

    [Header("Player Data References")] 
    [SerializeField] private PlayerSO playerOne;
    [SerializeField] private PlayerSO playerTwo;
    
    [Header("Game variables")] 
    [SerializeField] private int tilesToWin = 30;
    [SerializeField] private Vector3 playerOneStartPosition;
    [SerializeField] private Vector3 playerTwoStartPosition;
    
    [Header("Chain references")]
    [SerializeField] private HingeJoint chainCentreHinge;

    private bool _gameWon = false;
    
    public UnityEvent OnWin;

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
        playerOne.SetStartPosition(playerOneStartPosition);
        playerTwo.SetStartPosition(playerTwoStartPosition);
    }

    // Main game loop.
    private void Update()
    {
        // The chain breaks! The '7.0f' is a magic number as the chain length cannot dynamically change. 
        if (Vector3.Distance(playerOne.Position, playerTwo.Position) >= 7.0f)
        {
            Destroy(chainCentreHinge);
        }
        
        // Player one and two have both passed the win threshold. 
        if (playerOne.CurrentTilesMoved > tilesToWin && playerTwo.CurrentTilesMoved > tilesToWin)
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
    
    private void SwapDiceValues()
    {
        byte temp = playerOne.CurrentRoll;
        playerOne.UpdateCurrentRoll(playerTwo.CurrentRoll);
        playerTwo.UpdateCurrentRoll(temp);

        playerOne.WantsToSwap = false;
        playerTwo.WantsToSwap = false;
    }

    private void LeftPlayerRoll() => PlayerRoll(playerOne);
    private void LeftPlayerSwap() => PlayerSwap(playerOne);

    private void RightPlayerRoll() => PlayerRoll(playerTwo);
    private void RightPlayerSwap() => PlayerSwap(playerTwo);

    private void PlayerRoll(PlayerSO player)
    {
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
        
        /*if (player.CurrentRollState == RollState.Rolled)
        {
            player.CurrentRollState = RollState.Confirmed;
        }
        
        if (!player.CanRoll || player.CurrentTilesMoved > tilesToWin) return;
        
        byte roll = RollDice();
        
        player.MovePlayer(roll);
        player.CurrentRollChanged(roll);
        player.UpdateTilesMoved(roll);

        player.CanRoll = false;
        player.CurrentRollState = RollState.Rolled;*/
    }

    private void PlayerSwap(PlayerSO player)
    {
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

