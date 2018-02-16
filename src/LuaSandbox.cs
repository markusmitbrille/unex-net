using Autrage.LEX.NET.Serialization;
using NLua;
using System.Diagnostics;
using UnityEngine;

using static Autrage.LEX.NET.Bugger;

public class LuaSandbox : MonoBehaviour
{
    [TextArea]
    [SerializeField]
    [DataMember]
    private string initEnvironmentChunk =
    @"
-- Initialize Lua Sandbox

-- .NET Namespaces
import('System')
import('System.Collections')
import('System.Collections.Generic')
import('System.Linq')

-- Unity Namespaces
import('UnityEngine')
import('UnityEngine.UI')
    ";

    [SerializeField]
    [DataMember]
    private long maximumExecutionTime = 1000;

    [DataMember]
    private long executionTime = 0;

    private Lua environment;

    public object this[string fullPath]
    {
        get
        {
            return environment[fullPath];
        }

        set
        {
            environment[fullPath] = value;
        }
    }

    private void Awake()
    {
        environment = new Lua();
        environment.LoadCLRPackage();
        environment.DoString(initEnvironmentChunk, "initEnvironment");
    }

    private void Update()
    {
        // Reset execution time each tick
        executionTime = 0;
    }

    public object[] DoString(string chunk, string chunkName)
    {
        if (executionTime > maximumExecutionTime)
        {
            Warning($"Skipped chunk '{chunk}', because maximum execution time has been reached!");
            return null;
        }

        Stopwatch stopwatch = Stopwatch.StartNew();
        object[] results = environment.DoString(chunk, chunkName);
        stopwatch.Stop();

        executionTime += stopwatch.ElapsedMilliseconds;

        return results ?? new object[0];
    }
}