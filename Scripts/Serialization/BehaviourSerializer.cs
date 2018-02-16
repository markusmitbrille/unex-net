using Autrage.LEX.NET.Extensions;
using Autrage.LEX.NET.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static Autrage.LEX.NET.Bugger;

internal class BehaviourSerializer : ObjectSerializer
{
    private Dictionary<Behaviour, long> behaviourIDs = new Dictionary<Behaviour, long>();
    private long nextID = 0;

    private Dictionary<long, Behaviour> behaviours = new Dictionary<long, Behaviour>();

    public override bool CanHandle(Type type) => typeof(Behaviour).IsAssignableFrom(type) && type.IsDefined(typeof(DataContractAttribute), true);

    public override bool Serialize(Stream stream, object instance)
    {
        Type type = instance.GetType();
        if (!CanHandle(type))
        {
            Warning($"Cannot handle {type}!");
            return false;
        }

        Behaviour behaviour = (Behaviour)instance;
        if (behaviourIDs.ContainsKey(behaviour))
        {
            stream.Write(behaviourIDs[behaviour]);
            stream.Write(false);
            return true;
        }

        long id = nextID++;
        behaviourIDs[behaviour] = id;

        stream.Write(id);
        stream.Write(true);

        Marshaller.Serialize(stream, behaviour.gameObject);
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

        long? id = stream.ReadLong();
        if (id == null)
        {
            Warning($"Could not read behaviour id!");
            return null;
        }

        Behaviour behaviour = behaviours.GetValueOrDefault(id.Value);

        bool? hasPayload = stream.ReadBool();
        if (hasPayload == null)
        {
            Warning($"Could not read behaviour payload indicator!");
            return null;
        }
        if (hasPayload == false)
        {
            return behaviour;
        }

        GameObject gameObject = Marshaller.Deserialize<GameObject>(stream);
        if (gameObject == null)
        {
            Warning($"Could not deserialize behaviour's game object!");
            return null;
        }

        bool? enabled = stream.ReadBool();
        if (enabled == null)
        {
            Warning($"Could not read behaviour enabled value!");
            return null;
        }

        if (behaviour == null)
        {
            behaviour = (Behaviour)gameObject.AddComponent(type);
            if (behaviour == null)
            {
                Warning($"Could not create {type} behaviour on {gameObject.name}!");
                return null;
            }

            behaviours[id.Value] = behaviour;
        }

        behaviour.enabled = enabled.Value;

        DeserializeMembers(stream, behaviour);

        return behaviour;
    }
}