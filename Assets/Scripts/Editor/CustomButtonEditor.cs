using System;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[CustomEditor(typeof(CustomButton))]
public class MyButtonEditor : ButtonEditor
{
    private SerializedProperty onEnter;
    private SerializedProperty onDown;
    private SerializedProperty onUp;
    private SerializedProperty onExit;
    private CustomButton targetMyButton;
    
    private void Awake()
    {
        targetMyButton = target as CustomButton;

        onEnter = serializedObject.FindProperty(nameof(targetMyButton.OnEnter));
        onDown = serializedObject.FindProperty(nameof(targetMyButton.OnDown));
        onUp = serializedObject.FindProperty(nameof(targetMyButton.OnUp));
        onExit = serializedObject.FindProperty(nameof(targetMyButton.OnExit));
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.PropertyField(onEnter);
        EditorGUILayout.PropertyField(onDown);
        EditorGUILayout.PropertyField(onUp);
        EditorGUILayout.PropertyField(onExit);
        
        serializedObject.ApplyModifiedProperties();
    }
    
    
}
