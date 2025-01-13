using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "ChainGame/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    public string Name;
    
    public byte CurrentRoll { get; private set; } = 0;
    public RollState CurrentRollState { get; set; } = RollState.Idle;
    public byte CurrentTilesMoved { get; private set; } = 0;
    public bool CanRoll { get; set; } = true;
    public bool CanSwap { get; set; } = true;

    private void OnEnable()
    {
        CurrentRoll = 0;
        CurrentRollState = RollState.Idle;
        CurrentTilesMoved = 0;
        CanRoll = true;
        CanSwap = true;
    }

    public event UnityAction<byte> OnMove;
    public event UnityAction<byte> OnCurrentRollChanged;
    public event UnityAction<byte> OnCurrentTilesMovedChanged;
    
    public void MovePlayer(byte steps) => OnMove?.Invoke(steps);

    public void CurrentRollChanged(byte roll)
    {
        CurrentRoll = roll;
        OnCurrentRollChanged?.Invoke(roll);
    }

    public void UpdateTilesMoved(byte tiles)
    {
        CurrentTilesMoved += tiles;
        OnCurrentTilesMovedChanged?.Invoke(CurrentTilesMoved);
    } 
}

public enum RollState
{
    Idle = 0,
    Rolled = 1,
}