using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class GameObjectExtensions
{
    public static bool HasParent(this GameObject gameObject)
    {
        return gameObject.transform.parent != null;
    }

    public static GameObject GetParent(this GameObject gameObject)
    {
        return gameObject.transform.parent?.gameObject;
    }

    public static void SetParent(this GameObject gameObject, GameObject parent)
    {
        gameObject.transform.parent = parent.transform;
    }

    public static IEnumerable<GameObject> GetChildren(this GameObject gameObject)
    {
        foreach (Transform child in gameObject.transform)
        {
            yield return child.gameObject;
        }
    }

    public static IEnumerable<GameObject> GetSiblings(this GameObject gameObject)
    {
        foreach (Transform child in gameObject.transform.parent)
        {
            if (child != gameObject.transform)
            {
                yield return child.gameObject;
            }
        }
    }

    public static bool HasParent(this MonoBehaviour behaviour)
    {
        return behaviour.transform.parent != null;
    }

    public static GameObject GetParent(this MonoBehaviour behaviour)
    {
        return behaviour.transform.parent?.gameObject;
    }

    public static IEnumerable<GameObject> GetChildren(this MonoBehaviour behaviour)
    {
        foreach (Transform child in behaviour.transform)
        {
            yield return child.gameObject;
        }
    }

    public static IEnumerable<GameObject> GetSiblings(this MonoBehaviour behaviour)
    {
        foreach (Transform child in behaviour.transform.parent)
        {
            if (child != behaviour.transform)
            {
                yield return child.gameObject;
            }
        }
    }
}
