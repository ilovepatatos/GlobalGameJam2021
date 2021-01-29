using UnityEngine;

public static class SaveUtility
{
    public static void Save<T>(T toSave, string key = "save")
    {
        PlayerPrefs.SetString(key, JsonUtility.ToJson(toSave));
        PlayerPrefs.Save();
    }
    public static T Load<T>(string key = "save")
    {
        var msg = PlayerPrefs.GetString(key);
        return (T) JsonUtility.FromJson(msg, typeof(T));
    }
}
