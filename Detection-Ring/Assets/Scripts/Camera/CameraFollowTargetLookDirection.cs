using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

namespace Plumbly.Camera
{
    public class CameraFollowTargetLookDirection : MonoBehaviour
    {
        [SerializeField] private float _yawSensitivity = 100f;
        [SerializeField] private float _pitchSensitivity = 100f;
        [SerializeField] private float _pitchClampMax = 80f;
        [SerializeField] private float _pitchClampMin = -80f;
        [SerializeField, Range(0f, 0.5f)] private float _rotationSmoothTime = 0.03f;

        private float _pitch;
        private float _yaw;

        private UserInputAction _inputActions;
        private InputAction _cameraAction;

        private void Awake()
        {
            _inputActions = SingletonUserControls.Get();
            _cameraAction = _inputActions.PlayerMovement.Camera;
        }


        private void LateUpdate()
        {
            float deltaTime = Time.deltaTime;
            Vector2 input = _cameraAction.ReadValue<Vector2>();

            _yaw += input.x * _yawSensitivity * deltaTime;
            _pitch += input.y * _pitchSensitivity * deltaTime;
            _pitch = Mathf.Clamp(_pitch, _pitchClampMin, _pitchClampMax);

            transform.rotation = Quaternion.Euler(_pitch, _yaw, 0f);
        }
    }
}