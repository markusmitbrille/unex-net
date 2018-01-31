using UnityEngine;

public class Monoton<T> : MonoBehaviour where T : MonoBehaviour
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
}
