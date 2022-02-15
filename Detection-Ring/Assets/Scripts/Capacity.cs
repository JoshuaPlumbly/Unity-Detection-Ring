using UnityEngine;

[System.Serializable]
public class Capacity
{
    [SerializeField] float _amount;
    [SerializeField] float _maximumAmount;

    public float Amount => _amount;
    public float MaximumAmount => _maximumAmount;

    public float CurrentOverMaximumValue() => _amount / _maximumAmount;

    public Capacity(float amount, float maximumAmount)
    {
        _amount = amount;
        _maximumAmount = maximumAmount;
    }

    public float Extract(float request)
    {
        float result = Mathf.Min(request, _amount);
        _amount = Mathf.Max(_amount - request, 0f);
        return result;
    }

    public void Replenish(float replenishAmount)
    {
        _amount += replenishAmount;
        _amount = (_amount > _maximumAmount) ? _maximumAmount : _amount;
    }
}