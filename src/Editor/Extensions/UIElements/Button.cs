using System;
using UnityEditor;
using UnityEngine;

public class Button : UIContainer
{
    public event Action Clicked;

    public Button()
    {
        Style = new GUIStyle("button");
    }

    protected override void DrawContent()
    {
        if (ChildCount == 0 && content != null)
        {
            if (GUILayout.Button(content, Style, Options.ToArray()))
            {
                GUI.FocusControl(null);
                Clicked?.Invoke();
            }
        }
        else if (ChildCount > 0)
        {
            Rect area = EditorGUILayout.BeginVertical(Style);
            if (GUI.Button(area, GUIContent.none))
            {
                GUI.FocusControl(null);
                Clicked?.Invoke();
            }

            DrawChildren();
            EditorGUILayout.EndVertical();
        }
    }
}