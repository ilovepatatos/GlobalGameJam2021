using UnityEngine;

public class SoundManager : MonoBehaviour
{
#region Singleton

    private static SoundManager instance = null;

    private void Awake() {
        instance = this;
    }

#endregion

    [Header("Sound Manager")] public AudioSource Effects;

    public static void PlayOneShot(SoundSettings sound) {
        if (!sound) return;
        instance.Effects.PlayOneShot(sound.Clip, sound.Volume);
    }
}