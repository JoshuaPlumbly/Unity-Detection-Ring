using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Plumbly
{
    [RequireComponent(typeof(CharacterController), typeof(Health), typeof(CameraManager))]
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;

        [Header("Movement Parameters")]
        [SerializeField] private float _nomalMoveSpeed = 6f;
        [SerializeField] private float _maxJumpHeight = 0.8f;
        [SerializeField] private float _gravity = -9.8f;
        [SerializeField] private float _rotationSpeed = 4f;

        private CharacterController _charactoerController;
        private Vector3 _moveDirection;
        private Vector3 _velociy;
        private UserInputAction _inputActions;
        private InputAction _moveAction;

        private void Awake()
        {
            _inputActions = SingletonUserControls.Get();
            _charactoerController = GetComponent<CharacterController>();

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnEnable()
        {
            GetComponent<Health>().Died += Disable;
            _moveAction = _inputActions.PlayerMovement.Movement;
            _inputActions.PlayerActions.Jump.started += PerformJump;
        }

        private void OnDisable()
        {
            GetComponent<Health>().Died -= Disable;
            _inputActions.PlayerActions.Jump.started -= PerformJump;
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            Vector3 input = _moveAction.ReadValue<Vector2>();
            Movement(deltaTime, input);
            ApplyGravityToVelocity();
            ApplyVelocitytWithCharacterController();
        }

        #region Movement
        private void Movement(float deltaTime, Vector2 input)
        {
            Vector3 motion = CameraManager.GetCameraForward(Camera.main) * input.y;
            motion += CameraManager.GetCameraRight(Camera.main) * input.x;
            motion.y = 0f;
            motion.Normalize();
            motion *= _nomalMoveSpeed * deltaTime;

            _charactoerController.Move(motion);

            if (motion != Vector3.zero)
                TurnTowards(motion, deltaTime);
        }

        private void TurnTowards(Vector3 direction, float deltaTime)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            targetRotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * deltaTime);
            transform.rotation = targetRotation;
        }

        private void ApplyGravityToVelocity()
        {
            _velociy.y += _gravity * Time.deltaTime;
        }

        private void PerformJump(InputAction.CallbackContext obj)
        {
            if (_charactoerController.isGrounded)
                _velociy.y = Mathf.Sqrt(_maxJumpHeight * -2 * _gravity);
        }

        private void ApplyVelocitytWithCharacterController()
        {
            _charactoerController.Move(_velociy * Time.deltaTime);
        }
        #endregion

        public void Disable()
        {
            this.enabled = false;
        }
    }
}