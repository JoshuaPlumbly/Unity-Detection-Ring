using System.Collections;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(LineRenderer), typeof(ProximityDetetor))]
public class DetectionRing : MonoBehaviour
{
    [SerializeField] ProximityDetetor _detectionSystem;
    [SerializeField] ResourceTracker _battery= new ResourceTracker(100f,100f);
    [SerializeField] float _batteryLossPerSec;
    [SerializeField] bool _on;
    [SerializeField] int _ringPositionCount = 180;
    [SerializeField] float _lerpSpeed = 5f;
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

    private Vector3[] _ringPositions;

    private void Awake()
    {
        _lineRederer = GetComponent<LineRenderer>();
        _lineRederer.loop = true;

        SetDetectionSystem(GetComponent<ProximityDetetor>());
        SetRingPositionCount(_ringPositionCount);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (_on)
                TurnOff();
            else
                TurnOn();
        }

        if (!_on || !_battery.TryToUse(_batteryLossPerSec * Time.deltaTime))
            TurnOff();
    }

    public void TurnOff()
    {
        _on = false;
        _detectionSystem.enabled = false;
        _lineRederer.enabled = false;

        for (int i = 0; i < _ringPositions.Length; i++)
        {
            _ringPositions[i].y = 0f;
        }
    }

    public void TurnOn()
    {
        _on = true;
        _detectionSystem.enabled = true;
        _lineRederer.enabled = true;
    }

    public void LateUpdate()
    {
        if (!_on)
            return;

        DrawRing();
    }

    public void SetDetectionSystem(ProximityDetetor detectionSystem)
    {
        _detectionSystem = detectionSystem;
        _detectionKeys = detectionSystem.DetectionKeys;
        _maxDistance = _detectionSystem.DetectionRadius;
    }

    public void SetRingPositionCount(int positionCount)
    {
        _ringPositionCount = positionCount;
        _lineRederer.positionCount = positionCount;
        _ringPositions = new Vector3[positionCount];

        var anglePerNode = 360f / _lineRederer.positionCount;

        for (int i = 0; i < _ringPositions.Length; i++)
        {
            float angle = anglePerNode * i;
            float x = Mathf.Sin(angle * Mathf.PI / 180) * _ringRadius;
            float z = Mathf.Cos(angle * Mathf.PI / 180) * _ringRadius;

            _ringPositions[i] = new Vector3(x, 0f, z);
        }
    }

    public void DrawRing()
    {
        _lineRederer.positionCount = _ringPositions.Length;

        var anglePerNode = 360f / _lineRederer.positionCount;
        var gradent = EvaluateArray(_ringPositions.Length);
        var ringPositions = new Vector3[_ringPositions.Length];

        for (int i = 0; i < _ringPositions.Length; i++)
        {
            var targetHeight = gradent[i];
            
            targetHeight += GenerateNoise(_ringPositions[i].x, _ringPositions[i].z) * _noiseAmplitude;

            _ringPositions[i].y =  Mathf.Lerp(_ringPositions[i].y, targetHeight, _lerpSpeed * Time.deltaTime);
            ringPositions[i] = _ringPositions[i] + transform.position;
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