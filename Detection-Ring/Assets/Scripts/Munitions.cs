using UnityEngine;

[System.Serializable]
public class Munitions
{
    [SerializeField] private int _limit;
    [SerializeField] private int _currentCount;

    public int Limit => _limit;
    public int CurrentCount => _currentCount;

    public Munitions(int limit, int currentCount)
    {
        _limit = limit;
        _currentCount = currentCount;
    }

    public bool HasAtLeast(int amount)
    {
        return _currentCount >= amount;
    }

    public bool ExtractOne()
    {
        if (_currentCount < 1)
            return false;
        
        _currentCount--;
        return true;
    }

    public int Extract(int amount)
    {
        if (!HasAtLeast(amount))
            return _currentCount;

        _currentCount -= amount;
        return amount;
    }

    public void Replenish(int amount)
    {
        _currentCount += amount;
        _currentCount = (_currentCount > _limit) ? _limit : _currentCount;
    }

    public void Fill()
    {
        _currentCount = Limit;
    }

    public void Empty()
    {
        _currentCount = 0;
    }
}