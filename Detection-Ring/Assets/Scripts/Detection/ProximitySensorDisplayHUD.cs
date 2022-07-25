using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Plumbly.DetectionSystems
{
    public class ProximitySensorDisplayHUD : MonoBehaviour
    {
        [SerializeField] private GameObject _RadarParent;
        [SerializeField] private Transform _RadarTranform;
        [SerializeField] private ProximitySensor2 _proximitySensor;
        [SerializeField] private Material _radarMaterial;

        private UnityEngine.Camera _camera;

        private void Awake()
        {
            UpdateShader(new float[1000]);
        }

        private void OnEnable()
        {
            _proximitySensor.UpdateProximityData += UpdateShader;
            _proximitySensor.UpdatePowerStatus += Power;

            _camera = UnityEngine.Camera.main;

        }

        private void OnDisable()
        {
            _proximitySensor.UpdateProximityData -= UpdateShader;
            _proximitySensor.UpdatePowerStatus -= Power;
        }

        private void Power(bool on)
        {
            UpdateShader(new float[1000]);
            _RadarParent.SetActive(on);
        }

        private void Update()
        {
            UnityEngine.Camera camera = UnityEngine.Camera.main;

            if (camera != null)
                return;

            float z = camera.transform.eulerAngles.y;
            _RadarTranform.rotation = Quaternion.Euler(0f, 0f, z);
        }

        private void UpdateShader(float[] nodes)
        {
            _radarMaterial.SetFloatArray("_Segments", nodes);
            _radarMaterial.SetInt("_SegmentsCount", nodes.Length);
        }
    }
}