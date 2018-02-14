using Autrage.LEX.NET.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityObject = UnityEngine.Object;

internal class Savegame : MonoBehaviour
{
    public HashSet<object> Items { get; } = new HashSet<object>(new ReferenceComparer());

    public void Persist(Stream stream)
    {
        Marshaller marshaller = GetMarshaller();
        marshaller.Serialize(stream, Items.Count);
        foreach (var item in Items)
        {
            marshaller.Serialize(stream, item);
        }
    }

    public void Restore(Stream stream) => StartCoroutine(RestorationCoroutine(stream));

    private IEnumerator RestorationCoroutine(Stream stream)
    {
        foreach (UnityObject unityObject in Items.OfType<UnityObject>())
        {
            Destroy(unityObject);
        }

        yield return 0;

        Items.Clear();

        Marshaller marshaller = GetMarshaller();
        for (int i = 0, count = marshaller.Deserialize<int>(stream); i < count; i++)
        {
            marshaller.Deserialize(stream);
            // Return value is ignored, since items are responsible for adding themselves to savegame
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