using System.Collections.Generic;
using UnityEngine;

public class ComponetRetrieval
{
    public static T[] GetComponentsInColliders<T>(Collider[] colliders)
    {
        List<T> list = new List<T>();

        for (int i = 0; i < colliders.Length; i++)
        {
            T item = colliders[i].GetComponent<T>();

            if (item != null)
                list.Add(item);
        }

        return list.ToArray();
    }

    public static T[] FromOverlapSphere<T>(Vector3 orgin, float radius)
    {
        return GetComponentsInColliders<T>(Physics.OverlapSphere(orgin, radius));
    }
}