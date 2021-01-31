using UnityEngine;

public class LeanTweener : MonoBehaviour
{
    public LeanTweenConfig LeanTweenConfig;
    public bool PlayOnAwake;
    private void Awake()
    {
        if(PlayOnAwake)
            Play();
    }

    public void Play()
    {
        if(LeanTweenConfig != null)
            LeanTweenConfig.Create(gameObject);
        else
            Debug.LogWarning("UnAssigned LeanTweener", this);
    }
}


