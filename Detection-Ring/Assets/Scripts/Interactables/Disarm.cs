using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disarm : Intractable
{
    public GameObject toRemove;
    public static event Action<Disarm> OnDisarmed;
    public float _holdDuration;
    public float _holdTimeElapsed;

    public override string IntractableText => "Disarm";

    public override void OnInteract(GameObject subject)
    {
        _holdTimeElapsed += Time.deltaTime;

        if (_holdTimeElapsed >= _holdDuration)
        {
            OnDisarmed?.Invoke(this);
            gameObject.SetActive(false);
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
