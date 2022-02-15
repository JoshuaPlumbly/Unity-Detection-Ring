using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ProximitySensorDisplayRing : MonoBehaviour
{
    [SerializeField] private ProximitySensorB _proximitySensor;
    [SerializeField] private float _radius = 2f;
    [SerializeField] private float _heightScale = 0.8f;
    [SerializeField] private AnimationCurve _strengthCurve = new AnimationCurve(new Keyframe(0f,0f), new Keyframe(1f,1f), new Keyframe(2f,1.5f), new Keyframe(5f, 2f));

    private LineRenderer _lineRenderer;
    private Vector3[] _ringPositions;

    public const float doublePI = (float)Mathf.PI * 2f;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.loop = true;

        if (_proximitySensor == null)
            Debug.LogWarning(this + " is missing a proximity refrence.");

        UpdateAllPositions(_proximitySensor.IntensityValues);
    }

    private void OnEnable()
    {
        _proximitySensor.OnSetPower += SetPowerOn;
        _proximitySensor.OnSetIntensityValues += Refresh;
    }

    private void OnDisable()
    {
        _proximitySensor.OnSetPower -= SetPowerOn;
        _proximitySensor.OnSetIntensityValues -= Refresh;
    }

    public void SetPowerOn(bool isActive)
    {
        _lineRenderer.enabled = isActive;
    }

    private void UpdateAllPositions(float[] nodes)
    {
        if (_lineRenderer.positionCount != nodes.Length)
            _lineRenderer.positionCount = nodes.Length;

        if (_ringPositions == null || _ringPositions.Length != nodes.Length)
            _ringPositions = new Vector3[nodes.Length];

        _lineRenderer.positionCount = nodes.Length;

        for (int i = 0; i < nodes.Length; i++)
        {
            float circumferenceProgress = (float)i / nodes.Length;
            float currentRadian = circumferenceProgress * doublePI;

            float x = Mathf.Sin(currentRadian) * _radius;
            float z = Mathf.Cos(currentRadian) * _radius;
            float y = EvaluateYPosition(nodes[i]);

            _ringPositions[i] = new Vector3(x, y, z);
            _lineRenderer.SetPosition(i, transform.position + _ringPositions[i]);
        }
    }

    private Vector3[] GenerateRingVertices(int vertexCount, float radius)
    {
        Vector3[] vertices = new Vector3[vertexCount];
        float radiansPerVertex = (1f / vertexCount) * doublePI;

        for (int i = 0; i < vertexCount; i++)
        {
            float radians = radiansPerVertex * i;
            vertices[i] = new Vector3(Mathf.Sin(radians) * radians, Mathf.Cos(radians) * radians, 0f);
        }

        return vertices;
    }

    private void Refresh(float[] nodes)
    {
        _lineRenderer.positionCount = nodes.Length;

        if (_ringPositions.Length != nodes.Length)
            _ringPositions = GenerateRingVertices(nodes.Length, _radius);

        for (int i = 0; i < nodes.Length; i++)
        {
            _ringPositions[i].y = EvaluateYPosition(nodes[i]);
            _lineRenderer.SetPosition(i, transform.position + _ringPositions[i]);
        }
    }

    private float EvaluateYPosition(float y)
    {
        return _strengthCurve.Evaluate(y) * _heightScale;
    }
}