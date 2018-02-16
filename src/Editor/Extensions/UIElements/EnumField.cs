using System;
using UnityEditor;

public class EnumField<E> : Field<E> where E : struct, IConvertible
{
    public EnumField()
    {
        if (!typeof(E).IsEnum)
        {
            throw new ArgumentException("E must be an enum type!");
        }
    }

    protected override void DrawContent()
    {
        if (content == null)
        {
            Value = (E)Enum.Parse(typeof(E), EditorGUILayout.EnumPopup((Enum)Enum.Parse(typeof(E), Value.ToString())).ToString());
        }
        else
        {
            Value = (E)Enum.Parse(typeof(E), EditorGUILayout.EnumPopup(content, (Enum)Enum.Parse(typeof(E), Value.ToString()), Options.ToArray()).ToString());
        }
    }
}