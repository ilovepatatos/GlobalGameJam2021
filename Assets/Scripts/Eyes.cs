using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Animator))]
public class Eyes : MonoBehaviour
{
    [Header("Eyes")] 
    public float DelayMin = 1;
    public float DelayMax = 5;
    
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        PrepareNextBlink();
    }

    private void PrepareNextBlink() {
        ActionDelayedManager.AddAction(new ActionDelayed(Random.Range(DelayMin, DelayMax), Blink));
    }

    private void Blink() {
        animator.SetTrigger("Blink");
        PrepareNextBlink();
    }
}
