using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [Header("Input Reader")]
    [SerializeField] private InputReader inputReader;

    [Header("Player Data References")] 
    [SerializeField] private PlayerSO playerOne;
    [SerializeField] private PlayerSO playerTwo;

    [Header("Game variables")] 
    [SerializeField] private int tilesToWin = 30;

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
        byte roll = RollDice();
        player.MovePlayer(roll);
        player.CurrentRollChanged(roll);
        player.UpdateTilesMoved(roll);
    }

    private static byte RollDice()
    {
        byte randomNumber = (byte) Random.Range(1, 6);
        return randomNumber;
    } 
}

