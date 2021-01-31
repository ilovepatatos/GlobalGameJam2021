using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "RingConfigs", menuName = "Create Ring Configs", order = 0)]
public class RingConfigs : ScriptableObject
{
    public List<GameObject> Rings = new List<GameObject>();

    public GameObject GetRandom()
    {
        if (Rings.Count > 0)
            return Rings[Random.Range(0, Rings.Count)];

        throw new Exception("RingConfigs is empty");
    }
}
