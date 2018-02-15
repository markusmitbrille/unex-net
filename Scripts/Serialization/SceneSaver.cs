using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;
using static Autrage.LEX.NET.Bugger;

public class SceneSaver : Monoton<SceneSaver>
{
    private Dictionary<string, byte[]> data = new Dictionary<string, byte[]>();

    private void OnEnable()
    {
        SceneManager.sceneLoaded += Load;
        SceneManager.sceneUnloaded += Unload;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= Load;
        SceneManager.sceneUnloaded -= Unload;
    }

    private void Load(Scene scene, LoadSceneMode mode)
    {
        Log("Scene loading.");

        StartCoroutine(LoadCoroutine(scene));

        Log("Scene loaded.");
    }

    private void Unload(Scene scene)
    {
        Log("Scene unloading.");

        using (MemoryStream stream = new MemoryStream())
        {
            Log("Saving scene data.");

            Savegame.Save(stream);
            data[scene.path] = stream.ToArray();

            Log("Scene data saved.");
        }

        Savegame.Unload();

        Log("Scene unloaded.");
    }

    private IEnumerator LoadCoroutine(Scene scene)
    {
        Log("Scene load coroutine started.");

        if (data.ContainsKey(scene.path))
        {
            Log("Scene data found, restoring.");

            Savegame.Unload();

            Log("Scene load coroutine yielding.");

            yield return null;

            Log("Scene load coroutine yielded.");

            using (MemoryStream stream = new MemoryStream(data[scene.path]))
            {
                Savegame.Load(stream);
            }

            Log("Scene data restored.");
        }

        Log("Scene load coroutine finished.");
    }
}