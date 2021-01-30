using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public string exposedValue;
    
    
    public void SetLevel(float sliderValue)
    {
        if(!string.IsNullOrWhiteSpace(exposedValue))
            mixer.SetFloat("masterVolume", Mathf.Log10(sliderValue) * 20);
    }
}
