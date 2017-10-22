using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[CustomEditor (typeof(MultilingualText))]
public class MultilingualTextEditor : UnityEditor.UI.TextEditor
{
    private string currentKeyValue;
    private MultilingualText multiLangText;
    private bool showTextProperties = false;

    protected override void OnEnable()
    {
        base.OnEnable();
        multiLangText = target as MultilingualText;
        currentKeyValue = multiLangText.Key;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("Use this component to implement support for multiple languages. Write a JSON 'key-value' ressource file for each language you want to support.", MessageType.None);
        multiLangText.Key = EditorGUILayout.TextField(new GUIContent("Text key", "Key of the text resource you want to display"), multiLangText.Key);
        showTextProperties = EditorGUILayout.Foldout(showTextProperties, new GUIContent("Style", "Edit Text style"), true);
        if (showTextProperties)
            base.OnInspectorGUI();
        if (multiLangText.Key != currentKeyValue)
        {
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
        currentKeyValue = multiLangText.Key;
    }
}
