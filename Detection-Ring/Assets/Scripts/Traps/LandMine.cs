using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour
{
    [SerializeField] private Explostion _explostion;
    [SerializeField] private DisruptableDeviceManager _disruptable;
    [SerializeField] private GameObject _explostionEffect;

    private Collider _triggerZone;

    private void Awake()
    {
        _triggerZone = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        _disruptable.OnStatusChanged += SetProximitySensorStatus;
    }

    private void OnDisable()
    {
        _disruptable.OnStatusChanged -= SetProximitySensorStatus;
    }

    private void OnTriggerEnter(Collider other)
    {
        _explostion.Explode(transform.position);

        if (_explostionEffect != null)
            Instantiate(_explostionEffect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
        
        Destroy(gameObject);
    }

    public void SetProximitySensorStatus(DeviceStatus status)
    {
        _triggerZone.enabled = status == DeviceStatus.Operating;
    }
}
