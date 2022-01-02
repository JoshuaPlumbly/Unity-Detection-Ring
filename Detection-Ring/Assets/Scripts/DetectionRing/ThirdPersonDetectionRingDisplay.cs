using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ThirdPersonDetectionRingDisplay : MonoBehaviour
{
    [SerializeField] private ProximitySensorB _proximityRing;
    [SerializeField] private float _radius = 2f;
    [SerializeField] private float _heightScale = 0.8f;
    [SerializeField] private AnimationCurve _strengthToHeight = new AnimationCurve(new Keyframe(0f,0f), new Keyframe(1f,1f), new Keyframe(2f,1.5f), new Keyframe(5f, 2f));

    private LineRenderer _lineRenderer;
    private Vector3[] _ringPositions;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.loop = true;

        if (_proximityRing == null)
            Debug.LogWarning(this + " is missing a proximity refrence.");

        UpdateAllPositions(_proximityRing.Nodes);
    }

    private void OnEnable()
    {
        _proximityRing.OnSetActive += SetActiveState;
        _proximityRing.OnNodesUpdated += UpdateYPositions;
    }

    private void OnDisable()
    {
        _proximityRing.OnSetActive -= SetActiveState;
        _proximityRing.OnNodesUpdated -= UpdateYPositions;
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

        float anglePerNode = 360f / nodes.Length;
        _lineRenderer.positionCount = nodes.Length;

        for (int i = 0; i < nodes.Length; i++)
        {
            float angle = anglePerNode * i;
            float x = Mathf.Sin(angle * Mathf.PI / 180f) * _radius;
            float y = EvaluateYPosition(nodes[i]);
            float z = Mathf.Cos(angle * Mathf.PI / 180f) * _radius;
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