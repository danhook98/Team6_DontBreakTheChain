using System;
using UnityEngine;

/// <summary>
/// 'Collects' a coin when collided with.
/// </summary>
public class CoinCollect : MonoBehaviour
{ 
    [SerializeField] private PlayerSO player;
    [SerializeField] private ParticleSystem effect;

    // Using OnTriggerStay instead of OnTriggerEnter fixes the issue where sometimes a coin won't be 'collected'. 
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Coin")) return;
        
        // Check that the coin is indeed on the same tile as the player.
        if (player.CurrentTilesMoved == (byte) other.transform.position.z)
        {
            player.IncrementScore();

            Instantiate(effect, transform);

            Destroy(other.gameObject);
        }
    }
}
