using UnityEngine;

public class CoinCollect : MonoBehaviour
{ 
    [SerializeField] private PlayerSO player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin") && player.CurrentTilesMoved == (byte) other.transform.position.z)
        {
            player.IncrementScore();
            
            Destroy(other.gameObject);
        }
    }
}
