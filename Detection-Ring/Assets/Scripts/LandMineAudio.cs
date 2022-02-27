using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LandMineAudio : MonoBehaviour
{
    [SerializeField] private DisruptableDeviceManager _disruptable;
    [SerializeField] private AudioClip _disruptionAudio;

    private AudioSource _audioSource;
    private IEnumerator _loopAudio;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _disruptable.OnStatusChanged += OnStatusChanged;
    }

    private void OnDisable()
    {
        _disruptable.OnStatusChanged -= OnStatusChanged;
    }

    private void OnStatusChanged(DeviceStatus status)
    {
        if (status == DeviceStatus.Disrupted)
        {
            _audioSource.clip = _disruptionAudio;
            _audioSource.loop = true;
            _audioSource.Play();
        }
        else
            _audioSource.Stop();
    }
}
