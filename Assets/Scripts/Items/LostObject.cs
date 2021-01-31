using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LostObject : Interactable
{
    [Header("Lost Object")] 
    public string ShortName;
    public int Price = 100;
    public float Weight = 10;
    [TextArea] public string Description;

    public float CarryDistance;

    [Space] 
    public SoundSettings OnPickupSound;
    public SoundSettings OnDropSound;

    private Rigidbody2D rb;
    private Player player;

    private void Awake() {
        name = ShortName;
        rb = GetComponent<Rigidbody2D>();
    }

    public override void OnInteractionStart(Interacter interacter) {
        base.OnInteractionStart(interacter);
        
        if (!(interacter is Player p)) return;
        
        //Set Player Joint
        player = p;
        player.Joint.connectedBody = rb;
        
        SetPositionFromInteracter(interacter);
        
        SoundManager.PlayOneShot(OnPickupSound);
    }

    public override void OnInteractionStop() {
        base.OnInteractionStop();
        rb.velocity = Vector2.zero;
        SoundManager.PlayOneShot(OnDropSound);
    }

    public void Sell() {
        //TODO destroy animation
    }

    private void SetPositionFromInteracter(Interacter interacter) {
        Transform t = interacter.transform;
        transform.position = t.position + t.up * CarryDistance;
    }
}
