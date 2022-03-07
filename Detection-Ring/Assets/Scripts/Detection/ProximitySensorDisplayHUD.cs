using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProximitySensorDisplayHUD : MonoBehaviour
{
    [SerializeField] private GameObject _RadarParent;
    [SerializeField] private Transform _RadarTranform;
    [SerializeField] private ProximitySensorB _proximitySensor;
    [SerializeField] private Material _radarMaterial;

    private void OnEnable()
    {
        UpdateShader(new float[1000]);

        _proximitySensor.OnSetStrengthValues += UpdateShader;
        _proximitySensor.OnSetPower += Power;
    }

    private void OnDisable()
    {
        _proximitySensor.OnSetStrengthValues -= UpdateShader;
        _proximitySensor.OnSetPower -= Power;
    }

    private void Power(bool on)
    {
        UpdateShader(new float[1000]);
        _RadarParent.SetActive(on);
    }

    private void Update()
    {
        float z = CameraManager.Current.transform.eulerAngles.y;
        _RadarTranform.rotation = Quaternion.Euler(0f, 0f, z);
    }

    private void UpdateShader(float[] nodes)
    {
        _radarMaterial.SetFloatArray("_Segments", nodes);
        _radarMaterial.SetInt("_SegmentsCount", nodes.Length);
    }
}