using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BitMaskAttribute : PropertyAttribute
{
    public Type PropType { get; private set; }

    public BitMaskAttribute(Type propType)
    {
        PropType = propType;
    }
}