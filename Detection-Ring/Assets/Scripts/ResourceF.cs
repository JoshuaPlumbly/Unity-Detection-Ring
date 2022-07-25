using UnityEngine;

[System.Serializable]
public class ResourceF
{
    [SerializeField] float _currentValue;
    [SerializeField] float _maximumValue;

    public ResourceF(float currentValue, float maximumValue)
    {
        _currentValue = currentValue;
        _maximumValue = maximumValue;
    }

    public float CurrentValue => _currentValue;
    public float MaximumValue => _maximumValue;

    public float CurrentCapacityNormalize()
    {
        if (_maximumValue == 0f)
            return 0f;

        return _currentValue / _maximumValue;
    }

    public bool IsEmpty()
    {
        return _currentValue == 0f;
    }

    public bool IsFull()
    {
        return _currentValue == _maximumValue;
    }

    public bool HasAtLeast(float value)
    {
        return _currentValue >= value;
    }

    public void SetToEmpty()
    {
        _currentValue = 0f;
    }

    public void SetToFull()
    {
        _currentValue = _maximumValue;
    }

    public void SetTo(float value)
    {
        _currentValue = Mathf.Clamp(value, 0f, _maximumValue);
    }

    public void SetMaximumTo(float value)
    {
        _maximumValue = value;
        _currentValue = Mathf.Clamp(_currentValue, 0f, _maximumValue);
    }

    public void Add(float value)
    {
        _currentValue += value;
        _currentValue = Mathf.Min(_currentValue, _maximumValue);
    }

    public void Subtract(float value)
    {
        _currentValue -= value;
        _currentValue = Mathf.Max(_currentValue, 0f);
    }

    public bool TryToSpend(float value)
    {
        if (_currentValue < value)
            return false;

        Subtract(value);
        return true;
    }

    public float Withdraw(float value)
    {
        value = Mathf.Min(value, _currentValue);
        Subtract(value);
        return value;
    }
}