using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : Intractable
{
    public static event Action<PickUp> OnPickedUp;

    public override void OnInteract()
    {
        OnPickedUp?.Invoke(this);
        gameObject.SetActive(false);
    }
}
