using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disrupter : MonoBehaviour, IHandheldItem
{
    [SerializeField] int _loadedAmunitions = 30;
    [SerializeField] int _unloadedMunitions = 60;
    [SerializeField] int _loadedCapasity = 30;
    [SerializeField] float _fireRate = 0.1f;
    [SerializeField] float _canFireAfter = float.NegativeInfinity;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _range = 500f;

    private MunitionsUI _munitionsUI;

    private void Awake()
    {
        if (_audioSource != null)
            _audioSource.loop = false;
    }

    private void OnEnable()
    {
        _munitionsUI = FindObjectOfType<MunitionsUI>();


        if (_munitionsUI != null)
        {
            _munitionsUI.SetLoadedMunitionText(_loadedAmunitions.ToString());
            _munitionsUI.SetUnloadedMunitionText(_unloadedMunitions.ToString());
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
        bool canFire = _loadedAmunitions > 0 && Time.time > _canFireAfter;

        if (canFire == false)
            return false;

        _canFireAfter = Time.time + _fireRate;
        _loadedAmunitions--;
        return true;
    }
    
    private void UpdateLoadedMunitionsUI()
    {
        if (_munitionsUI != null)
            _munitionsUI.SetLoadedMunitionText(_loadedAmunitions.ToString());
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