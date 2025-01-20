using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollect : MonoBehaviour
{ 
    [SerializeField] private PlayerSO player;
    [SerializeField] private TextMeshProUGUI ScoreText;

    private int Score = 0;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            if (player.CurrentTilesMoved == collision.gameObject.transform.position.z)
            {
                Debug.Log("Score Gained");

                Score = int.Parse(ScoreText.text);

                Score += 1;

                ScoreText.text = $"{Score}";

                Destroy(collision.gameObject);
            }
        }
    }
}
