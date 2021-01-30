using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : Interacter
{
    [Header("Player")]
    public Animator Animator;
    public FixedJoint2D Joint;
    
    public PlayerInputPck Input = new PlayerInputPck();

    protected override void Update() {
        base.Update();
        Input.Update();

        if (Input.Interact)
            ResolveInteraction();
    }

    private void ResolveInteraction() {
        if (IsInteracting)
            TryTerminateInteraction();
        else
            TryInteract();
    }
}
