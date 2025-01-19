using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "ChainGame/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    public string Name;
    
    public byte CurrentRoll { get; private set; }
    public RollState CurrentRollState { get; set; }
    public byte CurrentTilesMoved { get; private set; }
    public bool CanRoll { get; set; }
    public bool WantsToSwap { get; set; }

    private void OnEnable()
    {
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
}