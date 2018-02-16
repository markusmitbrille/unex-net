using Autrage.LEX.NET;
using System.Linq;
using UnityEngine;
using SystemDebug = System.Diagnostics.Debug;

[DefaultExecutionOrder(-32000)]
internal class DebugListenerAdder : MonoBehaviour
{
    private void Awake()
    {
        if (!SystemDebug.Listeners.OfType<DebugListener>().Any())
        {
            SystemDebug.Listeners.Add(new DebugListener());
            Bugger.Log("Added debug listener.");
        }

        Destroy(this);
    }
}
