using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ThrowingItem))]
public class ThrowingItemPickup : Intractable
{
    public override string IntractableText => "Pick up";

    public override void OnInteract(GameObject subject)
    {
        if (subject.TryGetComponent<Inventory>(out var inventory)){
            inventory.PickUp(GetComponent<ThrowingItem>());
        }
    }

    public override void OnInteractDown(GameObject subject)
    {
    }

    public override void OnInteractUp(GameObject subject)
    {
    }
}
