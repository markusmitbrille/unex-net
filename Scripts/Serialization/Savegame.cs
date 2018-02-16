using Autrage.LEX.NET;
using Autrage.LEX.NET.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityObject = UnityEngine.Object;

internal class Savegame
{
    private static HashSet<object> items = new HashSet<object>(new ReferenceComparer());

    public static IEnumerable<object> Items
        => (from item in items where item != null select item).ToList();

    public static IEnumerable<UnityObject> UnityObjects
        => from unityObject in items.OfType<UnityObject>() where unityObject != null select unityObject;

    public static bool Register(object instance) => instance == null ? false : items.Add(instance);

    public static void Save(Stream stream) => GetMarshaller().Serialize(stream, Items);

    public static void Load(Stream stream)
    {
        IEnumerable<object> items = GetMarshaller().Deserialize<IEnumerable<object>>(stream);
        if (items == null)
        {
            Bugger.Warning("Could not deserialize savegame items!");
            return;
        }

        Savegame.items = new HashSet<object>(items, new ReferenceComparer());
    }

    public static void Unload()
    {
        foreach (UnityObject unityObject in UnityObjects)
        {
            UnityObject.Destroy(unityObject);
        }

        items = new HashSet<object>(new ReferenceComparer());
    }

    private static Marshaller GetMarshaller()
    {
        Marshaller marshaller = new Marshaller()
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

        marshaller.TypeResolver = (assembly, name, b) => assembly.GetType(name);
        return marshaller;
    }
}