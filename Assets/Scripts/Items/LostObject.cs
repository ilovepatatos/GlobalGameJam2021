using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LostObject : Interactable
{
    [Header("Lost Object")] 
    public string ShortName;
    [TextArea] public string Description;

    public float CarryDistance;

    [Space] 
    public SoundSettings OnPickupSound;
    public SoundSettings OnDropSound;

    private Rigidbody2D rb;

    private void Awake() {
        name = ShortName;
        rb = GetComponent<Rigidbody2D>();
    }

    public override void OnInteractionStart(Interacter interacter) {
        base.OnInteractionStart(interacter);
        
        if (!(interacter is Player)) return;
        
        //Set Player Joint
        Player player = interacter as Player;
        player.Joint.connectedBody = rb;
        
        SetPositionFromInteracter(interacter);
        
        SoundManager.PlayOneShot(OnPickupSound);
    }

    public override void OnInteractionStop() {
        base.OnInteractionStop();
        rb.velocity = Vector2.zero;
        SoundManager.PlayOneShot(OnDropSound);
    }

    private void SetPositionFromInteracter(Interacter interacter) {
        Transform t = interacter.transform;
        transform.position = t.position + t.up * CarryDistance;
    }
}
