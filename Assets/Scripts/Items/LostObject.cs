using System;
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

    public Action<Interactable> OnPlayerInteractionStart;
    public Action<Interactable> OnDestroyEvent;
    
    private Rigidbody2D rb;
    private Player player;

    private void Awake() {
        name = ShortName;
        rb = GetComponent<Rigidbody2D>();
    }

    public override void OnInteractionStart(Interacter interacter) {
        base.OnInteractionStart(interacter);
        
        if (!(interacter is Player p)) return;
        
        OnPlayerInteractionStart?.Invoke(this);
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
        Destroy(gameObject);
    }

    private void SetPositionFromInteracter(Interacter interacter) {
        Transform t = interacter.transform;
        transform.position = t.position + t.up * CarryDistance;
    }

    public override void OnDestroy()
    {
        OnDestroyEvent?.Invoke(this);
    }
}
