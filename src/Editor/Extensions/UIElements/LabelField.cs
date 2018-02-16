using UnityEditor;
using UnityEngine;

public class LabelField : Field<GUIContent>
{
    public LabelField()
    {
        Style = new GUIStyle(EditorStyles.label);
        Value = new GUIContent(GUIContent.none);
    }

    protected override void DrawContent()
    {
        if (content == null)
        {
            EditorGUILayout.LabelField(Value, Style, Options.ToArray());
        }
        else
        {
            EditorGUILayout.LabelField(content, Value, Style, Options.ToArray());
        }
    }
}