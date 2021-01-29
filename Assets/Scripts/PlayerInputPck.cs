using UnityEngine;

public class PlayerInputPck
{
    public bool Interact;
    public Vector2 MovementDirection = Vector2.zero;

    public void Update() {
        Interact = Input.GetButtonDown("Interact");
        MovementDirection = new Vector2((int) Input.GetAxis("Horizontal"), (int) Input.GetAxis("Vertical"));
    }
}