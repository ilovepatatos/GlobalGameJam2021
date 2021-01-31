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
        UIManager.Instance.Music.SetLevel(info.musicVolume);
        UIManager.Instance.Effect.SetLevel(info.effectVolume);
        UIManager.Instance.Ambient.SetLevel(info.ambientVolume);
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

    public static void UnlockArmor(Category category) {
        switch (category) {
            case Category.Armor01:
                Instance.info.HasUnlockSilverArmor = true;
                break;
            case Category.Armor02:
                Instance.info.HasUnlockGoldenArmor = true;
                break;
            case Category.Armor03:
                Instance.info.HasUnlockDiamondArmor = true;
                break;
            case Category.Armor04:
                Instance.info.HasUnlockDeluxeArmor = true;
                break;
            case Category.Armor05:
                Instance.info.HasUnlockStarArmor = true;
                break;
            case Category.Armor06:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(category), category, null);
        }
    }

    public static bool IsUnlockFromSave(Category category) {
        switch (category) {
            case Category.Armor01:
                return Instance.info.HasUnlockSilverArmor;
            case Category.Armor02:
                return Instance.info.HasUnlockGoldenArmor;
            case Category.Armor03:
                return Instance.info.HasUnlockDiamondArmor;
            case Category.Armor04:
                return Instance.info.HasUnlockDeluxeArmor;
            case Category.Armor05:
                return Instance.info.HasUnlockStarArmor;
            default:
                return false;
        }
    }

}
