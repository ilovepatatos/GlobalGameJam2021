using System;
using UnityEngine;

[Serializable]
public class Dialog
{
    [Header("Dialog")] 
    [HideInInspector] public bool IsPlaying;

    [Range(0, 1f)] public float DelayBetweenChar = 0.2f;
    public SoundSettings TypeCharacterSound;
    
    [Space]
    public string CharacterName;
    [TextArea(1, 3)] public string[] Sentences;
    private int currentSenteceIndex;

    public Action OnDialogStart, OnDialogStop;

    public void Start() {
        DialogManager.PrepareDialogBox(this, StartTypingSentence);
    }

    private void StartTypingSentence() {
        IsPlaying = true;
        currentSenteceIndex = 0;
        OnDialogStart?.Invoke();
        Next();
    }

    public void Stop() {
        IsPlaying = false;
        OnDialogStop?.Invoke();
        DialogManager.CloseDialogBox();
    }

    public void Next() {
        if (currentSenteceIndex >= Sentences.Length) {
            Stop();
            return;
        }
        DialogManager.TypeSentence(Sentences[currentSenteceIndex], DelayBetweenChar, TypeCharacterSound);
        currentSenteceIndex++;
    }

    public void CompleteSentence() {
        if(!DialogManager.TryCompleteSentence(Sentences[currentSenteceIndex - 1]))
            Next();
    }
}