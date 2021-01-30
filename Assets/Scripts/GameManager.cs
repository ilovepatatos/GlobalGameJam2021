using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake() {
#if !UNITY_EDITOR
        Debug.unityLogger.logEnabled = false;
#endif
    }

    public void StartGame() {
        TimelineRefs.Instance.Transition_Menu_Game.Play();
    }
}
