using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxf
{
    public float Min { get; private set; } = float.MaxValue;
    public float Max { get; private set; } = float.MinValue;

    public MinMaxf(float value)
    {
        Min = float.NaN;
        Max = float.NaN;
        Expand(value);
    }

    public MinMaxf(float a, float b)
    {
        Min = float.NaN;
        Max = float.NaN;
        Expand(a);
        Expand(b);
    }

    public MinMaxf(float[] values)
    {
        for (int i = 0; i < values.Length; i++)
        {
            Expand(values[i]);
        }
    }

    public void Expand(float value)
    {
        Min = Min < value ? Min: value;
        Max = Max > value ? Max : value;
    }

    public void Expand(float[] values)
    {
        for (int i = 0; i < values.Length; i++)
            Expand(values[i]);
    }

    public float GetRange()
    {
        return Mathf.Abs(Min - Max);
    }
}