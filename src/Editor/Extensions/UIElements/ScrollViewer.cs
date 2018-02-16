using UnityEngine;

public class ScrollViewer : UIContainer
{
    public bool AlwaysShowHorizontal { get; set; } = false;
    public bool AlwaysShowVertical { get; set; } = true;

    public GUIStyle HorizontalScrollbarStyle { get; private set; } = new GUIStyle("horizontalScrollbar");
    public GUIStyle VerticalScrollbarStyle { get; private set; } = new GUIStyle("verticalScrollbar");

    private Vector2 scrollPosition = Vector2.zero;

    protected override void DrawContent()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, AlwaysShowHorizontal, AlwaysShowVertical, HorizontalScrollbarStyle, VerticalScrollbarStyle, Style, Options.ToArray());
        DrawChildren();
        GUILayout.EndScrollView();
    }
}