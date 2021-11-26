using UnityEngine;

[System.Serializable]
public class ResourceTracker
{
    [SerializeField] float _amount;
    [SerializeField] float _maximumAmount;

    float AmountDividedByMaxAmount() => _amount / _maximumAmount;

    public ResourceTracker(float startAmount, float maximumAmount)
    {
        _amount = startAmount;
        _maximumAmount = maximumAmount;
    }

    public bool TryToUse(float useAmount)
    {
        if (_amount > 0f)
        {
            _amount -= useAmount;
            _amount = (_amount < 0f) ? 0f : _amount;
            return true;
        }

        return false;
    }

    public void Replenish(float replenishAmount)
    {
        _amount += replenishAmount;
        _amount = (_amount > _maximumAmount) ? _maximumAmount : _amount;
    }
}