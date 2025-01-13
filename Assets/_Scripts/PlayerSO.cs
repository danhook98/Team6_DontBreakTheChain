using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "ChainGame/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    public string Name;
    public byte CurrentRoll;
    public byte CurrentTilesMoved;

    private bool _canRoll = true;
    private bool _canSwap = true;

    public bool CanRoll
    {
        get { return _canRoll; }
        set { _canRoll = value; }
    }

    public bool CanSwap
    {
        get { return _canSwap; }
        set { _canSwap = value; }
    }

    private void OnEnable()
    {
        CurrentRoll = 0;
        CurrentTilesMoved = 0;
        _canRoll = true;
        _canSwap = true;
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
