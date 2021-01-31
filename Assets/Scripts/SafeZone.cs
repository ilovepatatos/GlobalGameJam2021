using UnityEngine;
using UnityEngine.Events;

public class SafeZone : MonoBehaviour
{
    public bool PlayerInSafeZone;

    public UnityEvent OnEnterSafeZone = new UnityEvent();
    public UnityEvent OnExitSafeZone = new UnityEvent();
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInSafeZone = true;
            OnEnterSafeZone?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInSafeZone = false;
            OnExitSafeZone?.Invoke();
        }
    }
}
