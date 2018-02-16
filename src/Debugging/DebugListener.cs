using Autrage.LEX.NET;
using System.Diagnostics;
using UnityDebug = UnityEngine.Debug;

internal class DebugListener : TraceListener
{
    public override void Write(string message) => UnityDebug.Log(message);

    public override void Write(object o) => UnityDebug.Log(o);

    public override void Write(object o, string category)
    {
        switch (category)
        {
            case Bugger.LogCategory:
                UnityDebug.Log(o);
                break;

            case Bugger.WarningCategory:
                UnityDebug.LogWarning(o);
                break;

            case Bugger.ErrorCategory:
                UnityDebug.LogError(o);
                break;

            default:
                UnityDebug.Log(o);
                break;
        }
    }

    public override void Write(string message, string category)
    {
        switch (category)
        {
            case Bugger.LogCategory:
                UnityDebug.Log(message);
                break;

            case Bugger.WarningCategory:
                UnityDebug.LogWarning(message);
                break;

            case Bugger.ErrorCategory:
                UnityDebug.LogError(message);
                break;

            default:
                UnityDebug.Log(message);
                break;
        }
    }

    public override void WriteLine(string message) => UnityDebug.Log(message);

    public override void WriteLine(object o) => UnityDebug.Log(o);

    public override void WriteLine(object o, string category)
    {
        switch (category)
        {
            case Bugger.LogCategory:
                UnityDebug.Log(o);
                break;

            case Bugger.WarningCategory:
                UnityDebug.LogWarning(o);
                break;

            case Bugger.ErrorCategory:
                UnityDebug.LogError(o);
                break;

            default:
                UnityDebug.Log(o);
                break;
        }
    }

    public override void WriteLine(string message, string category)
    {
        switch (category)
        {
            case Bugger.LogCategory:
                UnityDebug.Log(message);
                break;

            case Bugger.WarningCategory:
                UnityDebug.LogWarning(message);
                break;

            case Bugger.ErrorCategory:
                UnityDebug.LogError(message);
                break;

            default:
                UnityDebug.Log(message);
                break;
        }
    }
}