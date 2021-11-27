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

    public static DetectionKey ShortestDistance(DetectionKey[] detectionKey)
    { 
        DetectionKey result = null;
        float minDistance = float.PositiveInfinity;

        for (int i = 0; i < detectionKey.Length; i++)
        {
            if (minDistance > detectionKey[i].Distance)
            {
                result = detectionKey[i];
                minDistance = detectionKey[i].Distance;
            }
        }

        return result;
    }

    public static DetectionKey LargestDistance(DetectionKey[] detectionKey)
    {
        DetectionKey result = null;
        float maxDistance = float.NegativeInfinity;

        for (int i = 0; i < detectionKey.Length; i++)
        {
            if (maxDistance < detectionKey[i].Distance)
            {
                result = detectionKey[i];
                maxDistance = detectionKey[i].Distance;
            }
        }

        return result;
    }
}