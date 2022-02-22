using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int _hitPoints = 100;
    [SerializeField] private int _maxHitPoints = 100;
    [SerializeField] private GameObject _hitEffect;

    public event System.Action OnTakeDamage;
    public event System.Action OnDeath;

    public int GetHealth() => _hitPoints;
    public int GetMaxHealth() => _maxHitPoints;
    public float HealthDividedByMaxHealth() => (float)_hitPoints / _maxHitPoints;

    public void TakeDamage(int damage)
    {
        _hitPoints -= damage;
        _hitPoints = _hitPoints < 0 ? 0 : _hitPoints;
        OnTakeDamage?.Invoke();

        if (_hitPoints <= 0)
        {
            Death();
            OnDeath?.Invoke();
        }
    }

    public void TakeDamage(int damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        TakeDamage(damage);

        if (_hitPoints <= 0)
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
