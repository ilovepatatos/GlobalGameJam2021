using System;
using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{
    public static PlayerInfoManager Instance;

    public PlayerInfo Info
    {
        get
        {
            if(!loaded)
                Load();
            return info;
        }
    }

    private PlayerInfo info;
    private bool loaded;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() {
        Load();
        UIManager.Instance.Master.SetLevel(info.masterVolume);
        UIManager.Instance.Master.SetSliderValue(info.masterVolume);
        UIManager.Instance.Music.SetLevel(info.musicVolume);
        UIManager.Instance.Music.SetSliderValue(info.musicVolume);
        UIManager.Instance.Effect.SetLevel(info.effectVolume);
        UIManager.Instance.Effect.SetSliderValue(info.effectVolume);
        UIManager.Instance.Ambient.SetLevel(info.ambientVolume);
        UIManager.Instance.Ambient.SetSliderValue(info.ambientVolume);
    }

    private void OnDestroy() {
        Save();
    }

    public void Load()
    {
        loaded = true;
        info = SaveUtility.Load<PlayerInfo>() ?? new PlayerInfo();
    }

    public void Save()
    {
        SaveUtility.Save(Info);
    }

    public float GetMixerVolume(AudioMixerType audioMixerType)
    {
        return Info.GetMixerVolume(audioMixerType);
    }

    public void SetMixerVolume(AudioMixerType audioMixerType, float value)
    {
        Info.SetMixerVolume(audioMixerType, value);
    }

    public static void SetCoins(int amount) {
        Instance.info.coins = amount;
    }

}
