using UnityEngine;
using static Autrage.LEX.NET.Bugger;

public abstract class Monoton<T> : MonoBehaviour where T : Monoton<T>
{
    private static T instance = null;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject gameObject = new GameObject(typeof(T).Name);
                    instance = gameObject.AddComponent<T>();
                    DontDestroyOnLoad(gameObject);
                }
            }

            return instance;
        }
    }

    public static bool IsInitialized { get { return instance != null; } }

    private void Awake()
    {
        T monoton = this as T;
        if (monoton == null)
        {
            Warning($"{name} is not of type {typeof(T)}, destroying inconsistent monoton!");
            Destroy(this);
        }

        if (instance == null)
        {
            instance = monoton;
            DontDestroyOnLoad(this);
        }
        else
        {
            Log($"Monoton {typeof(T)} already initialized, destroying superfluous instance.");
            Destroy(this);
        }
    }
}