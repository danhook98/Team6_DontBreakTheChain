using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinSpawning : MonoBehaviour
{
    [SerializeField] GameObject Coin;

    Vector3 Startloc;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 11; i++)
        {
            int Side = Random.Range(0, 2);

            int Location = Random.Range(1, 30);

            if (Side == 0)
            {
                Side = -2;
            }
            if (Side == 1)
            {
                Side = 2;
            }

            Startloc = new Vector3(Side, 0, Location);
            Instantiate(Coin, Startloc, Quaternion.identity);
        }
    }
}
