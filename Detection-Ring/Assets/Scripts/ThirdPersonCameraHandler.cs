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
        [SerializeField] private Vector2 _sensitivity = new Vector2(2000f, 2000f);
        [SerializeField] private Vector2 _acceleration = new Vector2(2000f, 2000f);
        [SerializeField, Range(0f, 0.5f)] private float _rotationSmoothTime = 0.03f;

        private bool _isAimming = false;
        private Vector2 _rotation = Vector3.zero;
        private Vector2 _rotationVelociy = Vector2.zero;

        private UserInputAction _inputActions;
        private InputAction _aimAction;
        private InputAction _cameraAction;

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
            Vector2 targetVelociy = _cameraAction.ReadValue<Vector2>() * _sensitivity;

            _rotationVelociy = new Vector2(
                Mathf.MoveTowards(_rotationVelociy.x, targetVelociy.x, _acceleration.x * deltaTime),
                Mathf.MoveTowards(_rotationVelociy.y, targetVelociy.y, _acceleration.y * deltaTime));

            _rotation += _rotationVelociy * deltaTime;

            transform.rotation = Quaternion.Euler(_rotation.y, _rotation.x, 0f);
        }
    }
}