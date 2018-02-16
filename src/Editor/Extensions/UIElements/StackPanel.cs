using UnityEngine;

public enum Orientation
{
    Default,
    Vertical,
    Horizontal,
}

public class StackPanel : UIContainer
{
    public Orientation Orientation { get; set; } = Orientation.Vertical;

    protected override void DrawContent()
    {
        Orientation localOrientation = Orientation;

        if (localOrientation == Orientation.Horizontal)
        {
            if (content == null)
            {
                GUILayout.BeginHorizontal(Style, Options.ToArray());
            }
            else
            {
                GUILayout.BeginHorizontal(content, Style, Options.ToArray());
            }
        }
        else
        {
            if (content == null)
            {
                GUILayout.BeginVertical(Style, Options.ToArray());
            }
            else
            {
                GUILayout.BeginVertical(content, Style, Options.ToArray());
            }
        }

        DrawChildren();

        if (localOrientation == Orientation.Horizontal)
        {
            GUILayout.EndHorizontal();
        }
        else
        {
            GUILayout.EndVertical();
        }
    }
}