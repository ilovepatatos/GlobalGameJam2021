using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "Sound", menuName = "ScriptableObjects/Sound Definition")]
public class SoundDefinition : ScriptableObject
{
    public AudioClip Clip;
    public float Volume;
}
