using System;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectionView<T>
{
    List<T> Items { get; set; }
    Func<T, GUIContent> SelectItemContent { get; set; }
    GUIStyle ItemStyle { get; set; }
    List<GUILayoutOption> ItemOptions { get; set; }
}