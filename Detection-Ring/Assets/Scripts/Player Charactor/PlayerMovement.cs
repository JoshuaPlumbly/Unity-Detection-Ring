using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController),typeof(Health))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform _lookDiectionTransform;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;

    [SerializeField] private float _nomalMoveSpeed = 6f;
    [SerializeField] private float _maxJumpHeight = 0.8f;

    [SerializeField] private float _gravity = -9.8f;

    private CharacterController _charactoerController;
    private Vector3 _currentInputMovement;
    private Vector3 _velociy;

    private void OnEnable()
    {
        GetComponent<Health>().OnDeath += Disable;
    }

    private void OnDisable()
    {
        GetComponent<Health>().OnDeath -= Disable;
    }

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

        _currentInputMovement = _lookDiectionTransform.forward * v + _lookDiectionTransform.right * h;
        _currentInputMovement *= _nomalMoveSpeed;

        _velociy.x = _currentInputMovement.x;
        _velociy.z = _currentInputMovement.z;
    }

    private void HandleGravity()
    {
        _velociy.y += _gravity * Time.deltaTime;
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(_jumpKey) && _charactoerController.isGrounded)
            _velociy.y = Mathf.Sqrt(_maxJumpHeight * -2 * _gravity);
    }

    private void ApplyFinalMovement()
    {
        _charactoerController.Move(_velociy * Time.deltaTime);
    }

    public void Disable() 
    {
        this.enabled = false; 
    }
}
