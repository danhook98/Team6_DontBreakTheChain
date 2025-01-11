using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [Header("Input Reader")]
    [SerializeField] private InputReader inputReader;

    [Header("Game variables")] 
    [SerializeField] private int tilesToWin = 30;

    private byte _tilesMovedLeftPlayer = 0; 
    private byte _tilesMovedRightPlayer = 0;

    // The int parameter passed is the number of 'spaces' to move. Byte is used as the data type as realistically, the
    // players will never move beyond 256 tiles in one go.
    public UnityEvent<byte> LeftPlayerMove;
    public UnityEvent<byte> RightPlayerMove;

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
        
    }

    private void LeftPlayerSwap()
    {
        
    }

    private void RightPlayerRoll()
    {
        
    }

    private void RightPlayerSwap()
    {
        
    }
}
