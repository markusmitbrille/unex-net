using Autrage.LEX.NET.Extensions;
using Autrage.LEX.NET.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static Autrage.LEX.NET.Bugger;

internal class ComponentSerializer : ObjectSerializer
{
    private Dictionary<Component, long> componentIDs = new Dictionary<Component, long>();
    private long nextID = 0;

    private Dictionary<long, Component> components = new Dictionary<long, Component>();

    public override bool CanHandle(Type type) => typeof(Component).IsAssignableFrom(type);

    public override bool Serialize(Stream stream, object instance)
    {
        Type type = instance.GetType();
        if (!CanHandle(type))
        {
            Warning($"Cannot handle {type}!");
            return false;
        }

        Component component = (Component)instance;
        if (componentIDs.ContainsKey(component))
        {
            Marshaller.Serialize(stream, component.gameObject);
            stream.Write(componentIDs[component]);
            stream.Write(false);
            return true;
        }

        long id = nextID++;
        componentIDs[component] = id;

        Marshaller.Serialize(stream, component.gameObject);
        stream.Write(id);
        stream.Write(true);

        return SerializeMembers(stream, component);
    }

    public override object Deserialize(Stream stream, Type type)
    {
        if (!CanHandle(type))
        {
            Warning($"Cannot handle {type}!");
            return null;
        }

        GameObject gameObject = Marshaller.Deserialize<GameObject>(stream);
        if (gameObject == null)
        {
            Warning($"Could not deserialize component's game object!");
            return null;
        }

        long? id = stream.ReadLong();
        if (id == null)
        {
            Warning($"Could not read component id!");
            return null;
        }

        bool? hasPayload = stream.ReadBool();
        if (hasPayload == null)
        {
            Warning($"Could not read component payload indicator!");
            return null;
        }

        Component component = components.GetValueOrDefault(id.Value);
        if (component == null)
        {
            component = gameObject.AddComponent(type);
            components[id.Value] = component;
        }

        if (hasPayload == true)
        {
            DeserializeMembers(stream, component);
        }

        return component;
    }
}
