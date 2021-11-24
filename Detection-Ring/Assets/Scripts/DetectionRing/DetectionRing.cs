using System.Collections;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(LineRenderer))]
public class DetectionRing : MonoBehaviour
{
    [SerializeField] DetectionSystem _detectionSystem;
    [SerializeField] int _ringPositionCount = 180;
    [SerializeField] float _ringRadius = 2f;
    [SerializeField] float _amplitude = 1f;
    [SerializeField] float _bumbRadius = 0.05f;
    [SerializeField] float _maxDistance = 20f;
    [SerializeField] float _noiseFrequency = 0.2f;
    [SerializeField] float _noiseAmplitude = 0.1f;
    
    [SerializeField] AnimationCurve _diffuseCurve;
    [SerializeField] AnimationCurve _distatanceDropOffCurve;

    private LineRenderer _lineRederer;
    private SortedList<DetectionKey> _detectionKeys;

    private Vector3[] _flattedRingPositions;

    private void Awake()
    {
        _lineRederer = GetComponent<LineRenderer>();
        _lineRederer.loop = true;

        SetDetectionSystem(_detectionSystem);
        SetRingPositionCount(_ringPositionCount);
    }

    public void LateUpdate()
    {
        DrawRing();
    }

    public void SetDetectionSystem(DetectionSystem detectionSystem)
    {
        _detectionSystem = detectionSystem;
        _detectionKeys = detectionSystem.DetectionKeys;
        _maxDistance = _detectionSystem.DetectionRadius;
    }

    public void SetRingPositionCount(int positionCount)
    {
        _ringPositionCount = positionCount;
        _lineRederer.positionCount = positionCount;
        _flattedRingPositions = new Vector3[positionCount];

        var anglePerNode = 360f / _lineRederer.positionCount;

        for (int i = 0; i < _flattedRingPositions.Length; i++)
        {
            float angle = anglePerNode * i;
            float x = Mathf.Sin(angle * Mathf.PI / 180) * _ringRadius;
            float z = Mathf.Cos(angle * Mathf.PI / 180) * _ringRadius;

            _flattedRingPositions[i] = new Vector3(x, 0f, z);
        }
    }

    public void DrawRing()
    {
        _lineRederer.positionCount = _flattedRingPositions.Length;

        var anglePerNode = 360f / _lineRederer.positionCount;
        var gradent = EvaluateArray(_flattedRingPositions.Length);
        Vector3[] ringPositions = new Vector3[_flattedRingPositions.Length];

        for (int i = 0; i < _flattedRingPositions.Length; i++)
        {
            var vector = _flattedRingPositions[i];
            vector.y = gradent[i];
            
            vector += Vector3.up * GenerateNoise(vector.x, vector.z) * _noiseAmplitude;

            ringPositions[i] = transform.position + vector;
        }

        _lineRederer.SetPositions(ringPositions);
    }

    public float[] EvaluateArray(int length)
    {
        float[] result = new float[length];
        MinMaxf minMaxf = new MinMaxf(0f, 1f);

        int elementsPerDeffuse = Mathf.RoundToInt(length * _bumbRadius);
        float anglePerElement = 1f / length;

        for (int i = 0; i < _detectionKeys.Count; i++)
        {
            int closest = Mathf.RoundToInt(length * _detectionKeys[i].Direction);

            for (int j = -elementsPerDeffuse; j < elementsPerDeffuse; j++)
            {
                int index = Wrap(closest + j, length);
                float currentAngle = anglePerElement * index;
                float directionDistance = WrapShortistDistance(currentAngle, _detectionKeys[i].Direction, 1f);

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

    public float GenerateNoise(float x, float y)
    {
        x = (x * _noiseAmplitude) + Time.time;
        y = (y * _noiseAmplitude) + Time.time;

        return Mathf.PerlinNoise(x, y) * _noiseFrequency;
    }

    public static float CalucateAngle(Vector3 to, Vector3 from)
    {
        float x = from.x - to.x;
        float y = from.z - to.z;
        float angle = Mathf.Atan2(x, y) * Mathf.Rad2Deg;
        return Mathf.Sign(angle) < 0 ? 360f + angle : angle;
    }

    public static int Wrap(int i, int n)
    {
        return ((i % n) + n) % n;
    }

    public static float Wrap(float i, float n)
    {
        return ((i % n) + n) % n;
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