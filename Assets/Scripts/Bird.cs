using UnityEngine;
using UnityEngine.Events;

public class Bird : MonoBehaviour
{
    public GameObject birdVisual;
    public GameObject Shadow;
    [HideInInspector]public Player PlayerReference;

    [Space] 
    public Animator BirdAnimator;
    public Animator BecAnimator;
    public Animator ShadowAnimator;
    
    private Vector3 targetPosition;
    private Vector3 initPosition;
    private Vector3 retreatPosition;

    public UnityEvent OnReachedTarget = new UnityEvent();
    public UnityEvent OnReachedRetreat = new UnityEvent();
    private void Start()
    {
        InitVariables();
        SetInitialPositions();
        InitToTarget();

        OnReachedTarget.AddListener(() => { BirdAnimator.SetTrigger("Attack"); });
        OnReachedTarget.AddListener(() => { BecAnimator.SetTrigger("Attack"); });
        OnReachedTarget.AddListener(() => { ShadowAnimator.SetTrigger("Play_Reverse"); });
    }

    private void InitVariables()
    {
        targetPosition = PlayerReference.gameObject.transform.position;
        
        initPosition = targetPosition - Vector3.right * 10;
        retreatPosition = targetPosition + Vector3.right * 10;
    }

    private void SetInitialPositions()
    {
        birdVisual.transform.position = initPosition;
        Shadow.transform.position = targetPosition;
    }

    private void InitToTarget()
    {
        var toTargetTransition = LeanTween.value(birdVisual, initPosition, targetPosition, 2f);
        toTargetTransition.setOnUpdate((Vector3 f) => birdVisual.transform.position = f);
        toTargetTransition.setOnComplete(() =>
        {
            OnReachedTarget?.Invoke();
            TargetToRetreat();
        });
    }

    private void TargetToRetreat()
    {
        var toTargetTransition = LeanTween.value(birdVisual, targetPosition, retreatPosition, 2f);
        toTargetTransition.setOnUpdate((Vector3 f) =>
        {
            birdVisual.transform.position = f;
        });
        toTargetTransition.setOnComplete(() =>
        {
            OnReachedRetreat?.Invoke();
            Destroy(gameObject);
        });
    }
    
}
