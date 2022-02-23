using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disrupter : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _range = 500f;

    private void Awake()
    {
        if (_audioSource != null)
            _audioSource.loop = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Shoot();
    }

    private void Shoot()
    {
        Transform cameraTransform = Camera.main.transform;
        RaycastHit[] hits = Physics.RaycastAll(cameraTransform.position, cameraTransform.forward, _range);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.TryGetComponent<IDisruptable>(out IDisruptable disruptable))
                disruptable.Disrupt();
        }

        if (_audioSource != null)
            _audioSource.Play();
    }
}
