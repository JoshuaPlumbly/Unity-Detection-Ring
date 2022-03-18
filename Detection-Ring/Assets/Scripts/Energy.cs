using UnityEngine;

[System.Serializable]
public class Energy
{
    [SerializeField] float _currentCapacity;
    [SerializeField] float _maximumCapacity;

    public Energy(float currentCapacity, float maximumCapacity)
    {
        _currentCapacity = currentCapacity;
        _maximumCapacity = maximumCapacity;
    }

    public float CurrentCapacity => _currentCapacity;
    public float MaximumAmount => _maximumCapacity;

    public float CurrentCapacityNormalize() => _currentCapacity / _maximumCapacity;

    public void Replenish(float energyToReplenish)
    {
        _currentCapacity += energyToReplenish;
        _currentCapacity = (_currentCapacity > _maximumCapacity) ? _maximumCapacity : _currentCapacity;
    }

    public bool Consume(float energyToConsume)
    {
        if (_currentCapacity < energyToConsume)
            return false;

        Drain(energyToConsume);
        return true;
    }

    public float Drain(float energyToDrain)
    {
        float eneryUsed = Mathf.Min(energyToDrain, _currentCapacity);
        _currentCapacity = Mathf.Max(_currentCapacity - energyToDrain, 0f);
        return eneryUsed;
    }
}