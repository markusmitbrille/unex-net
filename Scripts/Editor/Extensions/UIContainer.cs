using System.Collections.Generic;

public abstract class UIContainer : UIElement
{
    public int ChildCount { get { return children.Count; } }

    private List<UIElement> children = new List<UIElement>();

    public void AddChild(UIElement element)
    {
        children.Add(element);
    }

    public bool RemoveChild(UIElement element)
    {
        return children.Remove(element);
    }

    protected void DrawChildren()
    {
        foreach (UIElement child in children)
        {
            child.Draw();
        }
    }
}