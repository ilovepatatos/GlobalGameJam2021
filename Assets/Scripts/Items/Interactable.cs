using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract bool IsInteractable();
    public abstract void Interact();
}
