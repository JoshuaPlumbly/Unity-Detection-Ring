using System.Collections;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(LineRenderer))]
public class DetectionRing : MonoBehaviour
{
    [SerializeField] DetectionSystem _detectionSystem;
    [SerializeField] int _nodeCount = 180;
    [SerializeField] float _ringRadius = 2f;
    [SerializeField] float _bumbRadius = 0.05f;
    [SerializeField] float _maxDistance = 20f;
    [SerializeField] float _frequency = 0.2f;
    [SerializeField] float _amplitude = 0.1f;
    
    [SerializeField] AnimationCurve _diffuseCurve;
    [SerializeField] AnimationCurve _distatanceDropOffCurve;
    [SerializeField] AnimationCurve _realValue;

    private LineRenderer _lineRederer;
    private SortedList<DetectionKey> _detectionKeys;

    private void Awake()
    {
        _lineRederer = GetComponent<LineRenderer>();
        _lineRederer.loop = true;

        SetDetectionSystem(_detectionSystem);
        SetNodeCount(_nodeCount);
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

    public void SetNodeCount(int value)
    {
        _nodeCount = value;
        _lineRederer.positionCount = value;
    }

    public void DrawRing()
    {
        var nodePositions = new Vector3[_lineRederer.positionCount];
        var anglePerNode = 360f / _lineRederer.positionCount;

        for (int i = 0; i < _lineRederer.positionCount; i++)
        {
            float angle = anglePerNode * i;
            float angleDec = angle / 360f;

            float x = Mathf.Sin(angle * Mathf.PI / 180) * _ringRadius;
            float z = Mathf.Cos(angle * Mathf.PI / 180) * _ringRadius;
            float y = Evaluate(angleDec) + GenerateNoise(x,z);

            nodePositions[i] = transform.position + new Vector3(x,y,z);
        }

        _lineRederer.SetPositions(nodePositions);
    }

    public float Evaluate(float t)
    {
        float height = 0f;

        for (int i = 0; i < _detectionKeys.Count; i++)
        {
            float directionDistance = WrapShortistDistance(t, _detectionKeys[i].Direction, 1f);

            if (directionDistance > _bumbRadius)
                continue;

            float h = _diffuseCurve.Evaluate(directionDistance / _bumbRadius);
            h *= _distatanceDropOffCurve.Evaluate(_detectionKeys[i].Distance / _maxDistance);
            height += h;
        }

        return height;
    }


    public float GenerateNoise(float x, float y)
    {
        x = (x * _amplitude) + Time.time;
        y = (y * _amplitude) + Time.time;

        return Mathf.PerlinNoise(x, y) * _frequency;
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