using Autrage.LEX.NET.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectionGrid<T> : UIElement, ICollectionView<T> where T : class
{
    public List<T> Items { get; set; } = new List<T>();

    public Func<T, GUIContent> SelectItemContent { get; set; } = item => GUIContent.none;

    public GUIStyle ItemStyle { get; set; } = new GUIStyle("button");
    public List<GUILayoutOption> ItemOptions { get; set; } = new List<GUILayoutOption>();

    public T SelectedItem
    {
        get
        {
            return Items?.GetValueOrDefault(SelectedIndex);
        }
        set
        {
            if (Items?.Contains(value) ?? false)
            {
                SelectedIndex = Items.IndexOf(value);
            }
        }
    }

    private int selectedIndex = -1;
    public int SelectedIndex
    {
        get { return selectedIndex; }
        private set
        {
            if (value != selectedIndex)
            {
                // Needs to be done, since dependent fields don't lose control when the selection of a selection grid is changed
                GUI.FocusControl(null);

                int oldIndex = selectedIndex;
                selectedIndex = value;
                SelectionChanged?.Invoke(Items.GetValueOrDefault(oldIndex), Items.GetValueOrDefault(selectedIndex));
            }
        }
    }

    public event Action<T, T> SelectionChanged;

    protected override void DrawContent()
    {
        if (SelectItemContent == null)
        {
            return;
        }
        if (Items == null)
        {
            return;
        }

        if (SelectedIndex < -1 || SelectedIndex >= Items.Count)
        {
            SelectedIndex = -1;
        }

        if (content == null)
        {
            GUILayout.BeginVertical(Style, Options.ToArray());
        }
        else
        {
            GUILayout.BeginVertical(content, Style, Options.ToArray());
        }

        SelectedIndex = GUILayout.SelectionGrid(SelectedIndex, Items.Select(SelectItemContent).Where(itemContent => itemContent != null).ToArray(), 1, ItemStyle, ItemOptions.ToArray());

        GUILayout.EndVertical();
    }

    public void Deselect()
    {
        SelectedIndex = -1;
    }
}