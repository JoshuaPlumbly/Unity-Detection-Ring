using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

namespace Plumbly
{
    public class CinemachinePOVExtension : CinemachineExtension
    {
        [SerializeField] private float _horizontalSpeed = 10f;
        [SerializeField] private float _verticalSpeed = 10f;
        [SerializeField] private float _clampPitchUp = 80f;
        [SerializeField] private float _clampPitchDown = -80f;

        private Vector3 _rotation = Vector3.zero;

        private UserInputAction _inputActions;
        private InputAction _cameraAction;

        protected override void Awake()
        {
            _inputActions = SingletonUserControls.Get();
            _cameraAction = _inputActions.PlayerMovement.Camera;
            base.Awake();
        }

        private void Start()
        {
            _rotation = transform.localRotation.eulerAngles;
        }

        private Vector2 GetCameraInput()
        {
            return _cameraAction.ReadValue<Vector2>();
        }

        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (vcam.Follow)
            {
                if (stage == CinemachineCore.Stage.Aim)
                {
                    Vector2 input = GetCameraInput();
                    //Debug.Log(input);
                    _rotation.x += input.x * _verticalSpeed * deltaTime;
                    _rotation.y += input.y * _horizontalSpeed * deltaTime;
                    _rotation.y = Mathf.Clamp(_rotation.y, _clampPitchDown, _clampPitchUp);
                    state.RawOrientation = Quaternion.Euler(_rotation.y, _rotation.x, 0f);
                }
            }
        }
    }
}