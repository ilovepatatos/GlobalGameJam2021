using UnityEngine;

public static class SaveUtility
{
    public static void Save<T>(T toSave, string key)
    {
        PlayerPrefs.SetString(key, JsonUtility.ToJson(toSave));
        PlayerPrefs.Save();
    }
    public static T Load<T>(string key)
    {
        var msg = PlayerPrefs.GetString(key);
        return (T) JsonUtility.FromJson(msg, typeof(T));
    }
}
