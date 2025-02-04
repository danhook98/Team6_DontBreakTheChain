using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Camerafollow : MonoBehaviour
{
    //[SerializeField]
    //private GameObject cameratarget;
    [SerializeField] private Vector3ValueSO pointone;
    [SerializeField] private Vector3ValueSO pointtwo;

    //[SerializeField] GameObject player1;
    //[SerializeField] GameObject player2;


    //private CinemachineVirtualCamera camera;
    // Start is called before the first frame update
    void Awake()
    {
        //camera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
       // float player1pos = player1.transform.position.z;
       // float player2pos = player2.transform.position.z;


        //float avergepos = (player1pos + player2pos) / 2;

        //gameObject.transform.position = new Vector3(player1.transform.position.x, player1.transform.position.y, avergepos);

        Vector3 target = Vector3.Lerp(pointone.value, pointtwo.value, 0.5f);
        Debug.Log(target);
        //cameratarget.transform.position = target;
       // camera.Follow = cameratarget.transform;

    }
}
