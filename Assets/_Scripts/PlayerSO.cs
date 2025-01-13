using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "ChainGame/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    public string Name;
    public byte CurrentRoll;
    public byte CurrentTilesMoved;
    public bool CanSwap;

    public event UnityAction<byte> OnMove;
    public event UnityAction<byte> OnCurrentRollChanged;
    public event UnityAction<byte> OnCurrentTilesMovedChanged;
    
    public void MovePlayer(byte steps) => OnMove?.Invoke(steps);
    public void CurrentRollChanged(byte roll) => OnCurrentRollChanged?.Invoke(roll);
    public void CurrentTilesMovedChanged(byte tiles) => OnCurrentTilesMovedChanged?.Invoke(tiles);
}
