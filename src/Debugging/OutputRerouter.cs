using Autrage.LEX.NET;
using Autrage.LEX.NET.Extensions;
using System;
using System.Linq;
using UnityEngine;
using SystemDebug = System.Diagnostics.Debug;

[DefaultExecutionOrder(-32000)]
internal class OutputRerouter : MonoBehaviour
{
    private void Awake()
    {
        if (SystemDebug.Listeners.OfType<DebugListener>().None())
        {
            SystemDebug.Listeners.Add(new DebugListener());
            Bugger.Log("Added debug listener.");
        }

        Console.SetOut(new ConsoleWriter());
        Console.Write("Set Console Out.");

        Destroy(this);
    }
}