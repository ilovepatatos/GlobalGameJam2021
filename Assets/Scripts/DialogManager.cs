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

    private static bool isCoroutineRunning;
    private static Coroutine currentCoroutine;

    public static void PrepareDialogBox(Dialog dialog, Action action) {
        CurrentDialog = dialog;
        Instance.DialogComponent.CharacterName.text = dialog.CharacterName;
        Instance.DialogComponent.Sentence.text = "";
        Instance.DialogComponent.OnPopUpStop = action;
        Instance.DialogComponent.PopUp();
    }

    public static void CloseDialogBox() {
        Instance.DialogComponent.PopDown();
        TerminateCoroutine();
    }

    public static bool TryCompleteSentence(string sentence) {
        if (!isCoroutineRunning)
            return false;
        TerminateCoroutine();

        Instance.DialogComponent.Sentence.text = sentence;
        return true;
    }

    public static void TypeSentence(string sentence, float charDelay, SoundSettings sound) {
        TerminateCoroutine();
        currentCoroutine = Instance.StartCoroutine(Type(sentence, charDelay, sound));
    }

    public static bool IsDialogPlaying() {
        return CurrentDialog != null && CurrentDialog.IsPlaying;
    }

    private static void TerminateCoroutine() {
        if (isCoroutineRunning)
            Instance.StopCoroutine(currentCoroutine);
        isCoroutineRunning = false;
    }

    private static IEnumerator Type(string sentence, float charDelay, SoundSettings sound) {
        isCoroutineRunning = true;

        WaitForSeconds wait = new WaitForSeconds(charDelay);
        Instance.DialogComponent.Sentence.text = "";

        foreach (char c in sentence) {
            Instance.DialogComponent.Sentence.text += c;
            SoundManager.PlayOneShot(sound);
            yield return wait;
        }

        isCoroutineRunning = false;
    }
}