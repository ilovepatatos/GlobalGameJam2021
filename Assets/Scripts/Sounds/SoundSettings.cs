using UnityEngine;

[CreateAssetMenu(fileName = "Sound", menuName = "ScriptableObjects/Sound Settings ")]
public class SoundSettings : ScriptableObject
{
    [Header("Sound Settings")]
    public AudioClip Clip;
    [Range(0f, 1f)] public float Volume = 1;
}
