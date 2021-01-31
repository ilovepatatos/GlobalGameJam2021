using System;
using UnityEngine;

/// <summary>
/// Looks for interactables in front within a certain range.
/// </summary>
public class Interacter : MonoBehaviour
{
    [Header("Interacter")] [Range(0f, 5f)]
    public float InteractDistance = 2;

    [HideInInspector] public int InteractLayer; //TODO Make selection like PhysicLayer

    [Space] public SoundSettings InteractErrorSound;

    //
    public bool IsInteracting => CurrentInteractableSelected != null;
    [HideInInspector] public Interactable CurrentInteractableSelected;

    public bool HasItemInRange => currentFrameInteractableInRange != null;
    private bool HadItemInRange => lastFrameInteractableInRange != null;
    private Interactable lastFrameInteractableInRange, currentFrameInteractableInRange;

    public event Action<Interactable> OnInteractableEnterRange, OnInteractableLeaveRange;
    public event Action<Interactable> OnInteractionBegin, OnInteractionEnd;
    public event Action OnInteractionFail;

    public virtual bool CanInteract() {
        return HasItemInRange;
    }

    public bool TryInteract() {
        if (!CanInteract() || !currentFrameInteractableInRange.CanStartInteraction(this)) {
            OnInteractionFail?.Invoke();
            return false;
        }

        CurrentInteractableSelected = currentFrameInteractableInRange;
        CurrentInteractableSelected.OnInteractionStart(this);
        OnInteraction(CurrentInteractableSelected);
        OnInteractionBegin?.Invoke(CurrentInteractableSelected);
        return true;
    }

    public bool TryTerminateInteraction() {
        if (!IsInteracting || !IsInteractableAccessible(CurrentInteractableSelected) ||
            !CurrentInteractableSelected.CanStopInteraction(this)) {
            OnInteractionFail?.Invoke();
            return false;
        }

        CurrentInteractableSelected.OnInteractionStop();
        OnTerminateInteraction(CurrentInteractableSelected);
        OnInteractionEnd?.Invoke(CurrentInteractableSelected);
        CurrentInteractableSelected = null;
        return true;
    }
    
    protected virtual void OnInteraction(Interactable interactable) { }
    protected virtual void OnTerminateInteraction(Interactable interactable) { }

    protected virtual void Awake() {
        InteractLayer = LayerMask.GetMask("Interactable");

        OnInteractionFail += () => { SoundManager.PlayOneShot(InteractErrorSound); };
        //OnInteractableEnterRange += obj => { Debug.Log($"{obj.name} entered interactable range"); };
        //OnInteractableLeaveRange += obj => { Debug.Log($"{obj.name} left interactable range..."); };
    }

    protected virtual void Update() {
        UpdateInteractableItemsInRange();
        ResolveInteractable();
    }

    private void UpdateInteractableItemsInRange() {
        lastFrameInteractableInRange = currentFrameInteractableInRange;
        currentFrameInteractableInRange = null;

        Transform t = transform;
        RaycastHit2D hit = Physics2D.Raycast(t.position, t.up, InteractDistance, InteractLayer);
        if (hit.transform != null) {
            hit.transform.TryGetComponent(out currentFrameInteractableInRange);
        }
    }

    private void ResolveInteractable() {
        if (HadItemInRange && !HasItemInRange)
            OnInteractableLeaveRange?.Invoke(lastFrameInteractableInRange);
        else if (!HadItemInRange && HasItemInRange)
            OnInteractableEnterRange?.Invoke(currentFrameInteractableInRange);
        else if (lastFrameInteractableInRange && lastFrameInteractableInRange.GetInstanceID() !=
            currentFrameInteractableInRange.GetInstanceID()) {
            OnInteractableLeaveRange?.Invoke(lastFrameInteractableInRange);
            OnInteractableEnterRange?.Invoke(currentFrameInteractableInRange);
        }
    }

    private bool IsInteractableAccessible(Interactable interactable) {
        int objectiveId = interactable.transform.GetInstanceID();

        Transform t = transform;
        RaycastHit2D[] hits = Physics2D.RaycastAll(t.position, t.up, InteractDistance, InteractLayer);
        foreach (RaycastHit2D hit in hits)
            if (hit.transform.GetInstanceID() == objectiveId)
                return true;

        return false;
    }

    private void OnDrawGizmosSelected() {
        Transform t = transform;
        Debug.DrawRay(t.position, t.up * InteractDistance, Color.red);
    }
}