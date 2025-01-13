using System;
using System.Collections;
using UnityEngine;
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
    [SerializeField] private float rollTimeout;

    private WaitForSeconds _rollTimeout;

    private void Start()
    {
        _rollTimeout = new WaitForSeconds(rollTimeout);
    }

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
        if (playerOne.CurrentTilesMoved >= tilesToWin && playerTwo.CurrentTilesMoved >= tilesToWin)
        {
            // Run some kind of game win method.
            return;
        }
        
        // Player one and two have both rolled.
        if (!playerOne.CanRoll && !playerTwo.CanRoll)
        {
            playerOne.CanRoll = true;
            playerTwo.CanRoll = true;
            
            Debug.Log("Round over");
        }
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
        if (!player.CanRoll) return;
        
        byte roll = RollDice();
        
        player.MovePlayer(roll);
        player.CurrentRollChanged(roll);
        player.UpdateTilesMoved(roll);

        player.CanRoll = false;

        //StartCoroutine(TimeoutRoll(player));
    }

    private IEnumerator TimeoutRoll(PlayerSO player)
    {
        player.CanRoll = false;
        yield return _rollTimeout;
        player.CanRoll = true;
    }

    private static byte RollDice()
    {
        byte randomNumber = (byte) Random.Range(1, 6);
        return randomNumber;
    } 
}

