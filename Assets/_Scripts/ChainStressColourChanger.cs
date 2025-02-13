using System.Collections;
using UnityEngine;

/// <summary>
/// Monitors the distance between the chain anchor points. If it's close to the maximum length of the chain, events are
/// triggered to change the chain link materials. 
/// </summary>
public class ChainStressColourChanger : MonoBehaviour
{
    [SerializeField] private ChainEventChannel chainEventChannel;
    
    [Header("Chain Anchor Point Vector Data References")]
    [SerializeField] private Vector3ValueSO anchorPointA;
    [SerializeField] private Vector3ValueSO anchorPointB;

    [Header("Stress Distance Variables")]
    [SerializeField] private float maxDistanceToCheck = 7f;
    [SerializeField][Range(0.5f, 1f)] private float percentageToStartChanging = 0.7f;

    private float _dangerZoneDistance;
    private float _distance;
    private WaitForSeconds _chainCheckInterval;

    private bool _alreadyStressed = false;

    private void Start()
    {
        _dangerZoneDistance = maxDistanceToCheck * percentageToStartChanging;
        _chainCheckInterval = new WaitForSeconds(0.5f);
        
        StartCoroutine(RunChainStressCheck());
    }

    // Checks to see if the chain's anchor points are far enough apart to qualify as 'stressed'. The material change
    // of the chain links is then triggered. 
    private IEnumerator RunChainStressCheck()
    {
        while (true)
        {
            _distance = Vector3.Distance(anchorPointA.value, anchorPointB.value);

            // Chain is close to breaking. 
            if (_distance > _dangerZoneDistance && !_alreadyStressed)
            {
                _alreadyStressed = true;
                chainEventChannel.SetChainInDangerZone(_alreadyStressed);
            }
            // Chain has relaxed.
            else if (_distance < _dangerZoneDistance && _alreadyStressed)
            {
                _alreadyStressed = false;
                chainEventChannel.SetChainInDangerZone(_alreadyStressed);
            }
            
            yield return _chainCheckInterval;
        }
    }
}
