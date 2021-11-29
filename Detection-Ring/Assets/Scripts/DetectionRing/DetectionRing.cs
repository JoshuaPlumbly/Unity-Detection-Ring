using System.Collections;
using UnityEngine;
using UnityEditor;
using System;

public class DetectionRing : MonoBehaviour
{
    [SerializeField] ResourceTracker _battery= new ResourceTracker(100f,100f);
    [SerializeField] float _batteryLossPerSec;
    [SerializeField] LayerMask _layerMask;
    [SerializeField] bool _isActive;
    [SerializeField] int _ringPositionCount = 180;
    [SerializeField] float _amplitude = 1f;
    [SerializeField] float _bumbRadius = 0.05f;
    [SerializeField] float _maxDistance = 20f;
    [SerializeField] float _noiseAmplitude = 0.2f;
    [SerializeField] float _noiseFrequency = 0.2f;

    [SerializeField] AnimationCurve _diffuseCurve;
    [SerializeField] AnimationCurve _distatanceDropOffCurve;

    private Vector3[] _ringPositions;

    public event Action<float[]> OnSegmentRepositioned;
    public event Action<bool> OnActiveStateChanaged;

    public int SegmentCount = 180;
    const float doublePI = 6.28318530718f;

    private void Awake()
    {
        _ringPositions = new Vector3[SegmentCount];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SetActiveState(!_isActive);
        }

        if (!_isActive || !_battery.TryToUse(_batteryLossPerSec * Time.deltaTime))
            SetActiveState(false);
    }

    public void SetActiveState(bool isActive)
    {
        _isActive = isActive;
        OnActiveStateChanaged?.Invoke(isActive);
    }

    public void LateUpdate()
    {
        if (!_isActive)
            return;

        Draw();
    }

    public void Draw()
    {
        float[] heightArray = EvaluateArray(SegmentCount);
        float radiusPerSegment = doublePI / SegmentCount;

        for (int i = 0; i < SegmentCount; i++)
        {
            heightArray[i] += GenerateNoise(radiusPerSegment * i);
        }

        OnSegmentRepositioned(heightArray);
    }

    public float GenerateNoise(float a)
    {
        float xOff = Mathf.Cos(a) + 1f;
        float yOff = Mathf.Sin(a) + 1f;
        xOff = (xOff * _noiseFrequency) + Time.time;
        yOff = (yOff * _noiseFrequency) + Time.time;

        return Mathf.PerlinNoise(xOff, yOff) * _noiseAmplitude;
    }

    public float[] EvaluateArray(int length)
    {
        float[] result = new float[length];
        MinMaxf minMaxf = new MinMaxf(0f, 1f);

        int elementsPerDeffuse = Mathf.RoundToInt(length * _bumbRadius);
        float anglePerElement = 1f / length;

        DetectionKey[] _detectionKeys = ProximityDetection.SeachProximity(transform.position, _maxDistance, _layerMask);

        for (int i = 0; i < _detectionKeys.Length; i++)
        {
            int closest = Mathf.RoundToInt(length * _detectionKeys[i].Direction);

            for (int j = -elementsPerDeffuse; j < elementsPerDeffuse; j++)
            {
                int index = Wrap(closest + j, length);
                float currentAngle = anglePerElement * index;
                float directionDistance = FindShortistDistanceInCircuit(currentAngle, _detectionKeys[i].Direction, 1f);

                float height = _diffuseCurve.Evaluate(directionDistance / _bumbRadius);
                height *= _distatanceDropOffCurve.Evaluate(_detectionKeys[i].Distance / _maxDistance);
                result[index] += height;
                minMaxf.Expand(result[index]);
            }
        }

        float scale = 1f / minMaxf.GetRange();

        for (int i = 0; i < length; i++)
        {
            result[i] *= scale;
        }

        return result;
    }

    public static int Wrap(int i, int n)
    {
        return ((i % n) + n) % n;
    }

    public static float Wrap(float i, float n)
    {
        return ((i % n) + n) % n;
    }

    public static float FindShortistDistanceInCircuit(float a, float b, float n)
    {
        a = Wrap(a, n);
        b = Wrap(b, n);
        float max = Mathf.Max(a, b);
        float min = Mathf.Min(a, b);
        float dist = max - min;

        return Mathf.Min(dist, n - dist);
    }
}