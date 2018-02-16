using UnityEditor;
using UnityEngine;

public class Label : UIElement
{
    public Label()
    {
        Style = new GUIStyle(EditorStyles.label);
    }

    protected override void DrawContent()
    {
        if (content != null)
        {
            GUILayout.Label(content, Style, Options.ToArray());
        }
    }
}