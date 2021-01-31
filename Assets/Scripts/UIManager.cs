using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public InteractPopup InteractPopup;

    [Space]
    public SetVolume Master;
    public SetVolume Music;
    public SetVolume Effect;
    public SetVolume Ambient;

    [Space] 
    public GameObject CoinCanvas_InGame;
    public GameObject PauseButton;

    public static void SetInteractPopupActive(bool activate) {
        if(activate)
            Instance.InteractPopup.PopUp();
        else
            Instance.InteractPopup.PopDown();
    }

    public static void SetPauseButtonActive(bool activate) {
        Instance.PauseButton.SetActive(activate);
    }

    public static void SetInGameCoinActive(bool activate) {
        Instance.CoinCanvas_InGame.SetActive(activate);
    }
}
