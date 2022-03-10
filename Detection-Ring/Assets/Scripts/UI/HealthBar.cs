using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image _healthBarImage;
    [SerializeField] Image _damgeEffectImage;
    [SerializeField] Health _health;
    [SerializeField] float _damageLerpDelay = 0.1f;
    [SerializeField] float _damageEffectDuration = 0.2f;

    private float _healthScaled;
    private IEnumerator _damageEffectEnumerator;

    private void OnEnable()
    {
        _healthScaled = _health.HealthDividedByMaxHealth();
        SetHealthBar(_healthScaled);

        _health.OnTakeDamage += OnTakeDamage;
    }

    private void OnDisable()
    {
        _health.OnTakeDamage -= OnTakeDamage;
    }

    private void OnTakeDamage()
    {
        _healthScaled = _health.HealthDividedByMaxHealth();
        SetHealthBar(_healthScaled);

        if (_damageEffectEnumerator != null)
            StopCoroutine(_damageEffectEnumerator);

        _damageEffectEnumerator = DamageEffect();
        StartCoroutine(_damageEffectEnumerator);
    }

    private void SetHealthBar(float scale)
    {
        _healthBarImage.fillAmount = scale;
    }

    private IEnumerator DamageEffect()
    {
        yield return new WaitForSeconds(_damageLerpDelay);

        float timeElapsed = 0f;
        float prevousHealhScale = _damgeEffectImage.fillAmount;

        while (timeElapsed < _damageEffectDuration)
        {
            _damgeEffectImage.fillAmount = Mathf.Lerp(prevousHealhScale, _healthScaled, timeElapsed / _damageEffectDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        _damgeEffectImage.fillAmount = _healthScaled;
    }

}