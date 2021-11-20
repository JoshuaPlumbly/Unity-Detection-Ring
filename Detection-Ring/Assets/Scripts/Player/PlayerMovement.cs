using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _normalMoveSpeed = 4f;
    [SerializeField] private float _weight = 9.8f;

    CharacterController _character;
    Transform _camera;
    Vector3 _velocity = Vector3.zero;

    private void Awake()
    {
        _character = GetComponent<CharacterController>();
        _camera = Camera.main.transform;
    }

    private void Update()
    {
        ComputeWalkingMovement();
        ComputeGravity();

        _character.Move(_velocity);
        _velocity = Vector3.zero;
    }

    private void ComputeWalkingMovement()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        var movement = _camera.forward * v;
        movement += _camera.right * h;
        movement.y = 0;
        movement.Normalize();
        movement *= _normalMoveSpeed * Time.deltaTime;

        _velocity += movement;
    }

    private void ComputeGravity()
    {
        if (_character.isGrounded)
            _velocity.y = 0f;

        _velocity.y -= _weight * Time.deltaTime;
    }
}