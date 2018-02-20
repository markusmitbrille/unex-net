using System;
using UnityEngine;

public static class MonobehaviourExtensions
{
    public static T AddComponent<T>(this MonoBehaviour mono) where T : Component => mono.gameObject.AddComponent<T>();

    public static Component AddComponent(this MonoBehaviour mono, Type componentType) => mono.gameObject.AddComponent(componentType);
}