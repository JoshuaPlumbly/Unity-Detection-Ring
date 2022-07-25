using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Plumbly.Camera
{
    public class CameraToogleAimmingZoom : MonoBehaviour
    {
        [SerializeField] private GameObject _cameraAimming;

        private UserInputAction _inputActions;
        private InputAction _aimAction;

        public bool IsAimming { get; private set; }

        private void Awake()
        {
            if (_cameraAimming == null)
                Debug.Log(this + " is missing a component. Check for missing dependencies.");
        }

        private void OnEnable()
        {
            _inputActions = SingletonUserControls.Get();
            _aimAction = _inputActions.PlayerActions.Aim;

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
                IsAimming = true;
            else if (obj.canceled)
                IsAimming = false;

            _cameraAimming.SetActive(IsAimming);
        }
    }
}