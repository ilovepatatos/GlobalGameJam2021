using UnityEngine;
using UnityEngine.Events;

public class Bird : MonoBehaviour
{
    public SoundSettings DamagePlayerSound;
    
    public GameObject birdVisual;
    public Shadow Shadow;
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
        int sign = Random.Range(0, 2) > 0 ? 1 : -1;
        initPosition = targetPosition - Vector3.right * 2 * sign;
        retreatPosition = targetPosition + Vector3.right * 20 * sign;
        var localScale = birdVisual.transform.localScale;
        localScale.x *= sign;
        birdVisual.transform.localScale = localScale;
    }

    private void SetInitialPositions()
    {
        birdVisual.transform.position = initPosition;
        Shadow.transform.position = targetPosition;
    }

    private void InitToTarget()
    {
        var toTargetTransition = LeanTween.value(birdVisual, initPosition, targetPosition, 1f);
        toTargetTransition.setOnUpdate((Vector3 f) => birdVisual.transform.position = f);
        toTargetTransition.setOnComplete(() =>
        {
            OnReachedTarget?.Invoke();
            TargetToRetreat();
          
            if(Shadow.IsPlayerInZone())
            {
                if (!PlayerReference.Bank.TryWithdraw(50))
                    PlayerReference.Bank.TryWithdraw(PlayerReference.Bank.MoneyAmount);
                SoundManager.PlayOneShot(DamagePlayerSound);
            }
            
            Shadow.gameObject.SetActive(false);
        });
    }
    

    private void TargetToRetreat()
    {
        var toTargetTransition = LeanTween.value(birdVisual, targetPosition, retreatPosition, 4f);
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
