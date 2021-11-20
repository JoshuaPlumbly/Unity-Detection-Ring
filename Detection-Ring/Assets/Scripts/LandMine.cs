using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour
{
    [SerializeField] GameObject _explosionEffect;

    private void OnTriggerEnter(Collider other)
    {
        Explode();
        Debug.Log("Boom");
    }

    private void Explode()
    {
        Instantiate(_explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
