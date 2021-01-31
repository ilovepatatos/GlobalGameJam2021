using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
#region Singleton

    public static UIManager Instance = null;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            //GameObject.DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

#endregion

    [Header("UI")] 
    public GameObject InteractPopup;

    public static void SetInteractPopupActive(bool activate) {
        Instance.InteractPopup.SetActive(activate);
    }
}
