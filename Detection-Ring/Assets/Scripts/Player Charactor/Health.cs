using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private Resource _hitPoints = new Resource(100, 100);
    [SerializeField] private GameObject _hitEffect;

    public event System.Action OnTakeDamage;
    public event System.Action Died;

    public float GetHealthNormalize()
    {
        return _hitPoints.CurrentCapacityNormalize();
    }

    public void TakeDamage(int damage)
    {
        _hitPoints.Subtract(damage);
        OnTakeDamage?.Invoke();

        if (_hitPoints.IsEmpty())
        {
            Death();
            Died?.Invoke();
        }
    }

    public void TakeDamage(int damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        TakeDamage(damage);

        if (_hitPoints.IsEmpty())
            Instantiate(_hitEffect, hitPoint, Quaternion.FromToRotation(Vector3.forward, hitDirection));
    }

    private void Death()
    {
        MonoBehaviour[] monoBehaviours = transform.GetComponentsInChildren<MonoBehaviour>();

        for (int i = 0; i < monoBehaviours.Length; i++)
        {
            monoBehaviours[i].enabled = false;
        }


        MeshRenderer[] meshrenders = transform.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < meshrenders.Length; i++)
        {
            meshrenders[i].enabled = false;
        }
    }
}
