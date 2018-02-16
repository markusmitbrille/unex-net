using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIElement
{
    protected GUIContent content { get; private set; } = new GUIContent(GUIContent.none);
    private GUIStyle style = new GUIStyle(GUIStyle.none);
    public GUIStyle Style
    {
        get { return style; }
        set
        {
            if (value != null)
            {
                value = new GUIStyle(value);
            }

            style = value;
        }
    }

    public string Text
    {
        get
        {
            return content.text;
        }
        set
        {
            content.text = value;
        }
    }
    public Texture Image
    {
        get
        {
            return content.image;
        }
        set
        {
            content.image = value;
        }
    }
    public string Tooltip
    {
        get
        {
            return content.tooltip;
        }
        set
        {
            content.tooltip = value;
        }
    }

    public TextAnchor Alignment
    {
        get
        {
            return Style.alignment;
        }
        set
        {
            Style.alignment = value;
        }
    }
    public TextClipping Clipping
    {
        get
        {
            return Style.clipping;
        }
        set
        {
            Style.clipping = value;
        }
    }
    public float FixedHeight
    {
        get
        {
            return Style.fixedHeight;
        }
        set
        {
            Style.fixedHeight = value;
        }
    }
    public float FixedWidth
    {
        get
        {
            return Style.fixedWidth;
        }
        set
        {
            Style.fixedWidth = value;
        }
    }
    public GUIStyleState Focused
    {
        get
        {
            return Style.focused;
        }
        set
        {
            Style.focused = value;
        }
    }
    public Font Font
    {
        get
        {
            return Style.font;
        }
        set
        {
            Style.font = value;
        }
    }
    public int FontSize
    {
        get
        {
            return Style.fontSize;
        }
        set
        {
            Style.fontSize = value;
        }
    }
    public FontStyle FontStyle
    {
        get
        {
            return Style.fontStyle;
        }
        set
        {
            Style.fontStyle = value;
        }
    }
    public GUIStyleState Hover
    {
        get
        {
            return Style.hover;
        }
        set
        {
            Style.hover = value;
        }
    }
    public ImagePosition ImagePosition
    {
        get
        {
            return Style.imagePosition;
        }
        set
        {
            Style.imagePosition = value;
        }
    }
    public float LineHeight
    {
        get
        {
            return Style.lineHeight;
        }
    }
    public RectOffset Margin
    {
        get
        {
            return Style.margin;
        }
        set
        {
            Style.margin = value;
        }
    }
    public int Left
    {
        get
        {
            return Style.margin.left;
        }
        set
        {
            Style.margin.left = value;
        }
    }
    public int Top
    {
        get
        {
            return Style.margin.top;
        }
        set
        {
            Style.margin.top = value;
        }
    }
    public int Right
    {
        get
        {
            return Style.margin.right;
        }
        set
        {
            Style.margin.right = value;
        }
    }
    public int Bottom
    {
        get
        {
            return Style.margin.bottom;
        }
        set
        {
            Style.margin.bottom = value;
        }
    }
    public GUIStyleState Normal
    {
        get
        {
            return Style.normal;
        }
        set
        {
            Style.normal = value;
        }
    }
    public GUIStyleState OnActive
    {
        get
        {
            return Style.onActive;
        }
        set
        {
            Style.onActive = value;
        }
    }
    public GUIStyleState OnFocused
    {
        get
        {
            return Style.onFocused;
        }
        set
        {
            Style.onFocused = value;
        }
    }
    public GUIStyleState OnHover
    {
        get
        {
            return Style.onHover;
        }
        set
        {
            Style.onHover = value;
        }
    }
    public GUIStyleState OnNormal
    {
        get
        {
            return Style.onNormal;
        }
        set
        {
            Style.onNormal = value;
        }
    }
    public RectOffset Overflow
    {
        get
        {
            return Style.overflow;
        }
        set
        {
            Style.overflow = value;
        }
    }
    public RectOffset Padding
    {
        get
        {
            return Style.padding;
        }
        set
        {
            Style.padding = value;
        }
    }
    public bool RichText
    {
        get
        {
            return Style.richText;
        }
        set
        {
            Style.richText = value;
        }
    }
    public bool StretchHeight
    {
        get
        {
            return Style.stretchHeight;
        }
        set
        {
            Style.stretchHeight = value;
        }
    }
    public bool StretchWidth
    {
        get
        {
            return Style.stretchWidth;
        }
        set
        {
            Style.stretchWidth = value;
        }
    }
    public bool WordWrap
    {
        get
        {
            return Style.wordWrap;
        }
        set
        {
            Style.wordWrap = value;
        }
    }

    public List<GUILayoutOption> Options { get; set; } = new List<GUILayoutOption>();

    public event Action PreviewDrew;
    public event Action Drew;

    public void Draw()
    {
        PreviewDrew?.Invoke();
        DrawContent();
        Drew?.Invoke();
    }

    protected abstract void DrawContent();
}