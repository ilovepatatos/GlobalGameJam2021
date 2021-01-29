using System;
using UnityEngine;

/// <summary>
/// Looks for interactables in front within a certain range.
/// </summary>
public class Interacter : MonoBehaviour
{
    [Header("Interacter")] 
    [Range(0f, 100f)]
    public float InteractDistance = 2;

    [HideInInspector] public int InteractLayer; //TODO Make selection like PhysicLayer

    [Space] public SoundDefinition InteractErrorSound;

    [HideInInspector] public bool HasItemInRange;
    private bool HadItemInRange;

    private Interactable lastFrameInteractableInRange, currentFrameInteractableInRange;

    public Action<Interactable> OnInterableItemInSight, OnInteractableItemOutOfSight;
    public Action OnInteractionFail;


    public void Interact() {
        if (HasItemInRange && currentFrameInteractableInRange) {
            if (currentFrameInteractableInRange.IsInteractable())
                currentFrameInteractableInRange.Interact();
        }
        else {
            //TODO Play fail sound
            OnInteractionFail?.Invoke();
        }
    }

    protected virtual void Awake() {
        InteractLayer = LayerMask.GetMask("Interactable");

        OnInterableItemInSight += obj => { Debug.Log($"Can interact with {obj.name}"); };
        OnInteractableItemOutOfSight += obj => { Debug.Log($"Cannot interact with {obj.name} anymore..."); };
    }

    protected virtual void Update() {
        UpdateInteractableItemsInRange();
        ResolveInteractable();
    }

    private void UpdateInteractableItemsInRange() {
        HadItemInRange = HasItemInRange;
        lastFrameInteractableInRange = currentFrameInteractableInRange;
        HasItemInRange = false;

        Transform t = transform;
        Debug.DrawRay(t.position, t.up, Color.red, InteractDistance);
        RaycastHit2D hit = Physics2D.Raycast(t.position, t.up, InteractDistance, InteractLayer);
        if (hit.transform != null) {
            HasItemInRange = hit.transform.TryGetComponent(out currentFrameInteractableInRange);
        }
    }

    private void ResolveInteractable() {
        if (HadItemInRange && !HasItemInRange)
            OnInteractableItemOutOfSight?.Invoke(lastFrameInteractableInRange);
        else if (!HadItemInRange && HasItemInRange)
            OnInterableItemInSight?.Invoke(currentFrameInteractableInRange);
        else if (lastFrameInteractableInRange && lastFrameInteractableInRange.GetInstanceID() != currentFrameInteractableInRange.GetInstanceID()) {
            OnInteractableItemOutOfSight?.Invoke(lastFrameInteractableInRange);
            OnInterableItemInSight?.Invoke(currentFrameInteractableInRange);
        }
    }
}