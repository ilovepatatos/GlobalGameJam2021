using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake() {
#if !UNITY_EDITOR
        Debug.unityLogger.logEnabled = false;
#endif
    }
}
