using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ChainEventChannel", menuName = "ChainGame/Chain Event Channel", order = 0)]
public class ChainEventChannel : ScriptableObject
{
    public event UnityAction<bool> OnChainInDangerZone;
    
    public void SetChainInDangerZone(bool inDangerZone) => OnChainInDangerZone?.Invoke(inDangerZone);
}