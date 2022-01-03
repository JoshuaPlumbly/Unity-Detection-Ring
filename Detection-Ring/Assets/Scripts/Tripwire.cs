using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tripwire : MonoBehaviour
{
    [SerializeField] private Transform _tripwireEndTransform;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private LayerMask _mask;

    public event Action OnTripped;
    public Vector3 WireStart => transform.position;
    public Vector3 WireEnd => _tripwireEndTransform.position;
    public Vector3 WireVector { get; private set; }

    public static List<Tripwire> Tripwires { get; private set; } = new List<Tripwire>();

    private void Awake()
    {
        if (_tripwireEndTransform == null)
        {
            Debug.LogWarning(this + " is missing a refrence to a transform. This is needed for the tripwire's ending point.");
            this.enabled = false;
        }
    }

    private void Update()
    {
        Vector3 _tripwireEnd = _tripwireEndTransform.position;
        Vector3 WireVector = _tripwireEnd - transform.position;

        if (_lineRenderer != null)
        {
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, _tripwireEnd);
        }

        if (Physics.Raycast(transform.position, WireVector, out RaycastHit hit, WireVector.magnitude, _mask))
        {
            OnTripped?.Invoke();
            _lineRenderer.enabled = false;
            this.enabled = false;
        }
    }

    private void OnEnable()
    {
        Tripwires.Add(this);
    }

    private void OnDisable()
    {
        Tripwires.Remove(this);
        _lineRenderer.enabled = false;
    }
}
