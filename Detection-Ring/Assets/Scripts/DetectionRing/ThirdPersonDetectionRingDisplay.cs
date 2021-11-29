using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ThirdPersonDetectionRingDisplay : MonoBehaviour
{
    [SerializeField] private DetectionRing _proximityRing;
    [SerializeField] float _radius = 2f;

    private LineRenderer _lineRenderer;
    private Vector3[] _ringPositions;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.loop = true;
    }

    private void OnEnable()
    {
        _proximityRing.OnActiveStateChanaged += SetActiveState;
        _proximityRing.OnSegmentRepositioned += Draw;

        SetSegmentCount(_lineRenderer.positionCount);
    }

    private void OnDisable()
    {
        _proximityRing.OnActiveStateChanaged -= SetActiveState;
        _proximityRing.OnSegmentRepositioned -= Draw;
    }

    public void SetActiveState(bool isActive)
    {
        _lineRenderer.enabled = isActive;
    }

    public void Draw(float[] segmentPositions)
    {
        if (_lineRenderer.positionCount != segmentPositions.Length)
            SetSegmentCount(segmentPositions.Length);

        Vector3[] newRingPositions = new Vector3[segmentPositions.Length];

        for (int i = 0; i < newRingPositions.Length; i++)
        {
            _ringPositions[i].y = segmentPositions[i];
            newRingPositions[i] = transform.position + _ringPositions[i];
        }

        _lineRenderer.SetPositions(newRingPositions);
    }

    public void SetSegmentCount(int segmentCount)
    {
        _ringPositions = new Vector3[segmentCount];
        _lineRenderer.positionCount = segmentCount;

        var anglePerNode = 360f / segmentCount;

        for (int i = 0; i < segmentCount; i++)
        {
            float angle = anglePerNode * i;
            float x = Mathf.Sin(angle * Mathf.PI / 180) * _radius;
            float z = Mathf.Cos(angle * Mathf.PI / 180) * _radius;

            _ringPositions[i] = new Vector3(x, 0f, z);
        }
    }
}