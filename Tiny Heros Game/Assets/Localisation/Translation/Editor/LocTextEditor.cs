using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LocText))]
public class LocTextEditor : Editor
{

    private static string defaultfile = "english";

    SerializedProperty key;
    SerializedProperty index;

    void OnEnable()
    {
        key = serializedObject.FindProperty("key");
        index = serializedObject.FindProperty("index");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        LocText loc = (LocText)target;
        EditorGUILayout.LabelField("Select the appropriate key for this text field.");
        EditorGUI.BeginChangeCheck();
        index.intValue = EditorGUILayout.Popup("KEY", index.intValue, LocalizedDataHolder.GetKeys().ToArray());
        if (EditorGUI.EndChangeCheck())
        {
            loc.SetKey(LocalizedDataHolder.GetKeys().ToArray()[index.intValue]);
            key.stringValue = LocalizedDataHolder.GetKeys().ToArray()[index.intValue];
        }
        serializedObject.ApplyModifiedProperties();
    }

    [MenuItem("Localisation/RefreshKeys")]
    static void OpenWindow()
    {
        string filePath = string.Format("Languages/{0}", defaultfile);
        TextAsset txt = (TextAsset)Resources.Load(filePath, typeof(TextAsset));

        if (txt)
        {
            LocalizedDataHolder.Refresh();
        }
        else
        {
            EditorUtility.DisplayDialog("ERROR:", " Default file (english.json) not found under path 'Resources/Languages'. Please put the file and try again", "Okay");
        }
    }
}
