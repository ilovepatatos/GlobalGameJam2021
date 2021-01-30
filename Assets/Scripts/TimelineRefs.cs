using UnityEngine;
using UnityEngine.Playables;

public class TimelineRefs : MonoBehaviour
{
#region Singleton

    public static TimelineRefs Instance = null;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            //GameObject.DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

#endregion

    [Header("Timelines")]
    public PlayableDirector Transition_Menu_Game;
}
