using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plumbly.DetectionSystems
{
    public class ProximitySensor2 : MonoBehaviour
    {
        [Header("Battery")]
        [SerializeField] private Energy _batteryLife = new Energy(120f, 120f);
        [SerializeField] private float _batteryConsumptionPerSecound = 1f;

        [Header("Detection")]
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _radius = 5f;
        [SerializeField] private int _segments = 180;
        [SerializeField] private ProximityScaner proximityScaner;

        public event Action<bool> UpdatePowerStatus;
        public event Action<float[]> UpdateProximityData;
        public event Action<float> BatteryLifeUpdated;

        private bool _isPowerOn;

        private void Awake()
        {
            SingletonUserControls.Get().PlayerActions.UsePassiveItem1.started += UsePassiveItem1_started;
        }

        private void UsePassiveItem1_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            SetPower(!_isPowerOn);
        }

        void Update()
        {
            //PlayerInput();

            if (!_isPowerOn) return;

            BatteryLife();
            PerformProximityScan();
        }

        //private void PlayerInput()
        //{
        //    if (Plumbly.PlayerInput.UsePassiveItemFlag1)
        //        SetPower(!_isPowerOn);
        //}

        private void BatteryLife()
        {
            if (!_batteryLife.Consume(_batteryConsumptionPerSecound * Time.deltaTime))
                SetPower(false);

            float batteryLife = _batteryLife.CurrentCapacityNormalize();
            BatteryLifeUpdated?.Invoke(batteryLife);
        }

        private void PerformProximityScan()
        {
            float[] proximityResults = proximityScaner.Perform(transform.position, _segments, _radius, _layerMask);
            UpdateProximityData?.Invoke(proximityResults);
        }

        public void SetPower(bool on)
        {
            _isPowerOn = on;
            UpdatePowerStatus?.Invoke(on);
        }
    }
}