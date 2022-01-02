using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximitySensorA : MonoBehaviour
{
    [SerializeField] private float _detectionRange = 20f;
    [SerializeField] private float _rateScale = 1f;
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
        Collider[] detectedObjects = Physics.OverlapSphere(transform.position, _detectionRange, _layerMask);
        Collider nearestCollider = FindNearestCollider(detectedObjects);

        if (nearestCollider == null)
            return;

        float rate = _rateScale * _rateCurve.Evaluate((transform.position - nearestCollider.transform.position).sqrMagnitude / (_detectionRange * _detectionRange));

        if (_lastBeep + rate > Time.time)
            return;

        _lastBeep = Time.time;
        _audioSource.Play();
    }

    public Collider FindNearestCollider(Collider[] collider)
    {
        if (collider == null || collider.Length == 0)
            return null;

        Collider nearestCollider = collider[0];
        float nearestMagnitude = (collider[0].transform.position - transform.position).sqrMagnitude;

        for (int i = 1; i < collider.Length; i++)
        {
            if ((collider[i].transform.position - transform.position).sqrMagnitude < nearestMagnitude)
            {
                nearestCollider = collider[i];
                nearestMagnitude = (collider[i].transform.position - transform.position).sqrMagnitude;
            }    
        }

        return nearestCollider;
    }
}