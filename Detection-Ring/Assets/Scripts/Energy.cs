using UnityEngine;

[System.Serializable]
public class Energy
{
    [SerializeField] float _limit;
    [SerializeField] float _currentAmount;

    public float Amount => _currentAmount;
    public float MaximumAmount => _limit;

    public float CurrentOverMaximumValue() => _currentAmount / _limit;

    public Energy(float amount, float maximumAmount)
    {
        _currentAmount = amount;
        _limit = maximumAmount;
    }

    public float Extract(float request)
    {
        float result = Mathf.Min(request, _currentAmount);
        _currentAmount = Mathf.Max(_currentAmount - request, 0f);
        return result;
    }

    public void Replenish(float replenishAmount)
    {
        _currentAmount += replenishAmount;
        _currentAmount = (_currentAmount > _limit) ? _limit : _currentAmount;
    }
}