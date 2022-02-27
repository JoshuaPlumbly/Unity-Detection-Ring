using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ThrowingItem))]
public class ThrowingItemPickup : Interactable
{
    public override void OnUpdate(GameObject subject)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (subject.TryGetComponent<Inventory>(out var inventory))
            {
                inventory.PickUp(GetComponent<ThrowingItem>());
            }
        }
    }

    public override void OnEnter(GameObject subject)
    {
    }

    public override void OnExit(GameObject subject)
    {
    }
}
