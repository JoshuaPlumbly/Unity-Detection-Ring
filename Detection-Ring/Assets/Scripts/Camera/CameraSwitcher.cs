using Plumbly.Camera;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Plumbly
{
    [RequireComponent(typeof(Health))]
    public class CameraSwitcher : MonoBehaviour
    {
        [SerializeField] private bool _startInFirstPerson;
        [SerializeField] private GameObject  _thirdPersonSystem;
        [SerializeField] private GameObject _firstPersonSystems;

        public static bool FirstPerson { get; private set; }

        private Health _health;
        private UserInputAction _inputActions;

        private void Start()
        {
            SwitchCamera(_startInFirstPerson);
        }

        private void OnEnable()
        {
            _inputActions = SingletonUserControls.Get();
            _inputActions.PlayerActions.ToggleCamera.started += CyclyeCamera;
            _health = GetComponent<Health>();
            _health.Died += Died;
        }

        private void OnDisable()
        {
            _inputActions.PlayerActions.ToggleCamera.started -= CyclyeCamera;
            _health.Died -= Died;
        }

        private void CyclyeCamera(InputAction.CallbackContext obj)
        {
            SwitchCamera(!FirstPerson);
        }

        private void SwitchCamera(bool toFirstPerson)
        {
            FirstPerson = toFirstPerson;
            _firstPersonSystems.SetActive(toFirstPerson);
            _thirdPersonSystem.SetActive(!toFirstPerson);
        }

        private void Died()
        {
            SwitchCamera(false);
        }
    }
}