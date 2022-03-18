using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour, IGameCamera
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform _lookDirectionTransform;
    [SerializeField] private float _horizontalSensitivity = 200f;
    [SerializeField] private float _verticalSensitivity = 200f;

    private float _pitch;
    private float _yaw;

    protected void Awake()
    {
        if (!_cameraTransform || !_lookDirectionTransform)
            Debug.LogWarning(this + " is missing dependencies.");
    }

    //private void Update()
    //{
    //    float mouseX = Input.GetAxisRaw("Mouse X");
    //    float mouseY = -Input.GetAxisRaw("Mouse Y");

    //    _yaw += mouseX * _horizontalSensitivity * Time.deltaTime;
    //    _pitch += mouseY * _verticalSensitivity * Time.deltaTime;

    //    _pitch = Mathf.Clamp(_pitch, -90f, 90f);
    //    _yaw = _yaw % 360f;
    //}

    private void LateUpdate()
    {
        _lookDirectionTransform.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
        transform.localRotation = Quaternion.Euler(0f, _yaw, 0f);
    }

    public void OnSwitchedTo()
    {
        _yaw = transform.localRotation.eulerAngles.y;

        _cameraTransform.gameObject.SetActive(true);
        _cameraTransform.parent = _lookDirectionTransform;
        _cameraTransform.localPosition = Vector3.zero;
        _cameraTransform.localRotation = Quaternion.identity;
    }

    public void OnSwitchedAwayFrom()
    {

    }
}
