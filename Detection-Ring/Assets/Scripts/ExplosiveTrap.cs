using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveTrap : ExplosiveDevice
{
    [SerializeField] private Tripwire _tripwire;

    private void OnEnable()
    {
        _tripwire.OnTripped += Explode;
    }

    private void OnDisable()
    {
        _tripwire.OnTripped -= Explode;
    }
}
