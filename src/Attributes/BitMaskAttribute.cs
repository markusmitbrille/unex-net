using System;
using UnityEngine;

public class BitMaskAttribute : PropertyAttribute
{
    public Type PropType { get; private set; }

    public BitMaskAttribute(Type propType)
    {
        PropType = propType;
    }
}