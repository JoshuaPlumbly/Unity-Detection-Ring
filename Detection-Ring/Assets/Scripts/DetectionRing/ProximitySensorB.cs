using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximitySensorB : MonoBehaviour
{
    [Header("Battery")]
    [SerializeField] Capacity _batteryPower;
    [SerializeField] float _batteryConsumptionPerSecound;

    [Header("Detection")]
    [SerializeField] LayerMask _layerMask;
    [SerializeField] float _maxDistance;
    [SerializeField, Range(0f,1f)] float _blendRadiusMin = 0.1f;
    [SerializeField, Range(0f,1f)] float _blendRadiusMax = 0.06f;
    [SerializeField] private float _wireBlendRadiusScale = 1.5f;
    [SerializeField] AnimationCurve _curve;


    private bool _isActive;
    [SerializeField] private float[] _intensityValues;

    public event Action<bool> OnSetActive;
    public event Action<float[]> OnIntensityValuesUpdated;
    public event Action<float> OnChanageInBattery;

    public float[] IntensityValues => _intensityValues;

    void Update()
    {
        CheckForPowerInput();
        CheckBatteryLife();

        if (!_isActive)
            return;

        RefreshBatteryUIElements();
        RefreshIntensityValues();
    }

    private void RefreshIntensityValues()
    {
        SetAllHeightElementsToZero();

        Collider[] colliders = Physics.OverlapSphere(transform.position, _maxDistance, _layerMask);

        float maxSistanceSqr = _maxDistance * _maxDistance;

        for (int i = 0; i < colliders.Length; i++)
        {
            int closestNodeIndex = Mathf.RoundToInt(_intensityValues.Length * CompassAngleNormalised(transform.position, colliders[i].transform.position));
            closestNodeIndex = closestNodeIndex < _intensityValues.Length ? closestNodeIndex : 0;
            float strength = Mathf.InverseLerp(maxSistanceSqr, 0f, (transform.position - colliders[i].transform.position).sqrMagnitude);
            int nodesToBlend = Mathf.RoundToInt(Mathf.Lerp(_blendRadiusMin, _blendRadiusMax, strength) * _intensityValues.Length);

            for (int j = -nodesToBlend; j < nodesToBlend; j++)
            {
                int nodeIndex = ((closestNodeIndex + j % _intensityValues.Length) + _intensityValues.Length) % _intensityValues.Length;
                float strengthB = Mathf.Lerp(strength, 0f, _curve.Evaluate((float)Mathf.Abs(j) / nodesToBlend));
                _intensityValues[nodeIndex] += strengthB;
            }
        }

        for (int i = 0; i < Tripwire.Tripwires.Count; i++)
        {
            Vector3 closest = ClosestPointToLine(transform.position, Tripwire.Tripwires[i].TripwireStart, Tripwire.Tripwires[i].TripwireEnd);

            if ((closest - transform.position).sqrMagnitude > _maxDistance * _maxDistance)
                continue;

            int closestNodeIndex = Mathf.RoundToInt(_intensityValues.Length * CompassAngleNormalised(transform.position, closest));
            closestNodeIndex = closestNodeIndex < _intensityValues.Length ? closestNodeIndex : 0;
            float strength = Mathf.InverseLerp(maxSistanceSqr, 0f, (transform.position - closest).sqrMagnitude);
            int nodesToBlend = Mathf.RoundToInt(Mathf.Lerp(_blendRadiusMin, _blendRadiusMax, strength) * _wireBlendRadiusScale * _intensityValues.Length);

            for (int j = -nodesToBlend; j < nodesToBlend; j++)
            {
                int nodeIndex = ((closestNodeIndex + j % _intensityValues.Length) + _intensityValues.Length) % _intensityValues.Length;
                float strengthB = Mathf.Lerp(strength, 0f, _curve.Evaluate((float)Mathf.Abs(j) / nodesToBlend));
                _intensityValues[nodeIndex] += strengthB;
            }
        }

        OnIntensityValuesUpdated?.Invoke(_intensityValues);
    }

    private void SetAllHeightElementsToZero()
    {
        for (int i = 0; i < _intensityValues.Length; i++)
            _intensityValues[i] = 0f;
    }

    private void RefreshBatteryUIElements()
    {
        OnChanageInBattery?.Invoke(_batteryPower.CurrentOverMaximumValue());
    }

    private void CheckBatteryLife()
    {
        if (!_isActive || _batteryPower.Request(_batteryConsumptionPerSecound * Time.deltaTime) == 0)
            SetActive(false);
    }

    private void CheckForPowerInput()
    {
        if (Input.GetKeyDown(KeyCode.T))
            SetActive(!_isActive);
    }

    public void SetActive(bool isActive)
    {
        _isActive = isActive;
        OnSetActive?.Invoke(isActive);
    }

    public static float CompassAngle(Vector3 to, Vector3 from)
    {
        float x = from.x - to.x;
        float z = from.z - to.z;
        float angle = Mathf.Atan2(x, z) * Mathf.Rad2Deg;
        return Mathf.Sign(angle) < 0 ? 360f + angle : angle;
    }

    public static float CompassAngleNormalised(Vector3 to, Vector3 from)
    {
        return CompassAngle(to, from) / 360f;
    }

    public static Vector3 ClosestPointToLine(Vector3 origin, Vector3 start, Vector3 end) 
    {
        Vector3 dist = end - start;

        float t = (
            (origin.x - start.x) * dist.x +
            (origin.y - start.y) * dist.y +
            (origin.z - start.z) * dist.z) /
            (dist.x * dist.x + dist.y * dist.y + dist.z * dist.z);

        return Vector3.Lerp(start, end, t);
    }
}