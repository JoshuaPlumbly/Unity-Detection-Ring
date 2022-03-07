using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class DisruptableDeviceManager : MonoBehaviour, IDisruptable
{
    [SerializeField] private IDisruted[] _disrutedElements;
    [SerializeField] private float _disruptDuration = 7f;
    [SerializeField] private float _restoreTime = 0.8f;

    public event Action<DeviceStatus> OnStatusChanged;
    
    private IEnumerator _disruptCoroutine;

    public void Disrupt()
    {
        if (_disruptCoroutine != null)
            StopCoroutine(_disruptCoroutine);

        _disruptCoroutine = DisruptTime(_disruptDuration);
        StartCoroutine(_disruptCoroutine);
    }

    private IEnumerator DisruptTime(float time)
    {
        OnStatusChanged?.Invoke(DeviceStatus.Disrupted);

        yield return new WaitForSeconds(time);
        OnStatusChanged?.Invoke(DeviceStatus.Recovering);

        yield return new WaitForSeconds(_restoreTime);
        OnStatusChanged?.Invoke(DeviceStatus.Operating);
    }
}
