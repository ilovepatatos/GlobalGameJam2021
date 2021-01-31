using UnityEngine;

public class RingSpawner : MonoBehaviour
{
    public RingConfigs Configs;
    public RingCoverConfigs CoverConfigs;
    public SpriteRenderer Renderer;
    private GameObject ring;
    private LostObject lostObject;
    private void Start()
    {
        ShuffleCover();
        Spawn();
    }

    public void Spawn()
    {
        if (ring != null) return;
        
        Renderer.enabled = true;
        ring = Instantiate(Configs.GetRandom(), transform);
        lostObject = ring.GetComponent<LostObject>();
        lostObject.OnPlayerInteractionStart += OnPlayerInteractionStart;
    }

    private void OnPlayerInteractionStart(Interactable interactable)
    {
        lostObject.OnPlayerInteractionStart -= OnPlayerInteractionStart;
        Renderer.enabled = false;
    }

    public void ShuffleCover()
    {
        Renderer.sprite = CoverConfigs.GetRandom();
    }
}
