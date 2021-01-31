using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class CustomPlayableDirector : MonoBehaviour
{
    public UnityEvent OnTimelineStop;

    private PlayableDirector timeline;

    private void Awake() {
        timeline = GetComponent<PlayableDirector>();
        timeline.stopped += director => OnTimelineStop?.Invoke();
    }
}
