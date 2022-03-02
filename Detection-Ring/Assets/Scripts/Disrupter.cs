using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disrupter : MonoBehaviour, IHandheldItem
{
    [SerializeField] private Munitions _munitions;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _range = 500f;
    [SerializeField] private float _fireRate = 0.1f;

    private float _canFireAfter = 0f;

    private void Awake()
    {
        if (_audioSource != null)
            _audioSource.loop = false;
    }

    public void Use()
    {
        if (!CanFire())
            return;

        _munitions.ExtractOne();
        _canFireAfter = Time.time + _fireRate;


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

    public bool CanFire()
    {
        return _canFireAfter < Time.time && _munitions.HasAtLeast(1);
    }

    public void PlaySound(AudioClip audioClip, float volumeScale = 1f)
    {
        if (audioClip != null && _audioSource != null)
            _audioSource.PlayOneShot(audioClip, volumeScale);
    }

    public GameObject Prefab()
    {
        return gameObject;
    }
}
