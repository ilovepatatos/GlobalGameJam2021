using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public bool IsPlayerInside;

    public Collider2D Collider2D;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("VAR");
        if(other.CompareTag("Player"))
            IsPlayerInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("VAR");
        if(other.CompareTag("Player"))
            IsPlayerInside = false;
    }
    
    
    public bool IsPlayerInZone() {
        List<Collider2D> hits = new List<Collider2D>();
        
        ContactFilter2D filter = new ContactFilter2D();
        filter.layerMask = LayerMask.GetMask("Player");
        filter.useTriggers = true;

        Collider2D.OverlapCollider(filter, hits);

        return hits.Count > 0;
    }
}
