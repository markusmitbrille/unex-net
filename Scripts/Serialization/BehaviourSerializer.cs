using Autrage.LEX.NET.Extensions;
using Autrage.LEX.NET.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static Autrage.LEX.NET.Bugger;

internal class BehaviourSerializer : ObjectSerializer
{
    private Dictionary<Behaviour, long> componentIDs = new Dictionary<Behaviour, long>();
    private long nextID = 0;

    private Dictionary<long, Behaviour> components = new Dictionary<long, Behaviour>();

    public override bool CanHandle(Type type) => typeof(Behaviour).IsAssignableFrom(type);

    public override bool Serialize(Stream stream, object instance)
    {
        Type type = instance.GetType();
        if (!CanHandle(type))
        {
            Warning($"Cannot handle {type}!");
            return false;
        }

        Behaviour behaviour = (Behaviour)instance;
        if (componentIDs.ContainsKey(behaviour))
        {
            Marshaller.Serialize(stream, behaviour.gameObject);
            stream.Write(componentIDs[behaviour]);
            stream.Write(false);
            return true;
        }

        long id = nextID++;
        componentIDs[behaviour] = id;

        Marshaller.Serialize(stream, behaviour.gameObject);
        stream.Write(id);
        stream.Write(true);

        stream.Write(behaviour.enabled);

        return SerializeMembers(stream, behaviour);
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
            Warning($"Could not deserialize behaviour's game object!");
            return null;
        }

        long? id = stream.ReadLong();
        if (id == null)
        {
            Warning($"Could not read behaviour id!");
            return null;
        }

        bool? hasPayload = stream.ReadBool();
        if (hasPayload == null)
        {
            Warning($"Could not read behaviour payload indicator!");
            return null;
        }

        bool? enabled = stream.ReadBool();
        if (enabled == null)
        {
            Warning($"Could not read behaviour enabled value!");
            return null;
        }

        Behaviour behaviour = components.GetValueOrDefault(id.Value);
        if (behaviour == null)
        {
            behaviour = (Behaviour)gameObject.AddComponent(type);
            components[id.Value] = behaviour;
        }

        if (hasPayload == true)
        {
            DeserializeMembers(stream, behaviour);
        }

        behaviour.enabled = enabled.Value;

        return behaviour;
    }
}