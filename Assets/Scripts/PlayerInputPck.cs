using UnityEngine;

public class PlayerInputPck
{
    public bool Interact;
    public Vector2 MovementDirection = Vector2.zero;

    public bool Mouse0;
    public bool Space;

    public bool ESC;

    public void Update() {
        Interact = Input.GetButtonDown("Interact");
        MovementDirection = new Vector2((int) Input.GetAxis("Horizontal"), (int) Input.GetAxis("Vertical"));
        
        Mouse0 = Input.GetMouseButtonDown(0);
        Space = Input.GetButtonDown("Jump");
        ESC = Input.GetKeyDown(KeyCode.Escape);
    }
}