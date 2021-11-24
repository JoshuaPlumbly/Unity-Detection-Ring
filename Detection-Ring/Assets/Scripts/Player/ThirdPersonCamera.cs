using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _horizontalSensitivity = 100f;
    [SerializeField] float _verticalSensitivity = 100f;
    [SerializeField] float _lowestCameraPitch = -80f;
    [SerializeField] float _highestCameraPitch = 80f;
    [SerializeField] float _mouseRotAcceleration;
    [SerializeField] float _smoothSpeed;
    [SerializeField] Vector3 _offset;

    [SerializeField] Transform _yawTr;
    [SerializeField] Transform _pitchTr;

    float _yaw;
    float _pitch;

    void Start()
    {
        _pitch = _pitchTr.rotation.eulerAngles.x;
        _yaw = _yawTr.rotation.eulerAngles.y;
    }

    private void Update()
    {
        ComputeUserInput();
    }

    private void LateUpdate()
    {
        if (_target == null)
            return;

        CameraRotation();
        CameraTranslate();
    }

    private void ComputeUserInput()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        _yaw += mouseX * _horizontalSensitivity * Time.deltaTime;
        _pitch += mouseY * _verticalSensitivity * Time.deltaTime;

        _pitch = Mathf.Clamp(_pitch, _lowestCameraPitch, _highestCameraPitch);
        _yaw = _yaw % 360f;
    }

    void CameraRotation()
    {
        Quaternion yAngle = Quaternion.Euler(_yawTr.rotation.eulerAngles.x, _yaw, _yawTr.rotation.eulerAngles.z);
        _yawTr.rotation = Quaternion.Slerp(_yawTr.rotation, yAngle, Time.deltaTime * _mouseRotAcceleration);

        Quaternion xAngle = Quaternion.Euler(_pitch, _yawTr.rotation.eulerAngles.y, _yawTr.rotation.eulerAngles.z);
        _pitchTr.rotation = Quaternion.Slerp(_pitchTr.rotation, xAngle, Time.deltaTime * _mouseRotAcceleration);
    }

    void CameraTranslate()
    {
        _yawTr.transform.position = Vector3.Lerp(_yawTr.transform.position, _target.position, Time.deltaTime * _smoothSpeed);
        transform.localPosition = Vector3.Lerp(transform.localPosition, _offset, Time.deltaTime * 5);
    }
}