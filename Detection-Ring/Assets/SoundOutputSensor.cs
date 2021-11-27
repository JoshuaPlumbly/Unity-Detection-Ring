using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOutputSensor : MonoBehaviour
{
    [SerializeField] private float _detectionRange = 20f;
    [SerializeField] private float _rate = 1f;
    [SerializeField] AnimationCurve _rateCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    [SerializeField] LayerMask _layerMask;

    private AudioSource _audioSource;
    private float _lastBeep;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _lastBeep = 0f;
    }

    private void Update()
    {
        var detectedObjects = ProximityDetection.SeachProximity(transform.position, _detectionRange, _layerMask);

        if (detectedObjects.Length == 0)
            return;

        var closest = DetectionKey.ShortestDistance(detectedObjects).Distance;
        var rate = _rate * _rateCurve.Evaluate(closest / _detectionRange);

        if (_lastBeep + rate > Time.time)
            return;

        Debug.Log("Beep! " + rate);
        _lastBeep = Time.time;
        _audioSource.Play();
    }
}
