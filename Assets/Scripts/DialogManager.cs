using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Can pop up/down the dialog
/// Can set 
/// </summary>
public class DialogManager : MonoBehaviour
{
#region Singleton

    public static DialogManager Instance = null;

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
    
    public DialogComponent DialogComponent;

    public static Dialog CurrentDialog;
    private static Coroutine currentCoroutine;

    public static void PrepareDialogBox(Dialog dialog, Action action) {
        CurrentDialog = dialog;
        Instance.DialogComponent.Sentence.text = "";
        Instance.DialogComponent.OnPopUpStop = action;
        Instance.DialogComponent.PopUp();
    }

    public static void CloseDialogBox() {
        Instance.DialogComponent.PopDown();
    }

    public static void TypeSentence(string sentence, float charDelay, SoundSettings sound) {
        if(currentCoroutine != null)
            Instance.StopCoroutine(currentCoroutine);
        currentCoroutine = Instance.StartCoroutine(Type(sentence, charDelay, sound));
    }

    public static bool IsDialogPlaying() {
        return CurrentDialog != null && CurrentDialog.IsPlaying;
    }
    
    private static IEnumerator Type(string sentence, float charDelay, SoundSettings sound) {
        WaitForSeconds wait = new WaitForSeconds(charDelay);
        Instance.DialogComponent.Sentence.text = "";
        
        foreach (char c in sentence) {
            Instance.DialogComponent.Sentence.text += c;
            SoundManager.PlayOneShot(sound);
            yield return wait;
        }
    }
}
