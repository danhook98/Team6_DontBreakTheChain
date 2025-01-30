using System;
using UnityEngine;

// This file allows for a data file to be created that can store a Vector3 value, which persists between scenes, and 
// doesn't require editor references. A great benefit of this is prefabs can have a reference to a data file of this
// scriptable object, and it will never be lost unless the data file is deleted. I use this file to get the world
// position of the chain anchor points on each player. 

[CreateAssetMenu(fileName = "Vector3 value", menuName = "ChainGame/Data Values/Vector3", order = 0)]
public class Vector3ValueSO : ScriptableObject
{
    public Vector3 value;

    private void OnEnable()
    {
        value = Vector3.zero;
    }
}