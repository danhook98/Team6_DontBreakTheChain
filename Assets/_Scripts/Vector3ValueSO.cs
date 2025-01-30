using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Vector3 value", menuName = "ChainGame/Data Values/Vector3", order = 0)]
public class Vector3ValueSO : ScriptableObject
{
    public Vector3 value;

    private void OnEnable()
    {
        value = Vector3.zero;
    }
}