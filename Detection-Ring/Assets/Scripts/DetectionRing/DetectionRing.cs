using System.Collections;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(LineRenderer))]
public class DetectionRing : MonoBehaviour
{
    [SerializeField] int _nodeCount = 180;
    [SerializeField] float _detectionRadius = 20f;
    [SerializeField] float _ringRadius = 1.2f;
    [SerializeField] LayerMask _targetLayerMask;
    [SerializeField] float _frequency;
    [SerializeField] float _amplitude;
    [SerializeField] DetectionRingHeightMap _heightMap = new DetectionRingHeightMap();

    private LineRenderer _lineRederer;
    private Collider[] _colliders;

    private void Awake()
    {
        _lineRederer = GetComponent<LineRenderer>();
        _lineRederer.loop = true;

        SetNodeCount(_nodeCount);
    }

    public void Update()
    {
        _colliders = Physics.OverlapSphere(transform.position, _detectionRadius, _targetLayerMask);
        _heightMap.Clear();

        for (int i = 0; i < _colliders.Length; i++)
        {
            float direction = CalucateAngle(transform.position, _colliders[i].transform.position) / 360f;
            float distance = Vector3.Distance(transform.position, _colliders[i].transform.position);
            _heightMap.Add(direction, distance);
        }

        DrawRing();
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
            float y = _heightMap.Evauate(angleDec) + GenerateNoise(x,z);

            nodePositions[i] = transform.position + new Vector3(x,y,z);
        }

        _lineRederer.SetPositions(nodePositions);
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
}