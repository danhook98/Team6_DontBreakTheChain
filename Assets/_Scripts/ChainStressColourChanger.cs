using System.Collections;
using UnityEngine;

public class ChainStressColourChanger : MonoBehaviour
{
    [Header("Chain Anchor Point Vector Data References")]
    [SerializeField] private Vector3ValueSO anchorPointA;
    [SerializeField] private Vector3ValueSO anchorPointB;

    [Header("Stress Distance Variables")]
    [SerializeField] private float maxDistanceToCheck = 7f;
    [SerializeField][Range(0.5f, 1f)] private float percentageToStartChanging = 0.7f;

    private float _dangerZoneDistance;
    private float _distance;
    private WaitForSeconds _chainCheckInterval;

    private void Start()
    {
        _dangerZoneDistance = maxDistanceToCheck * percentageToStartChanging;
        _chainCheckInterval = new WaitForSeconds(0.25f);

        StartCoroutine(RunChainStressCheck());
    }

    // Checks to see if the chain's anchor points are far enough apart to qualify as 'stressed'. The material change
    // of the chain links is then triggered. 
    private IEnumerator RunChainStressCheck()
    {
        while (true)
        {
            _distance = Vector3.Distance(anchorPointA.value, anchorPointB.value);

            if (_distance > _dangerZoneDistance)
            {
                Debug.Log("Danger Zone");
            }
            
            yield return _chainCheckInterval;
        }
    }
}
