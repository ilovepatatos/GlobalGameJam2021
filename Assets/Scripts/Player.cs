using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : Interacter
{
    public PlayerInputPck Input = new PlayerInputPck();
    
    protected override void Update() {
        base.Update();
        Input.Update();

        if(Input.Interact)
            Interact();
    }
}
