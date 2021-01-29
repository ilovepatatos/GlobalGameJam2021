using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "Game", menuName = "Sound Definition")]
public class SoundDefinition : ScriptableObject
{
    public AudioClip Clip;
    public AudioMixerGroup Output;
}
