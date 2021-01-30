using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioMixerType exposedValue;

    private string mixerProperty;
    private Slider slider;
    private void Start()
    {
        slider = GetComponent<Slider>();
        mixerProperty = Global.MixerProperty[exposedValue];
        var value = PlayerInfoManager.Instance.GetMixerVolume(exposedValue);
        slider.value = value;
        SetLevel(value);
    }

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat(mixerProperty, Mathf.Log10(sliderValue) * 20);
        PlayerInfoManager.Instance.SetMixerVolume(exposedValue, sliderValue);
    }
}
