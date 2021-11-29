using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] FirstPersonCamera _firstPersonCamera;
    [SerializeField] ThirdPersonCamera _thirdPersonCamera;

    private bool _inFirstPerson;

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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            SwitchCamera(!_inFirstPerson);
    }
}
