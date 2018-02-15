using Autrage.LEX.NET.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityObject = UnityEngine.Object;

internal class Savegame : MonoBehaviour
{
    private HashSet<object> items = new HashSet<object>(new ReferenceComparer());

    public static bool Register(object instance) => Monoton<Savegame>.Instance.items.Add(instance);

    public static void Save(Stream stream) => Monoton<Savegame>.Instance.Persist(stream);

    public static void Load(Stream stream) => Monoton<Savegame>.Instance.StartCoroutine(Monoton<Savegame>.Instance.Restore(stream));

    private void Persist(Stream stream)
    {
        Marshaller marshaller = GetMarshaller();
        marshaller.Serialize(stream, items.Count);
        foreach (var item in items)
        {
            marshaller.Serialize(stream, item);
        }
    }

    private IEnumerator Restore(Stream stream)
    {
        foreach (UnityObject unityObject in items.OfType<UnityObject>())
        {
            Destroy(unityObject);
        }

        yield return 0;

        items = new HashSet<object>(new ReferenceComparer());

        Marshaller marshaller = GetMarshaller();
        for (int i = 0, count = marshaller.Deserialize<int>(stream); i < count; i++)
        {
            items.Add(marshaller.Deserialize(stream));
        }
    }

    private Marshaller GetMarshaller()
    {
        return new Marshaller()
        {
            new BehaviourSerializer(),
            new ComponentSerializer(),
            new GameObjectSerializer(),
            new PrimitiveSerializer(),
            new EnumSerializer(),
            new ValueTypeSerializer(),
            new ListSerializer(),
            new DictionarySerializer(),
            new GenericCollectionSerializer(),
            new DelegateSerializer(),
            new ContractSerializer()
        };
    }
}