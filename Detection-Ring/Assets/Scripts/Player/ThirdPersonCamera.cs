using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour, IGameCamera
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform _lookDirectionTransform;
    [SerializeField] float _horizontalSensitivity = 200f;
    [SerializeField] float _verticalSensitivity = 200f;

    [SerializeField] float _lowestCameraPitch = -80f;
    [SerializeField] float _highestCameraPitch = 80f;

    [SerializeField] float _smoothSpeed;
    [SerializeField] Vector3 _offset;

    private float _yaw = 0f;
    private float _pitch = 0f;

    protected void Awake()
    {
        if (!_cameraTransform || !_lookDirectionTransform)
            Debug.LogWarning(this + " is missing dependencies.");
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        _yaw += mouseX * _horizontalSensitivity * Time.deltaTime;
        _pitch += mouseY * _verticalSensitivity * Time.deltaTime;

        _pitch = Mathf.Clamp(_pitch, _lowestCameraPitch, _highestCameraPitch);
        _yaw = _yaw % 360f;
    }

    private void LateUpdate()
    {
        _lookDirectionTransform.localRotation = Quaternion.Euler(_pitch, _yaw, 0f);
        _cameraTransform.localPosition = _offset;
    }

    public void OnSwitchedTo()
    {
        _cameraTransform.gameObject.SetActive(true);
        _cameraTransform.parent = _lookDirectionTransform;
        _cameraTransform.localPosition = _offset;
        _cameraTransform.localRotation = Quaternion.identity;

        _pitch = _lookDirectionTransform.eulerAngles.x;
        _yaw = _lookDirectionTransform.eulerAngles.y;
    }

    public void OnSwitchedAwayFrom()
    {
        
    }
}
