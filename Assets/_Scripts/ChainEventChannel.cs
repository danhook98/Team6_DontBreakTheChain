using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Scriptable object used as an event channel for the chain. 
/// </summary>
[CreateAssetMenu(fileName = "ChainEventChannel", menuName = "ChainGame/Chain Event Channel", order = 0)]
public class ChainEventChannel : ScriptableObject
{
    public event UnityAction<bool> OnChainInDangerZone;
    
    public void SetChainInDangerZone(bool inDangerZone) => OnChainInDangerZone?.Invoke(inDangerZone);
}