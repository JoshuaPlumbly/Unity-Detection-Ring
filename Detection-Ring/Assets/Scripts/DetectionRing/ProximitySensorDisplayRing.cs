using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ProximitySensorDisplayRing : MonoBehaviour
{
    [SerializeField] private ProximitySensorB _proximitySensor;
    [SerializeField] private float _radius = 2f;
    [SerializeField] private float _heightScale = 0.8f;
    [SerializeField] private AnimationCurve _strengthToHeight = new AnimationCurve(new Keyframe(0f,0f), new Keyframe(1f,1f), new Keyframe(2f,1.5f), new Keyframe(5f, 2f));

    private LineRenderer _lineRenderer;
    private Vector3[] _ringPositions;

    public const float doublePI = (float)Mathf.PI * 2f;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.loop = true;

        if (_proximitySensor == null)
            Debug.LogWarning(this + " is missing a proximity refrence.");

        UpdateAllPositions(_proximitySensor.Nodes);
    }

    private void OnEnable()
    {
        _proximitySensor.OnSetActive += SetActiveState;
        _proximitySensor.OnNodesUpdated += UpdateYPositions;
    }

    private void OnDisable()
    {
        _proximitySensor.OnSetActive -= SetActiveState;
        _proximitySensor.OnNodesUpdated -= UpdateYPositions;
    }

    public void SetActiveState(bool isActive)
    {
        _lineRenderer.enabled = isActive;
    }

    private void Update()
    {
        for (int i = 0; i < _ringPositions.Length; i++)
            _lineRenderer.SetPosition(i, transform.position + _ringPositions[i]);
    }

    private void UpdateAllPositions(float[] nodes)
    {
        if (nodes.Length <= 0)
            return;

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
        }
    }

    private void UpdateYPositions(float[] nodes)
    {
        if (nodes.Length <= 0)
            return;

        if (nodes.Length != _ringPositions.Length || nodes.Length != _lineRenderer.positionCount)
        {
            UpdateAllPositions(nodes);
            return;
        }

        for (int i = 0; i < nodes.Length; i++)
            _ringPositions[i].y = EvaluateYPosition(nodes[i]);
    }

    private float EvaluateYPosition(float y)
    {
        return _strengthToHeight.Evaluate(y) * _heightScale;
    }
}