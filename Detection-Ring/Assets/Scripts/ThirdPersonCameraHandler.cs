using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Plumbly.CameraScripts
{
    public class ThirdPersonCameraHandler : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _aimVirtualCamera;
        [SerializeField] private Transform _followTarget;
        [SerializeField] private float _yawSensitivity = 1f;
        [SerializeField] private float _pitchSensitivity = 1f;
        [SerializeField, Range(0f, 0.5f)] private float _rotationSmoothTime = 0.03f;

        private UserInputAction _inputActions;
        private InputAction _aimAction;
        private InputAction _cameraAction;

        private bool _isAimming;
        private float _yawAngle = 0f;
        private float _pitchAngle = 0f;

        private Vector2 _rotationDelta = Vector2.zero;
        private Vector2 _rotationDeltaVelocity = Vector2.zero;
        

        private void Awake()
        {
            if (_aimVirtualCamera == null || _followTarget == null)
                Debug.Log(this + " is missing at least one requirements component. Check for missing dependencies.");

            _inputActions = SingletonUserControls.Get();
            _aimAction = _inputActions.PlayerActions.Aim;
            _cameraAction = _inputActions.PlayerMovement.Camera;
        }

        private void OnEnable()
        {
            _aimAction.performed += OnAimChanged;
            _aimAction.canceled += OnAimChanged;
        }

        private void OnDisable()
        {
            _aimAction.performed -= OnAimChanged;
            _aimAction.canceled -= OnAimChanged;
        }

        private void OnAimChanged(InputAction.CallbackContext obj)
        {
            if (obj.performed)
                _isAimming = true;
            else if (obj.canceled)
                _isAimming = false;

            _aimVirtualCamera.gameObject.SetActive(_isAimming);
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            Vector2 input = _cameraAction.ReadValue<Vector2>();

            _rotationDelta = Vector2.SmoothDamp(_rotationDelta, input, ref _rotationDeltaVelocity, _rotationSmoothTime);

            _yawAngle += input.x * _yawSensitivity * deltaTime;
            _pitchAngle += input.y * _pitchSensitivity * deltaTime;
            _pitchAngle = Mathf.Clamp(_pitchAngle, -90f, 90);

            _followTarget.rotation = Quaternion.Euler(_pitchAngle, _yawAngle, 0f);
        }
    }
}