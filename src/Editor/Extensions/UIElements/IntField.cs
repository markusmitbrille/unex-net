using UnityEditor;
using UnityEngine;

public class IntField : Field<int>
{
    public int Min { get; set; } = int.MinValue;
    public int Max { get; set; } = int.MaxValue;

    public IntField()
    {
        Style = new GUIStyle(EditorStyles.textArea);
    }

    protected override void DrawContent()
    {
        if (content == null)
        {
            Value = EditorGUILayout.IntField(Value, Style, Options.ToArray());
        }
        else
        {
            Value = EditorGUILayout.IntField(content.text, Value, Style, Options.ToArray());
        }

        Value = Mathf.Clamp(Value, Min, Max);
    }
}