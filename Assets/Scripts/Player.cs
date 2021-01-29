using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : Interacter
{
    [Header("Player")]
    public Animator Animator;
    
    public PlayerInputPck Input = new PlayerInputPck();
    
    protected override void Update() {
        base.Update();
        Input.Update();

        if(Input.Interact)
            Interact();
    }
}
