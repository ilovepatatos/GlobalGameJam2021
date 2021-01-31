using UnityEngine;
using Random = UnityEngine.Random;

public class BirdManager : MonoBehaviour
{
    public Player PlayerReference;
    public SafeZone SafeZone;
    public Bird birdPrefab;
    
    [MinMax(0, 100)]
    public Vector2 timeRangeOfAttack;
    
    private float nextAttackTime;
    private void Awake()
    {
        SetNextAttackTime();
    }

    private void SetNextAttackTime()
    {
        nextAttackTime = Time.time + Random.Range(timeRangeOfAttack.x, timeRangeOfAttack.y);
    }

    private void Update()
    {
        if (!(nextAttackTime <= Time.time)) return;
        if (!SafeZone.PlayerInSafeZone)
            CreateBird();
        
        SetNextAttackTime();
    }

    private void CreateBird()
    {
        var bird = Instantiate(birdPrefab);
        bird.PlayerReference = PlayerReference;
    }
}
