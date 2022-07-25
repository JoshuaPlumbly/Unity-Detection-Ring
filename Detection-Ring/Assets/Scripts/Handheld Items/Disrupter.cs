using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disrupter : MonoBehaviour, HandheldImplement
{
    [SerializeField] Resource _loadedAmmunition = new Resource(08, 08);
    [SerializeField] Resource _unloadedAmmunition = new Resource(0, int.MaxValue);
    [SerializeField] float _fireRate = 0.1f;
    [SerializeField] float _canFireAfter = float.NegativeInfinity;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _range = 500f;

    private MunitionsUI _ammunition;

    private void Awake()
    {
        if (_audioSource != null)
            _audioSource.loop = false;
    }

    private void OnEnable()
    {
        _ammunition = FindObjectOfType<MunitionsUI>();


        if (_ammunition != null)
        {
            _ammunition.SetLoadedMunitionText(_loadedAmmunition.CurrentValue.ToString());
            _ammunition.SetUnloadedMunitionText(_unloadedAmmunition.CurrentValue.ToString());
        }
    }

    public void Use()
    {
        if (!AttemptToFire())
            return;

        UpdateLoadedMunitionsUI();
        DisruptDevicesBeingPointedAt();
        PlayFireSoundEffect();
    }

    public bool AttemptToFire()
    {
        bool canFire = !_loadedAmmunition.IsEmpty() && Time.time > _canFireAfter;

        if (canFire == false)
            return false;

        _canFireAfter = Time.time + _fireRate;
        _loadedAmmunition.Subtract(1);
        return true;
    }
    
    private void UpdateLoadedMunitionsUI()
    {
        if (_ammunition != null)
            _ammunition.SetLoadedMunitionText(_loadedAmmunition.CurrentValue.ToString());
    }

    private void DisruptDevicesBeingPointedAt()
    {
        Transform cameraTransform = Camera.main.transform;
        RaycastHit[] hits = Physics.RaycastAll(cameraTransform.position, cameraTransform.forward, _range);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.TryGetComponent<IDisruptable>(out IDisruptable disruptable))
                disruptable.Disrupt();
        }
    }

    private void PlayFireSoundEffect()
    {
        if (_audioSource != null)
            _audioSource.Play();
    }

    public void PlaySound(AudioClip audioClip, float volumeScale = 1f)
    {
        if (audioClip != null && _audioSource != null)
            _audioSource.PlayOneShot(audioClip, volumeScale);
    }
}