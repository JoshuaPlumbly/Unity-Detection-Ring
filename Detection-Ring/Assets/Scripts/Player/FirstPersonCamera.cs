using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private float sensitivityX = 1f;
    [SerializeField] private float sensitivityY = 1f;

    public Camera Camera { get; private set; }

    float _pitch;
    float _yaw;

    private void Awake()
    {
        Camera = GetComponentInChildren<Camera>();

        if (Camera == null)
            Debug.LogWarning(this.name + " should have a camera that is accessible with the GetComponentInChildren methord.");
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = -Input.GetAxisRaw("Mouse Y");

        _yaw += mouseX * sensitivityX;
        _pitch += mouseY * sensitivityY;

        _pitch = Mathf.Clamp(_pitch, -90f, 90f);
        Camera.transform.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
        transform.localRotation = Quaternion.Euler(0f, _yaw, 0f);
    }

    private void OnDisable()
    {
        Camera.enabled = false;

        if (Camera.GetComponent<AudioListener>() != null)
            Camera.GetComponent<AudioListener>().enabled = false;
    }

    private void OnEnable()
    {
        Camera.enabled = true;

        if (Camera.GetComponent<AudioListener>() != null)
            Camera.GetComponent<AudioListener>().enabled = true;

        _yaw = transform.eulerAngles.y;
        _pitch = 0f;
    }
}
