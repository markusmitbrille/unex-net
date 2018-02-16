using UnityEngine;
using UnityEditor;

public class FloatField : Field<float>
{
    public float Min { get; set; } = float.MinValue;
    public float Max { get; set; } = float.MaxValue;

    public FloatField()
    {
        Style = new GUIStyle(EditorStyles.textArea);
    }

    protected override void DrawContent()
    {
        if (content == null)
        {
            Value = EditorGUILayout.FloatField(Value, Style, Options.ToArray());
        }
        else
        {
            Value = EditorGUILayout.FloatField(content.text, Value, Style, Options.ToArray());
        }

        Value = Mathf.Clamp(Value, Min, Max);
    }
}