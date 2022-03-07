using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FirstPersonCamera), typeof(ThirdPersonCamera), typeof(Health))]
public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private bool _inFirstPerson;
    [SerializeField] private ThirdPersonCamera _thirdPersonCamera;
    [SerializeField] private FirstPersonCamera _firstPersonCamera;

    public Camera CurrentCamera { get; private set; }
    public Transform CurrentCameraTr { get; private set; }

    private void OnEnable()
    {
        GetComponent<Health>().OnDeath += HandleOnDeath;
    }

    private void OnDisable()
    {
        GetComponent<Health>().OnDeath -= HandleOnDeath;
    }

    private void Start()
    {
        _firstPersonCamera = GetComponent<FirstPersonCamera>();
        _thirdPersonCamera = GetComponent<ThirdPersonCamera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SwitchCamera(_inFirstPerson);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            SwitchCamera(!_inFirstPerson);
    }

    public void SwitchCamera(bool toFirstPerson)
    {
        _inFirstPerson = toFirstPerson;

        if (toFirstPerson)
        {
            _thirdPersonCamera.OnSwitchedAwayFrom();
            _thirdPersonCamera.enabled = false;
            _firstPersonCamera.enabled = true;
            _firstPersonCamera.OnSwitchedTo();
        }
        else
        {
            _firstPersonCamera.OnSwitchedAwayFrom();
            _firstPersonCamera.enabled = false;
            _thirdPersonCamera.enabled = true;
            _thirdPersonCamera.OnSwitchedTo();
        }
    }

    public void HandleOnDeath()
    {
        SwitchCamera(false);
        _thirdPersonCamera.enabled = false;
    }
}