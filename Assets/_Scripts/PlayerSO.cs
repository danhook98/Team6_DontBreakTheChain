using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "ChainGame/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    public string Name;
    public byte CurrentRoll;
    public byte CurrentTilesMoved;

    public float NextRollTime { get; set; } = 0f;
    public bool CanRoll { get; set; } = true;
    public bool CanSwap { get; set; } = true;

    private void OnEnable()
    {
        CurrentRoll = 0;
        CurrentTilesMoved = 0;
        NextRollTime = 0f;
        CanRoll = true;
        CanSwap = true;
    }

    public event UnityAction<byte> OnMove;
    public event UnityAction<byte> OnCurrentRollChanged;
    public event UnityAction<byte> OnCurrentTilesMovedChanged;
    
    public void MovePlayer(byte steps) => OnMove?.Invoke(steps);
    public void CurrentRollChanged(byte roll) => OnCurrentRollChanged?.Invoke(roll);

    public void UpdateTilesMoved(byte tiles)
    {
        CurrentTilesMoved += tiles;
        OnCurrentTilesMovedChanged?.Invoke(CurrentTilesMoved);
    } 
}
