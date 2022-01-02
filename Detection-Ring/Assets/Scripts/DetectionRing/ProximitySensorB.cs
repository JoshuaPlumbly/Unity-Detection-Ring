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
    [SerializeField] AnimationCurve _curve;
    [SerializeField] private float[] _nodes;

    private bool _isActive;

    public event Action<bool> OnSetActive;
    public event Action<float[]> OnNodesUpdated;

    public float[] Nodes => _nodes;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            SetActive(!_isActive);

        if (!_isActive || _batteryPower.Request(_batteryConsumptionPerSecound * Time.deltaTime) == 0)
            SetActive(false);

        if (!_isActive)
            return;

        Collider[] colliders = Physics.OverlapSphere(transform.position, _maxDistance, _layerMask);

        float maxSistanceSqr = _maxDistance * _maxDistance;

        for (int i = 0; i < _nodes.Length; i++)
            _nodes[i] = 0f;

        for (int i = 0; i < colliders.Length; i++)
        {
            int closestNodeIndex = Mathf.RoundToInt(_nodes.Length * CompassAngleNormalised(transform.position, colliders[i].transform.position));
            closestNodeIndex = closestNodeIndex < _nodes.Length ? closestNodeIndex : 0;
            float strength = Mathf.InverseLerp(maxSistanceSqr, 0f, (transform.position - colliders[i].transform.position).sqrMagnitude);
            int nodesToBlend = Mathf.RoundToInt(Mathf.Lerp(_blendRadiusMin, _blendRadiusMax, strength) * _nodes.Length);

            for (int j = -nodesToBlend; j < nodesToBlend; j++)
            {
                int nodeIndex = ((closestNodeIndex + j % _nodes.Length) + _nodes.Length) % _nodes.Length;
                float strengthB = Mathf.Lerp(strength, 0f, _curve.Evaluate((float)Mathf.Abs(j) / nodesToBlend));
                _nodes[nodeIndex] += strengthB;
            }
        }

        OnNodesUpdated?.Invoke(_nodes);
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
}