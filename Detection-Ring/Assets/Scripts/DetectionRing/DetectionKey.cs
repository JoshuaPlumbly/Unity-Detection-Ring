using System;

public class DetectionKey : IComparable<DetectionKey>
{
    public float Direction { get; private set; }
    public float Distance { get; private set; }

    public DetectionKey(float direction, float distance)
    {
        Direction = direction;
        Distance = distance;
    }

    public int CompareTo(DetectionKey other)
    {
        return this.Direction.CompareTo(other.Direction);
    }
}