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
        if (_firstPersonCamera == null)
            Debug.Log(this.name + " is missing a first person camera.");

        if (_thirdPersonCamera == null)
            Debug.Log(this.name + " is missing a third person camera.");

        SwitchCamera(_inFirstPerson);
    }

    public void SwitchCamera(bool firstPerson)
    {
        CameraManager.SwithCamera(firstPerson ? _firstPersonCamera : _thirdPersonCamera as InGameCamera);
        _inFirstPerson = firstPerson;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            SwitchCamera(!_inFirstPerson);
    }
}