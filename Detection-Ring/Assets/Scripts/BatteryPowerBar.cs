using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BatteryPowerBar : MonoBehaviour
{
    [SerializeField] ProximitySensorB _health;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _health.OnChanageInBattery += ChanageFillAmount;
    }

    private void ChanageFillAmount(float newFillAmount)
    {
        _image.fillAmount = newFillAmount;
    }
}