using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : Interactable
{
    [SerializeField] private string _useText;
    public override string UseText => _useText;

    public static event Action<PickUp> OnPickedUp;

    public override void Use()
    {
        gameObject.SetActive(false);
        OnPickedUp?.Invoke(this);
    }
}
