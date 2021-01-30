using System.Collections.Generic;

public static class Global
{
    
    
    public static Dictionary<AudioMixerType, string> MixerProperty = new Dictionary<AudioMixerType, string>
    {
        {AudioMixerType.Master, "masterVolume"},
        {AudioMixerType.Music, "musicVolume"},
        {AudioMixerType.Effect, "effectVolume"},
        {AudioMixerType.Ambient, "ambientVolume"}
    };
}

public enum AudioMixerType
{
    Master, Music, Effect, Ambient
}