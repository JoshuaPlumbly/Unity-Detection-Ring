using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : Explosive
{
    [SerializeField] private DisruptableDeviceManager _disruptable;

    private Collider _triggerZone;

    private void Awake()
    {
        _triggerZone = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        _disruptable.OnStatusChanged += OnStausUpdated;
    }

    private void OnDisable()
    {
        _disruptable.OnStatusChanged -= OnStausUpdated;
    }

    private void OnTriggerEnter(Collider other)
    {
        Explode();
    }

    public void OnStausUpdated(DeviceStatus status)
    {
        _triggerZone.enabled = status == DeviceStatus.Operating;
    }
}