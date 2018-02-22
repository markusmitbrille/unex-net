using Autrage.LEX.NET;
using System.Collections.ObjectModel;

public class IdentifiableCollection<TKey, TValue> : KeyedCollection<TKey, TValue>
    where TKey : Identity
    where TValue : IIdentifiable<TKey>
{
    public IdentifiableCollection() : base(new IdentityEqualityComparer<TKey>())
    {
    }

    protected override TKey GetKeyForItem(TValue item) => item.ID;

    protected override void InsertItem(int index, TValue item)
    {
        if (item == null)
        {
            Bugger.Warning($"Tried to insert null, discarding value!");
            return;
        }

        if (Contains(GetKeyForItem(item)))
        {
            Bugger.Warning($"{item.GetType()} {item} already contained in {GetType()}, discarding value!");
            return;
        }

        base.InsertItem(index, item);
    }

    protected override void SetItem(int index, TValue item)
    {
        if (item == null)
        {
            Bugger.Warning($"Tried to insert null, discarding value!");
            return;
        }

        if (!Contains(GetKeyForItem(item)))
        {
            Bugger.Warning($"{item.GetType()} {item} already contained in {GetType()}, discarding value!");
            return;
        }

        base.SetItem(index, item);
    }
}