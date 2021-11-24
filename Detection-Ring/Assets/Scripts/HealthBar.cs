using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] Health _health;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _health.OnTakeDamage += UpdateHealth;
    }

    private void UpdateHealth() {
        _image.fillAmount = _health.HealthDividedByMaxHealth();
    }
}
