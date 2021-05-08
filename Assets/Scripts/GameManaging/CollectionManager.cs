
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// A generic collection with semi random retrieval methods
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class CollectionManager<T> : MonoBehaviour
{
    protected class ItemData
    {
        public T Object;
        public int PickCount;
    }

    protected List<ItemData> Collection = new List<ItemData>();
    
    public T GetRandomItem(int seed = 1)
    {
        //Semi random gamemode picker
        List<ItemData> temp = new List<ItemData>();
        foreach (var item in Collection)
        {
            if (temp.Count == 0)
            {
                temp.Add(item);
            }
            else if (temp[temp.Count - 1].PickCount == item.PickCount)
            {
                temp.Add(item);
            }
            else if (temp[temp.Count - 1].PickCount > item.PickCount)
            {
                temp.Clear();
                temp.Add(item);
            }
        }
        var index = Random.Range(0, temp.Count);
        temp[index].PickCount += 1;

        return temp[index].Object;
    }

    public int GetRandomItemIndex(int seed = 1)
    {
        //Semi random gamemode picker
        List<ItemData> temp = new List<ItemData>();
        foreach (var item in Collection)
        {
            if (temp.Count == 0)
            {
                temp.Add(item);
            }
            else if (temp[temp.Count - 1].PickCount == item.PickCount)
            {
                temp.Add(item);
            }
            else if (temp[temp.Count - 1].PickCount > item.PickCount)
            {
                temp.Clear();
                temp.Add(item);
            }
        }
        var index = Random.Range(0, temp.Count);
        temp[index].PickCount++;
        var result = Collection.FindIndex(x => x == temp[index]);

        return result;
    }

    public T GetItem(int index)
    {
        if (index >= Collection.Count || index < 0)
            return default(T);

        
        return Collection[index].Object;
    }

    public void AddCollection(List<T> collection)
    {
        foreach (var item in collection)
        {
            AddItem(item);
        }
    }

    public void AddItem(T item)
    {
        var data = new ItemData();
        data.Object = item;
        data.PickCount = 0;
        Collection.Add(data);
    }
}

