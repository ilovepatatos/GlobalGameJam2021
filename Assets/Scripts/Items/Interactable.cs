using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Interactable : MonoBehaviour
{
    [HideInInspector] public Interacter Interacter;
    
    public virtual void OnInteractionStart(Interacter interacter) {
        Interacter = interacter;
    }

    public virtual void OnInteractionStop() {
        Interacter = null;
    }

    public virtual bool CanStartInteraction(Interacter interacter) {
        return !Interacter;
    }

    public virtual bool CanStopInteraction(Interacter interacter) {
        return Interacter.GetInstanceID() == interacter.GetInstanceID();
    }

    public virtual void OnDestroy() { }
}
