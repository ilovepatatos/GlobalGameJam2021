using UnityEngine;

public class ToggleOnStart : MonoBehaviour
{
    public bool enable;
    private void Start()
    {
        gameObject.SetActive(enable);
    }
}
