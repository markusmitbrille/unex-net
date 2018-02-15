using Autrage.LEX.NET.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Autrage.LEX.NET.Bugger;
using UnityObject = UnityEngine.Object;

internal class Savegame
{
    private static HashSet<object> items = new HashSet<object>(new ReferenceComparer());

    public static bool Register(object instance) => items.Add(instance);

    public static void Save(Stream stream)
    {
        Log("Persisting savegame items.");

        Marshaller marshaller = GetMarshaller();
        marshaller.Serialize(stream, items.Count);
        foreach (var item in items)
        {
            marshaller.Serialize(stream, item);
        }

        Log("Savegame items persisted.");
    }

    public static void Load(Stream stream)
    {
        Log("Restoring savegame items.");

        Marshaller marshaller = GetMarshaller();
        for (int i = 0, count = marshaller.Deserialize<int>(stream); i < count; i++)
        {
            items.Add(marshaller.Deserialize(stream));
        }

        Log("Savegame items restored.");
    }

    public static void Unload()
    {
        Log("Clearing savegame items.");

        foreach (UnityObject unityObject in items.OfType<UnityObject>())
        {
            UnityObject.Destroy(unityObject);
        }

        items = new HashSet<object>(new ReferenceComparer());

        Log("Savegame items cleared, unity objects will be destroyed next frame.");
    }

    private static Marshaller GetMarshaller()
    {
        return new Marshaller()
        {
            new BehaviourSerializer(),
            new GameObjectSerializer(),
            new PrimitiveSerializer(),
            new EnumSerializer(),
            new ValueTypeSerializer(),
            new StringSerializer(),
            new ListSerializer(),
            new DictionarySerializer(),
            new GenericCollectionSerializer(),
            new DelegateSerializer(),
            new ContractSerializer()
        };
    }
}