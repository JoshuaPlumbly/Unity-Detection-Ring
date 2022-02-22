using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Tripwire : MonoBehaviour
{

    [SerializeField] private Transform _tripwireStartTransform;
    [SerializeField] private Transform _tripwireEndTransform;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private LayerMask _mask;

    public event Action OnTripped;
    public Vector3 WireVector { get; private set; }

    public static List<Tripwire> Tripwires { get; private set; } = new List<Tripwire>();

    public Vector3 TripwireStart => _tripwireStartTransform.position;
    public Vector3 TripwireEnd => _tripwireEndTransform.position;


    private void Awake()
    {
        if (_tripwireEndTransform == null || _tripwireStartTransform == null)
        {
            Debug.LogWarning(this + " is missing the transform component(s) for the tripwire's start and/or end point.");
            this.enabled = false;
            return;
        }

        Vector3 tripWireStartToEnd = TripwireEnd - TripwireStart;

        if (_lineRenderer != null)
        {
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, _tripwireStartTransform.position);
            _lineRenderer.SetPosition(1, _tripwireEndTransform.position);
        }

        transform.position = TripwireStart + (tripWireStartToEnd * 0.5f);
        transform.LookAt(_tripwireEndTransform);

        var collider = GetComponent<BoxCollider>();
        collider.center = Vector3.zero;
        collider.size = new Vector3(0.05f, 0.05f, tripWireStartToEnd.magnitude);
        collider.isTrigger = true;
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

    private void OnTriggerEnter(Collider other)
    {
        OnTripped?.Invoke();
        _lineRenderer.enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        this.enabled = false;
    }
}
