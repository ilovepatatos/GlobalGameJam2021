using System;
using UnityEngine;

/// <summary>
/// Looks for interactables in front within a certain range.
/// </summary>
public class Interacter : MonoBehaviour
{
    [Header("Interacter")] [Range(0f, 100f)]
    public float InteractDistance = 2;

    [HideInInspector]
    public int InteractLayer = LayerMask.GetMask("Interactable"); //TODO Make selection like PhysicLayer

    [Space] public SoundDefinition InteractErrorSound;

    [HideInInspector] public bool HasItemInRange;
    private bool HadItemInRange;

    private Interactable lastFrameInteractableInRange, currentFrameInteractableInRange;

    public Action<Interactable> OnInterableItemInSight, OnInteractableItemOutOfSight;
    public Action OnInteractionFail;


    public void Interact() {
        if (HasItemInRange && currentFrameInteractableInRange != null) {
            if (currentFrameInteractableInRange.IsInteractable())
                currentFrameInteractableInRange.Interact();
        }
        else {
            //TODO Play fail sound
            OnInteractionFail?.Invoke();
        }
    }

    protected virtual void Update() {
        UpdateInteractableItemsInRange();
        ResolveInteractable();
    }

    private void UpdateInteractableItemsInRange() {
        HadItemInRange = HasItemInRange;
        lastFrameInteractableInRange = currentFrameInteractableInRange;

        Transform t = transform;
        RaycastHit2D hit = Physics2D.Raycast(t.position, t.forward, InteractDistance, InteractLayer);
        HasItemInRange = hit.transform.TryGetComponent(out currentFrameInteractableInRange);
    }

    private void ResolveInteractable() {
        if (HadItemInRange && !HasItemInRange)
            OnInteractableItemOutOfSight?.Invoke(lastFrameInteractableInRange);
        else if (!HadItemInRange && HasItemInRange)
            OnInterableItemInSight?.Invoke(currentFrameInteractableInRange);
        else if (lastFrameInteractableInRange.GetInstanceID() != currentFrameInteractableInRange.GetInstanceID()) {
            OnInteractableItemOutOfSight?.Invoke(lastFrameInteractableInRange);
            OnInterableItemInSight?.Invoke(currentFrameInteractableInRange);
        }
    }
}