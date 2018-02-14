using Autrage.LEX.NET;
using System.Linq;
using UnityEngine;
using SystemDebug = System.Diagnostics.Debug;

internal class DebugListenerAdder : MonoBehaviour
{
    private void Start()
    {
        if (!SystemDebug.Listeners.OfType<DebugListener>().Any())
        {
            SystemDebug.Listeners.Add(new DebugListener());
            DebugUtils.Log("Added debug listener.");
        }

        Destroy(this);
    }
}
