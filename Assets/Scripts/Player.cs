using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    public PlayerInputPck Input = new PlayerInputPck();

    private void Update() {
        Input.Update();
    }
}
