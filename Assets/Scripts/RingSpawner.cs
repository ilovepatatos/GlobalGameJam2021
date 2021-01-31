using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class RingSpawner : MonoBehaviour
{
    [Header("Configs")]
    public RingConfigs Configs;
    public RingCoverConfigs CoverConfigs;
    
    [FormerlySerializedAs("Renderer")] [Header("Renderers")]
    public SpriteRenderer RendererCover;
    public SpriteRenderer RendererBackground;
    [MinMax(1, 100)]public Vector2 RespawnTimeRange;

    public event Action<LostObject> OnSpawnRing;
    public event Action<RingSpawner> OnSpawnerInteraction;
    
    private GameObject ring;
    private LostObject lostObject;
    
    private bool CanSpawn() => !RendererBackground.isVisible;
    
    private void Start()
    {
        ShuffleCover();
        Spawn();
    }

    private void TrySpawn()
    {
        StartCoroutine(RetrySpawn());
    }

    IEnumerator RetrySpawn()
    {
        while (!CanSpawn())
            yield return new WaitForSeconds(1f);
        
        Spawn();
    }

    private void Spawn()
    {
        RendererCover.enabled = true;
        ring = Instantiate(Configs.GetRandom(), transform);
        lostObject = ring.GetComponent<LostObject>();
        lostObject.OnPlayerInteractionStart += OnPlayerInteractionStart;
        lostObject.OnDestroyEvent += OnLostObjectOnDestroyEvent;
        
        OnSpawnRing?.Invoke(lostObject);
    }

    private void OnLostObjectOnDestroyEvent(Interactable interactable)
    {
        var actionDelay = new ActionDelayed(Random.Range(RespawnTimeRange.x, RespawnTimeRange.y), TrySpawn);
        ActionDelayedManager.AddAction(actionDelay);
        // TrySpawn();
    }

    private void OnPlayerInteractionStart(Interactable interactable)
    {
        lostObject.OnPlayerInteractionStart -= OnPlayerInteractionStart;
        RendererCover.enabled = false;
        OnSpawnerInteraction?.Invoke(this);
    }

    private void ShuffleCover()
    {
        RendererCover.sprite = CoverConfigs.GetRandom();
    }
}
