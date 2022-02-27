using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class LightBillbordBlink : MonoBehaviour, IDisruted
{
    [SerializeField] private DisruptableDeviceManager _disruptable;
    [SerializeField] private float _flickerMin = 0.1f;
    [SerializeField] private float _flickerMax = 0.3f;
    [SerializeField] private float _disruptRate = 1f;
    [SerializeField] private float _recoverRate = 2f;

    private MeshRenderer _lightBillbord;
    private float _timer;
    private float _rate;

    private void Awake()
    {
        _lightBillbord = GetComponent<MeshRenderer>();
        OnStausUpdated(DeviceStatus.Operating);
    }

    private void OnEnable()
    {
        _disruptable.OnStatusChanged += OnStausUpdated;
    }

    private void OnDisable()
    {
        _disruptable.OnStatusChanged -= OnStausUpdated;
    }

    private void Update()
    {
        _timer -= Time.deltaTime * _rate;

        if (_timer <= 0f)
        {
            _lightBillbord.enabled = !_lightBillbord.enabled;
            _timer = Random.Range(_flickerMin, _flickerMax);
        }
    }

    public void OnStausUpdated(DeviceStatus status)
    {
        switch (status)
        {
            case DeviceStatus.Operating:
                _rate = 0f;
                _timer = 0.001f;
                _lightBillbord.enabled = false;
                break;
            case DeviceStatus.Disrupted:
                _rate = _disruptRate;
                break;
            case DeviceStatus.Recovering:
                _rate = _recoverRate;
                break;
            default:
                break;
        }
    }
}
