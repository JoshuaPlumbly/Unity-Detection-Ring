using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;

    [SerializeField] private float _nomalMoveSpeed = 6f;
    [SerializeField] private float _maxJumpHeight = 0.8f;

    [SerializeField] private float _gravityFalling = -9.8f;
    [SerializeField] private float _gravityNotFalling = -6f;
    [SerializeField] private float _gravityGrounded = -0.05f;
    [SerializeField] private float _terminalVelocity = -20f;

    private CharacterController _charactoerController;
    private Vector3 _currentInputMovement;
    private Vector3 _velociy;

    private void Awake()
    {
        _charactoerController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        GetMovementDirection();
        HandleGravity();
        HandleJump();
        ApplyFinalMovement();
    }

    private void GetMovementDirection()
    {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        _currentInputMovement = transform.forward * v + transform.right * h;
        _currentInputMovement *= _nomalMoveSpeed;

        _velociy.x = _currentInputMovement.x;
        _velociy.z = _currentInputMovement.z;
    }

    private void HandleGravity()
    {
        _velociy.y += _gravityFalling * Time.deltaTime;
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(_jumpKey) && _charactoerController.isGrounded)
            _velociy.y = Mathf.Sqrt(_maxJumpHeight * -2 * _gravityFalling);
    }

    private void ApplyFinalMovement()
    {
        _charactoerController.Move(_velociy * Time.deltaTime);
    }
}
