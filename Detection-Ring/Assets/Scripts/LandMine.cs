using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour
{
    [SerializeField] int _damage;
    [SerializeField] GameObject _explosionEffect;

    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();

        if (health != null)
        {
            var hitPoint = other.ClosestPoint(transform.position);
            var hitDirection = other.transform.position - transform.position;
            health.TakeHit(_damage, hitPoint, hitDirection);
        }

        Explode();
    }

    private void Explode()
    {
        Instantiate(_explosionEffect, transform.position, Quaternion.LookRotation(Vector3.up));
        Destroy(gameObject);
    }
}