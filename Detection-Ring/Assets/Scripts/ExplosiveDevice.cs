using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveDevice : MonoBehaviour
{
    [SerializeField] protected int _damage = 100;
    [SerializeField] protected float _force = 700f;
    [SerializeField] protected float _range = 9f;
    [SerializeField] protected GameObject _explosionEffect;

    protected void Explode()
    {
        var colliders = Physics.OverlapSphere(transform.position, _range);

        for (int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(_force, transform.position, _range);
            }

            if (colliders[i].TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                var hitPoint = colliders[i].ClosestPoint(transform.position);
                var hitDirection = colliders[i].transform.position - transform.position;
                damageable.TakeDamage(_damage, hitPoint, hitDirection);
            }
        }

        Instantiate(_explosionEffect, transform.position, Quaternion.LookRotation(Vector3.up));
        Destroy(gameObject);
    }
}