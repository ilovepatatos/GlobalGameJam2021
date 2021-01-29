using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HiddenObject : MonoBehaviour
{
    public bool IsUnderPlayer;

    private void OnTriggerEnter(Collider other) {
        IsUnderPlayer = true;
    }

    private void OnTriggerExit(Collider other) {
        IsUnderPlayer = false;
        
    }
}
