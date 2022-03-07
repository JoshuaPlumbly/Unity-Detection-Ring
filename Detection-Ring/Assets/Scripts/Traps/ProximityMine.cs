using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ProximityMine : ExplosiveDevice
{
    [SerializeField] private float _angleRange;

    private void OnTriggerStay(Collider other)
    {
        Vector3 dirToTarget = (other.ClosestPoint(transform.position + transform.forward) - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, dirToTarget);

        if (angle <= _angleRange)
            Explode();
    }
}
