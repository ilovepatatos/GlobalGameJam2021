using UnityEngine;

public class PlayerInputPck
{
    public Vector2 MovementDirection = Vector2.zero;

    public void Update() {
        MovementDirection = new Vector2((int) Input.GetAxis("Horizontal"), (int) Input.GetAxis("Vertical"));
    }
}