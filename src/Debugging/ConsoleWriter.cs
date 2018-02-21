using System.IO;
using System.Text;
using UnityDebug = UnityEngine.Debug;

internal class ConsoleWriter : TextWriter
{
    public override Encoding Encoding=>Encoding.UTF8;

    public override void Write(string value)
    {
        UnityDebug.Log(value);
    }

    public override void WriteLine()
    {
    }

    public override void WriteLine(string value)
    {
        UnityDebug.Log(value);
    }
}
