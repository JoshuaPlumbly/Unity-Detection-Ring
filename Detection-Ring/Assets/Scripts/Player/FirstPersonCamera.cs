using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : InGameCamera
{
    [SerializeField] private float sensitivityX = 200f;
    [SerializeField] private float sensitivityY = 200f;

    private float _pitch;
    private float _yaw;

    private GameObject cameraGameObject;
    private Transform cameraTransform;

    protected override void Awake()
    {
        GameCamera = GetComponentInChildren<Camera>();
        cameraGameObject = GameCamera.gameObject;
        cameraTransform = GameCamera.transform;
        cameraGameObject.SetActive(false);
        this.enabled = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = -Input.GetAxisRaw("Mouse Y");

        _yaw += mouseX * sensitivityX;
        _pitch += mouseY * sensitivityY;

        _pitch = Mathf.Clamp(_pitch, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
        transform.localRotation = Quaternion.Euler(0f, _yaw, 0f);
    }

    public override void OnSwitchedTo()
    {
        this.enabled = true;
        cameraGameObject.SetActive(true);

        _yaw = transform.eulerAngles.y;
        _pitch = 0f;
    }

    public override void OnSwitchedAwayFrom()
    {
        cameraGameObject.SetActive(false);
    }
}
