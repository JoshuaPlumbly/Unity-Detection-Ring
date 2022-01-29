using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveIntractable : Intractable
{
    public static event Action<RemoveIntractable> OnRemoved;

    public override void OnInteract(GameObject subject)
    {
        OnRemoved?.Invoke(this);
        gameObject.SetActive(false);
    }
}
