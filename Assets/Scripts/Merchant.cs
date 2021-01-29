using UnityEngine;

public class Merchant : Interactable
{
    public override bool IsInteractable() {
        return true;
    }

    public override void Interact() {
        Debug.Log("Bonjour mr, que puis-je pour vous?");
    }
}
