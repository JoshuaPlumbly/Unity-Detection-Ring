using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximitySensorB : MonoBehaviour
{
    [Header("Battery")]
    [SerializeField] Energy _batteryPower = new Energy(120f, 120f);
    [SerializeField] float _batteryConsumptionPerSecound = 1f;

    [Header("Detection")]
    [SerializeField] LayerMask _layerMask;
    [SerializeField] float _radius = 5f;

    [Header("Stenght")]
    [SerializeField, Range(0f,1f)] float _blendRadiusMin = 0.1f;
    [SerializeField, Range(0f,1f)] float _blendRadiusMax = 0.06f;
    [SerializeField] AnimationCurve _curve;


    private bool _isPowerOn;
    [SerializeField] private float[] _strengthValues = new float[90];

    public event Action<bool> OnSetPower;
    public event Action<float[]> OnSetStrengthValues;
    public event Action<float> OnChanageInBattery;

    public float[] IntensityValues => _strengthValues;
    
    public const float Tau = 6.2831853071796f;
    public const float RevPerRad = 0.1591549430919f;

    void Update()
    {
        CheckForPowerInput();
        
        if (!_isPowerOn)
            return;

        CheckBatteryLife();
        RefreshIntensityValues();
    }

    private void RefreshIntensityValues()
    {
        SetStrengthValuesToZero();

        Vector3 position = transform.position;
        Collider[] colliders = Physics.OverlapSphere(position, _radius, _layerMask, QueryTriggerInteraction.Collide);
        float maxSistanceSqr = _radius * _radius;

        for (int i = 0; i < colliders.Length; i++)
        {
            int closestNodeIndex = Mathf.RoundToInt(_strengthValues.Length * AzimuthRevolutions(position, colliders[i].ClosestPoint(position)));
            closestNodeIndex = Mathf.Clamp(closestNodeIndex, 0, _strengthValues.Length);
            float strength = Mathf.InverseLerp(maxSistanceSqr, 0f, (position - colliders[i].transform.position).sqrMagnitude);
            int nodesToBlend = Mathf.RoundToInt(Mathf.Lerp(_blendRadiusMin, _blendRadiusMax, strength) * _strengthValues.Length);

            for (int j = -nodesToBlend; j < nodesToBlend; j++)
            {
                int nodeIndex = ((closestNodeIndex + j % _strengthValues.Length) + _strengthValues.Length) % _strengthValues.Length;
                float strengthB = Mathf.Lerp(strength, 0f, _curve.Evaluate((float)Mathf.Abs(j) / nodesToBlend));
                _strengthValues[nodeIndex] += strengthB;
            }
        }

        OnSetStrengthValues?.Invoke(_strengthValues);
    }

    private void SetStrengthValuesToZero()
    {
        for (int i = 0; i < _strengthValues.Length; i++)
            _strengthValues[i] = 0f;
    }

    private void RefreshBatteryUIElements()
    {
        OnChanageInBattery?.Invoke(_batteryPower.CurrentOverMaximumValue());
    }

    private void CheckBatteryLife()
    {
        if (!_isPowerOn || _batteryPower.Extract(_batteryConsumptionPerSecound * Time.deltaTime) == 0)
            SetPower(false);

        RefreshBatteryUIElements();
    }

    private void CheckForPowerInput()
    {
        if (Input.GetKeyDown(KeyCode.T))
            SetPower(!_isPowerOn);
    }

    public void SetPower(bool on)
    {
        _isPowerOn = on;
        SetStrengthValuesToZero();
        OnSetStrengthValues?.Invoke(_strengthValues);
        OnSetPower?.Invoke(on);
        RefreshIntensityValues();
    }

    public static float AzimuthRadians(Vector3 from, Vector3 to)
    {
        float x = to.x - from.x;
        float z = to.z - from.z;

        return (Mathf.Atan2(x, z) + Tau) % Tau;
    }

    public static float AzimuthRevolutions(Vector3 from, Vector3 to)
    {
        return AzimuthRadians(from, to) * RevPerRad;
    }
}