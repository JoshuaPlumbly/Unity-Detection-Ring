using System;
using System.Collections.Generic;

public class SortedList<T> where T : IComparable<T>
{
    public List<T> Items { get; private set; } = new List<T>();
    
    public T this[int index]
    { 
        get 
        { 
            return Items[index]; 
        } 
    }

    public void Clear() => Items.Clear();
    public int Count => Items.Count;

    public void Add(T newItem)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (newItem.CompareTo(Items[i]) < 1)
            {
                Items.Insert(i, newItem);
                return;
            }
        }

        Items.Add(newItem);
    }
}