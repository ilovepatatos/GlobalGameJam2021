using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "RingCoverConfig", menuName = "Create Ring Covers", order = 0)]
public class RingCoverConfigs : ScriptableObject
{
    public List<Sprite> Covers = new List<Sprite>();

    public Sprite GetRandom()
    {
        if (Covers.Count > 0)
            return Covers[Random.Range(0, Covers.Count)];

        throw new Exception("RingConfigs is empty");
    }
}
