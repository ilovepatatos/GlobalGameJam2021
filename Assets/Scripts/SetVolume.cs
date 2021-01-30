using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioMixerType exposedValue;

    private string mixerProperty;
    private Slider slider;

    [Header("Sound")] public float Step = 1;
    public SoundSettings OnStepChangeSound;
    private float currentStep;

    private void Start() {
        slider = GetComponent<Slider>();
        mixerProperty = Global.MixerProperty[exposedValue];
        var value = PlayerInfoManager.Instance.GetMixerVolume(exposedValue);
        slider.value = value;
        SetLevel(value);
    }

    public void SetLevel(float sliderValue) {
        mixer.SetFloat(mixerProperty, Mathf.Log10(sliderValue) * 20);
        PlayerInfoManager.Instance.SetMixerVolume(exposedValue, sliderValue);
    }

    public void OnValueChanged(float value) {
        if (Math.Abs(currentStep - value) < Step)
            return;

        currentStep = value;
        SoundManager.PlayOneShot(OnStepChangeSound);
    }
}