using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DetectionRingHeightMap : SortedList<DetectionKey> 
{ 
    public AnimationCurve diffuseCurve;
    public AnimationCurve distatanceDropOffCurve;
    public float maxDistance = 40f;
    public float length = 4f;
    public float maxHeight = 0.2f;

    public void Add(float direction, float distance)
    {
        Add(new DetectionKey(direction, distance));
    }

    public float Evauate(float t)
    {
        float height = 0f;

        for (int i = 0; i < Items.Count; i++)
        {
            float directionDistance = WrapShortistDistance(t, Items[i].Direction, 1f);

            if (directionDistance > length)
                continue;

            float h = diffuseCurve.Evaluate(directionDistance / length);
            h *= distatanceDropOffCurve.Evaluate(Items[i].Distance / maxDistance);
            height += h;
        }

        return height;
    }

    public static float Wrap(float t, float n)
    {
        return ((t % n) + n) % n;
    }

    public static float WrapShortistDistance(float a, float b, float n)
    {
        a = Wrap(a, n);
        b = Wrap(b, n);
        float max = Mathf.Max(a, b);
        float min = Mathf.Min(a, b);
        float dist = max - min;

        return Mathf.Min(dist, n - dist);
    }
}