using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disarm : Intractable
{
    [SerializeField] private GameObject _toRemove;
    [SerializeField] private float _holdDuration;
    [SerializeField] private float _holdTimeElapsed;

    public static event Action<Disarm> OnDisarmed;

    public override string IntractableText => "Disarm";

    public override void OnInteract(GameObject subject)
    {
        _holdTimeElapsed += Time.deltaTime;

        if (_holdTimeElapsed >= _holdDuration)
        {
            OnDisarmed?.Invoke(this);
            _toRemove.SetActive(false);
        }
    }

    public override void OnInteractDown(GameObject subject)
    {
        _holdTimeElapsed = 0f;
    }

    public override void OnInteractUp(GameObject subject)
    {

    }
}
