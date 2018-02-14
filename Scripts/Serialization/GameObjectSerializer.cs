using Autrage.LEX.NET.Extensions;
using Autrage.LEX.NET.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using static Autrage.LEX.NET.Bugger;

internal class GameObjectSerializer : Serializer
{
    private Dictionary<GameObject, long> gameObjectIDs = new Dictionary<GameObject, long>();
    private long nextID = 0;

    private Dictionary<long, GameObject> gameObjects = new Dictionary<long, GameObject>();

    public override bool CanHandle(Type type) => typeof(GameObject) == type;

    public override bool Serialize(Stream stream, object instance)
    {
        Type type = instance.GetType();
        if (!CanHandle(type))
        {
            Warning($"Cannot handle {type}!");
            return false;
        }

        GameObject gameObject = (GameObject)instance;
        if (gameObjectIDs.ContainsKey(gameObject))
        {
            stream.Write(gameObjectIDs[gameObject]);
            stream.Write(false);
            return true;
        }

        long id = nextID++;
        gameObjectIDs[gameObject] = id;

        stream.Write(id);
        stream.Write(true);

        return SerializeGameObject(stream, gameObject);
    }

    public override object Deserialize(Stream stream, Type type)
    {
        if (!CanHandle(type))
        {
            Warning($"Cannot handle {type}!");
            return null;
        }

        long? id = stream.ReadLong();
        if (id == null)
        {
            Warning($"Could not read game object id!");
            return null;
        }

        bool? hasPayload = stream.ReadBool();
        if (hasPayload == null)
        {
            Warning($"Could not read game object payload indicator!");
            return null;
        }

        GameObject gameObject = gameObjects.GetValueOrDefault(id.Value);
        if (gameObject == null)
        {
            gameObject = new GameObject();
            gameObjects[id.Value] = gameObject;
        }

        if (hasPayload == true)
        {
            DeserializeGameObject(stream, gameObject);
        }

        return gameObject;
    }

    private bool SerializeGameObject(Stream stream, GameObject gameObject)
    {
        stream.Write(gameObject.name, Marshaller.Encoding);
        stream.Write(gameObject.tag, Marshaller.Encoding);
        stream.Write(gameObject.layer);
        stream.Write(gameObject.activeSelf);

        SerializeComponents(stream, gameObject);
        SerializeParent(stream, gameObject);

        return true;
    }

    private void SerializeComponents(Stream stream, GameObject gameObject)
    {
        IEnumerable<Component> components =
            from component in gameObject.GetComponents<Component>()
            where !component.GetType().IsDefined(typeof(IgnoreComponentAttribute), true)
            select component;

        if (components.OfType<IgnoreComponents>().Any())
        {
            stream.Write(false);
            return;
        }

        stream.Write(true);
        stream.Write(components.Count());
        foreach (Component component in components)
        {
            Marshaller.Serialize(stream, component);
        }
    }

    private void SerializeParent(Stream stream, GameObject gameObject)
    {
        GameObject parent = gameObject.GetParent();
        if (parent == null)
        {
            stream.Write(false);
        }
        else
        {
            stream.Write(true);
            SerializeGameObject(stream, parent);
        }
    }

    private void DeserializeGameObject(Stream stream, GameObject gameObject)
    {
        gameObject.name = stream.ReadString(Marshaller.Encoding) ?? "";
        gameObject.tag = stream.ReadString(Marshaller.Encoding) ?? "";
        gameObject.layer = stream.ReadInt() ?? 0;
        gameObject.SetActive(stream.ReadBool() ?? true);

        DeserializeComponents(stream, gameObject);
        DeserializeParent(stream, gameObject);
    }

    private void DeserializeComponents(Stream stream, GameObject gameObject)
    {
        long? count = stream.ReadInt();
        if (count == null)
        {
            Warning($"Could not read game object {gameObject.name} component count!");
            return;
        }

        for (int i = 0; i < count.Value; i++)
        {
            Component component = Marshaller.Deserialize<Component>(stream);
            if (component == null)
            {
                Warning($"Could not deserialize component on game object {gameObject.name}!");
            }

            // component value is not used since the component serializer is responsible for adding it to the game object
        }
    }

    private void DeserializeParent(Stream stream, GameObject gameObject)
    {
        bool? hasParent = stream.ReadBool();
        if (hasParent == null)
        {
            Warning($"Could not read parent indicator for game object {gameObject.name}!");
            return;
        }
        if (hasParent == false)
        {
            return;
        }

        GameObject parent = Marshaller.Deserialize<GameObject>(stream);
        if (parent == null)
        {
            Warning($"Could not deserialize parent of game object {gameObject.name}!");
            return;
        }

        gameObject.SetParent(parent);
    }
}