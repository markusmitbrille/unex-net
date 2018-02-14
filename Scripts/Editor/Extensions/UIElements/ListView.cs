using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ListView<T> : UIElement, ICollectionView<T>
{
    private List<T> items = new List<T>();
    public List<T> Items
    {
        get
        {
            return items;
        }
        set
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                items = value;
            }
        }
    }

    public Func<T, GUIContent> SelectItemContent { get; set; } = item => GUIContent.none;

    public GUIStyle ItemStyle { get; set; } = new GUIStyle(EditorStyles.label);
    public List<GUILayoutOption> ItemOptions { get; set; } = new List<GUILayoutOption>();

    public UIElement ItemTemplate { get; set; }

    protected override void DrawContent()
    {
        if (content == null)
        {
            GUILayout.BeginVertical(Style, Options.ToArray());
        }
        else
        {
            GUILayout.BeginVertical(content, Style, Options.ToArray());
        }

        foreach (T item in items)
        {
            GUIContent itemContent = SelectItemContent?.Invoke(item);
            if (itemContent != null)
            {
                GUILayout.Label(itemContent, ItemStyle, ItemOptions.ToArray());
            }
        }

        GUILayout.EndVertical();
    }
}