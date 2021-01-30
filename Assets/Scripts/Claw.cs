using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Claw : MonoBehaviour
{
    [Header("Claw")] 
    [MinMax(0, 10)] public Vector2 Delay = new Vector2(3, 5);
    
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        PrepareNextBlink();
    }

    private void PrepareNextBlink() {
        ActionDelayedManager.AddAction(new ActionDelayed(Random.Range(Delay.x, Delay.y), Blink));
    }

    private void Blink() {
        animator.SetTrigger("Blink");
        PrepareNextBlink();
    }
}
