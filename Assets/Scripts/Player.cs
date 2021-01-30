using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : Interacter
{
    [Header("Player")] 
    public Transform Body;
    public Animator Animator;
    public FixedJoint2D Joint;
    private PlayerMovement playerMovement;

    public PlayerInputPck Input = new PlayerInputPck();

    public bool IsCarryingObject => CurrentInteractableSelected is LostObject;

    protected override void Awake() {
        base.Awake();
        playerMovement = GetComponent<PlayerMovement>();
    }

    protected override void Update() {
        base.Update();
        Input.Update();

        if (Input.Interact)
            ResolveInteraction();
    }

    protected override void OnInteraction(Interactable interactable) {
        base.OnInteraction(interactable);

        if (CurrentInteractableSelected is LostObject obj) {
            SetPivot(obj.CarryDistance);
            playerMovement.OnPickupObject();
        }
    }

    protected override void OnTerminateInteraction(Interactable interactable) {
        base.OnTerminateInteraction(interactable);
        Joint.connectedBody = playerMovement.Rb;
        
        if (CurrentInteractableSelected is LostObject obj)
            SetPivot(-obj.CarryDistance);
    }

    private void ResolveInteraction() {
        if (IsInteracting)
            TryTerminateInteraction();
        else
            TryInteract();
    }

    private void SetPivot(float distance) {
        Transform t = transform;
        Vector3 dir = t.up * distance;
        t.position += dir;
        Body.position -= dir;
    }
}