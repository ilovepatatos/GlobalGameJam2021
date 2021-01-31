using System;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    public bool PlayerInSafeZone;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player _))
        {
            PlayerInSafeZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player _))
        {
            PlayerInSafeZone = false;
        }
    }
}
