using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Animator))]
public class Eyes : MonoBehaviour
{
    [Header("Eyes")] 
    [MinMax(1, 10)]
    public Vector2 Delay = new Vector2(0, 10);
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
