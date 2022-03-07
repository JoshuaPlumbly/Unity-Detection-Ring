public class Smallest<T>
{
    private T _smallestItem;
    private float _smallestValue;

    public Smallest(T defult, float minAngle = float.PositiveInfinity)
    {
        _smallestItem = defult;
        _smallestValue = minAngle;
    }
    
    public T GetItem() => _smallestItem;
    
    public float GetLowiestValue() => _smallestValue;

    public void Add(T item, float value)
    {
        if (value > _smallestValue)
            return;

        _smallestItem = item;
        _smallestValue = value;
    }
}