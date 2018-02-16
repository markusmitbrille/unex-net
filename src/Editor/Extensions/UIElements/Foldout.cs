using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;

using static UnityEditor.EditorGUILayout;

public class Foldout : UIContainer
{
    public AnimBool Animation { get; set; } = null;

    private bool show = true;

    public Foldout()
    {
        Style = new GUIStyle(EditorStyles.foldout);
    }

    protected override void DrawContent()
    {
        if (content == null)
        {
            return;
        }

        if (Animation == null)
        {
            show = Foldout(show, content, true, Style);

            if (show)
            {
                EditorGUI.indentLevel++;
                DrawChildren();
                EditorGUI.indentLevel--;
            }
        }
        else
        {
            Animation.target = Foldout(Animation.target, content, true, Style);

            if (BeginFadeGroup(Animation.faded))
            {
                EditorGUI.indentLevel++;
                DrawChildren();
                EditorGUI.indentLevel--;
            }

            EndFadeGroup();
        }
    }
}