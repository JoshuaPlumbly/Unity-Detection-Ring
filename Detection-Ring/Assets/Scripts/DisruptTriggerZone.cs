using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DisruptTriggerZone : MonoBehaviour, IDisruptable
{
    [SerializeField] private Collider _triggerZone;
    [SerializeField] private float _disruptDuration = 7f;
    [SerializeField] private float _restoreTime = 0.8f;

    private IEnumerator _disruptCoroutine;

    public void Disrupt()
    {
        if (_disruptCoroutine != null)
            StopCoroutine(_disruptCoroutine);

        _disruptCoroutine = DisruptTime(_disruptDuration);
        StartCoroutine(_disruptCoroutine);
    }

    private IEnumerator DisruptTime(float time)
    {
        _triggerZone.enabled = false;
        AudioSource audioSource = GetComponent<AudioSource>();

        if (!audioSource.isPlaying)
            audioSource.Play();

        yield return new WaitForSeconds(time);
        audioSource.Stop();

        yield return new WaitForSeconds(_restoreTime);
        _triggerZone.enabled = true;

    }
}
