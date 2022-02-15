using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    [SerializeField] protected int _damage = 100;
    [SerializeField] protected float _range = 9f;
    [SerializeField] protected GameObject _explosionEffect;

    protected void Explode()
    {
        var colliders = Physics.OverlapSphere(transform.position, _range);

        for (int i = 0; i < colliders.Length; i++)
        {
            var health = colliders[i].GetComponentInParent<IDamageable>();

            if (health == null)
                continue;

            var hitPoint = colliders[i].ClosestPoint(transform.position);
            var hitDirection = colliders[i].transform.position - transform.position;
            health.TakeDamage(_damage, hitPoint, hitDirection);
        }

        Instantiate(_explosionEffect, transform.position, Quaternion.LookRotation(Vector3.up));
        Destroy(gameObject);
    }
}