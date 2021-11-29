using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _nomalMoveSpeed = 6f;

    private Vector3 _moveDirection;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _rb.interpolation = RigidbodyInterpolation.Interpolate;
        _rb.drag = 6f;
    }

    private void Update()
    {
        GetMovementDirection();
    }

    private void GetMovementDirection()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        _moveDirection = transform.forward * v + transform.right * h;
    }

    private void LateUpdate()
    {
        _rb.AddForce(_moveDirection.normalized * _nomalMoveSpeed, ForceMode.Acceleration);
    }
}
