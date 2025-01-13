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

    // Main game loop.
    private void Update()
    {
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
    }

    private void GameWon()
    {
        if (_gameWon) return;
        _gameWon = true;
        OnWin?.Invoke();
    }

    private void LeftPlayerRoll()
    {
        PlayerRoll(playerOne);
    }

    private void LeftPlayerSwap()
    {
        
    }

    private void RightPlayerRoll()
    {
        PlayerRoll(playerTwo);
    }

    private void RightPlayerSwap()
    {
        
    }

    private void PlayerRoll(PlayerSO player)
    {
        switch (player.CurrentRollState)
        {
            case RollState.Idle:
                Debug.Log("Player is rolled");
                // The roll state may be 'rolled', but the player isn't currently allowed to do anything (e.g. waiting for
                // the other player to roll).
                if (!player.CanRoll || player.CurrentTilesMoved > tilesToWin) break;

                byte roll = RollDice();
                player.CurrentRollChanged(roll);
                
                player.CurrentRollState = RollState.Rolled;
                
                // TODO: display the roll value and a message stating to press again to confirm.
                
                break;
            case RollState.Rolled:
                Debug.Log("Player is confirmed");
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

    private static byte RollDice()
    {
        byte randomNumber = (byte) Random.Range(1, 6);
        return randomNumber;
    } 
}

