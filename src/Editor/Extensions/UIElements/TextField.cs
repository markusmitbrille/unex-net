using UnityEditor;
using UnityEngine;

public class TextField : Field<string>
{
    public TextField()
    {
        Style = new GUIStyle(EditorStyles.textArea);
        Value = "";
    }

    protected override void DrawContent()
    {
        if (content == null)
        {
            Value = EditorGUILayout.TextField(Value, Style, Options.ToArray());
        }
        else
        {
            Value = EditorGUILayout.TextField(content, Value, Style, Options.ToArray());
        }
    }
}