using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LostObject : Interactable
{
    [Header("Lost Object")] 
    public string ShortName;
    [TextArea] public string Description;

    [Space] 
    public SoundSettings OnPickupSound;
    public SoundSettings OnDropSound;

    [HideInInspector] public Rigidbody2D Rigidbody;

    private void Awake() {
        name = ShortName;
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    public override void OnInteractionStart(Interacter interacter) {
        base.OnInteractionStart(interacter);
        if (!(interacter is Player)) return;
        
        Player player = interacter as Player;
        player.Joint.connectedBody = Rigidbody;
        SoundManager.PlayOneShot(OnPickupSound);
    }

    public override void OnInteractionStop() {
        base.OnInteractionStop();
        SoundManager.PlayOneShot(OnDropSound);
    }
}
