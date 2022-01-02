using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour
{
    [SerializeField] int _damage =  100;
    [SerializeField] float _range = 9f;
    [SerializeField] GameObject _explosionEffect;

    private void OnTriggerEnter(Collider other)
    {
        Explode();
    }

    private void Explode()
    {
        var colliders = Physics.OverlapSphere(transform.position, _range);

        for (int i = 0; i < colliders.Length; i++)
        {
            var health = colliders[i].GetComponentInParent<Health>();

            if (health == null)
                continue;

            var hitPoint = colliders[i].ClosestPoint(transform.position);
            var hitDirection = colliders[i].transform.position - transform.position;
            health.TakeHit(_damage, hitPoint, hitDirection);
        }

        Instantiate(_explosionEffect, transform.position, Quaternion.LookRotation(Vector3.up));
        Destroy(gameObject);
    }
}