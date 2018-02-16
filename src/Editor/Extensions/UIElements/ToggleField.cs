using UnityEditor;
using UnityEngine;

public class ToggleField : Field<bool>
{
    public ToggleField()
    {
        Style = new GUIStyle(EditorStyles.toggle);
        Value = false;
    }

    protected override void DrawContent()
    {
        if (content == null)
        {
            Value = EditorGUILayout.Toggle(Value, Style, Options.ToArray());
        }
        else
        {
            Value = EditorGUILayout.Toggle(content, Value, Style, Options.ToArray());
        }
    }
}