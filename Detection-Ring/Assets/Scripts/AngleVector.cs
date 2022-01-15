using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleVector : MonoBehaviour
{
    public Vector2 _pos;
    public float _AngleDec;

    private void Update()
    {
        _pos = transform.position;
        _AngleDec = Mathf.Atan2(_pos.y, _pos.x) * Mathf.Rad2Deg;
        _AngleDec = (_AngleDec + 360) % 360;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Vector2.zero, 0.2f);
        Gizmos.DrawSphere(transform.position, 0.2f);
        Gizmos.DrawLine(Vector2.zero, transform.position);
    }
}
