using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "ChainGame/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    public string Name;

    public Vector3 Position { get; set; } = Vector3.zero;
    
    // Dice-centric variables.
    public byte CurrentRoll { get; private set; } = 0;
    public RollState CurrentRollState { get; set; } = RollState.Idle;
    public byte CurrentTilesMoved { get; private set; } = 0;
    public bool CanRoll { get; set; } = true;
    public bool WantsToSwap { get; set; } = false;
    
    // Other
    public bool JustInput { get; set; } = false;

    private void OnEnable()
    {
        Position = Vector3.zero;
        CurrentRoll = 0;
        CurrentRollState = RollState.Idle;
        CurrentTilesMoved = 0;
        CanRoll = true;
        WantsToSwap = false;
    }

    public void ResetState()
    {
        Position = Vector3.zero;
        CurrentRoll = 0;
        CurrentRollState = RollState.Idle;
        CurrentTilesMoved = 0;
        CanRoll = true;
        WantsToSwap = false;
    }

    public event UnityAction<byte> OnMove;
    public event UnityAction<Vector3> OnSetStartPosition;
    public event UnityAction<byte> OnCurrentRollChanged;
    public event UnityAction<byte> OnCurrentTilesMovedChanged;
    public event UnityAction OnScoreChanged;
    
    public void MovePlayer(byte steps) => OnMove?.Invoke(steps);
    public void SetStartPosition(Vector3 startPosition) => OnSetStartPosition?.Invoke(startPosition);

    public void UpdateCurrentRoll(byte roll)
    {
        CurrentRoll = roll;
        OnCurrentRollChanged?.Invoke(roll);
    }

    public void UpdateTilesMoved(byte tiles)
    {
        CurrentTilesMoved += tiles;
        OnCurrentTilesMovedChanged?.Invoke(CurrentTilesMoved);
    } 
    
    public void IncrementScore() => OnScoreChanged?.Invoke();
}