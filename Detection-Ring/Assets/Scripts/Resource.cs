using UnityEngine;

[System.Serializable]
public class Resource
{
    [SerializeField] private int _currentValue;
    [SerializeField] private int _maximumValue;

    public Resource(int currentValue, int maximumValue)
    {
        _currentValue = currentValue;
        _maximumValue = maximumValue;
    }

    public float CurrentValue => _currentValue;
    public float MaximumValue => _maximumValue;

    public float CurrentCapacityNormalize()
    {
        if (_maximumValue == 0)
            return 0f;

        return (float)_currentValue / _maximumValue;
    }

    public bool IsEmpty()
    {
        return _currentValue == 0f;
    }

    public bool IsFull()
    {
        return _currentValue == _maximumValue;
    }

    public bool HasAtLeast(int value)
    {
        return _currentValue >= value;
    }

    public void SetToEmpty()
    {
        _currentValue = 0;
    }

    public void SetToFull()
    {
        _currentValue = _maximumValue;
    }

    public void SetTo(int value)
    {
        _currentValue = Mathf.Clamp(value, 0, _maximumValue);
    }

    public void SetMaximumTo(int value)
    {
        _maximumValue = value;
        _currentValue = Mathf.Clamp(_currentValue, 0, _maximumValue);
    }

    public void Add(int value)
    {
        _currentValue += value;
        _currentValue = Mathf.Min(_currentValue, _maximumValue);
    }

    public void Subtract(int value)
    {
        _currentValue -= value;
        _currentValue = Mathf.Max(_currentValue, 0);
    }

    public bool TryToSpend(int value)
    {
        if (_currentValue < value)
            return false;

        Subtract(value);
        return true;
    }

    public int Withdraw(int value)
    {
        value = Mathf.Min(value, _currentValue);
        Subtract(value);
        return value;
    }
}