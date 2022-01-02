using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private bool _inFirstPerson;
    [SerializeField] FirstPersonCamera _firstPersonCamera;
    [SerializeField] ThirdPersonCamera _thirdPersonCamera;

    public Camera CurrentCamera { get; private set; }
    public Transform CurrentCameraTr { get; private set; }

    private void Start()
    {
        _firstPersonCamera = FindObjectOfType<FirstPersonCamera>();
        _thirdPersonCamera = FindObjectOfType<ThirdPersonCamera>();

        if (_firstPersonCamera == null)
            Debug.Log(this.name + " is missing a first person camera.");

        if (_thirdPersonCamera == null)
            Debug.Log(this.name + " is missing a third person camera.");

        SwitchCamera(_inFirstPerson);
    }

    public void SwitchCamera(bool firstPerson)
    {
        _inFirstPerson = firstPerson;
        _firstPersonCamera.enabled = firstPerson;
        _thirdPersonCamera.enabled = !firstPerson;

        CurrentCamera = firstPerson ? _firstPersonCamera.Camera : _thirdPersonCamera.Camera;
        CurrentCameraTr = CurrentCamera.transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            SwitchCamera(!_inFirstPerson);
    }
}
