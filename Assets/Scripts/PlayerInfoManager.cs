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

}
