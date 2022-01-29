using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ThrowingItem))]
public class ThrowingItemPickup : Intractable
{
    public override void OnInteract(GameObject subject)
    {
        if (subject.TryGetComponent<Inventory>(out var inventory)){
            inventory.PickUp(GetComponent<ThrowingItem>());
        }
    }
}
