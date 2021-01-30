using System;

[Serializable]
public class PlayerInfo
{
    public int coins;
    public float masterVolume = 0.5f;
    public float musicVolume = 0.5f;
    public float effectVolume = 0.5f;
    public float ambientVolume = 0.5f;
    
    
    public float GetMixerVolume(AudioMixerType audioMixerType)
    {
        switch (audioMixerType)
        {
            case AudioMixerType.Master:
                return masterVolume;
            case AudioMixerType.Music:
                return musicVolume;
            case AudioMixerType.Effect:
                return effectVolume;
            case AudioMixerType.Ambient:
                return ambientVolume;
            default:
                throw new ArgumentOutOfRangeException(nameof(audioMixerType), audioMixerType, null);
        }
    }
    
    public void SetMixerVolume(AudioMixerType audioMixerType, float value)
    {
        switch (audioMixerType)
        {
            case AudioMixerType.Master:
                masterVolume = value;
                break;
            case AudioMixerType.Music:
                musicVolume = value;
                break;
            case AudioMixerType.Effect:
                effectVolume = value;
                break;
            case AudioMixerType.Ambient:
                ambientVolume = value;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(audioMixerType), audioMixerType, null);
        }
    }
}