using UnityEngine;

public class ProximityDetetor : MonoBehaviour
{
    [SerializeField] float _detectionRadius = 20f;
    [SerializeField] LayerMask _targetLayerMask;

    public Collider[] DetectedObjects { get; private set; }
    public SortedList<DetectionKey> DetectionKeys { get; private set; } = new SortedList<DetectionKey>();
    public float DetectionRadius => _detectionRadius;

    public void Update()
    {
        DetectedObjects = Physics.OverlapSphere(transform.position, _detectionRadius, _targetLayerMask);
        DetectionKeys.Clear();

        for (int i = 0; i < DetectedObjects.Length; i++)
        {
            float direction = CalucateAngle(transform.position, DetectedObjects[i].transform.position) / 360f;
            float distance = Vector3.Distance(transform.position, DetectedObjects[i].transform.position);
            DetectionKeys.Add(new DetectionKey(direction, distance));
        }
    }

    public static float CalucateAngle(Vector3 to, Vector3 from)
    {
        float x = from.x - to.x;
        float y = from.z - to.z;
        float angle = Mathf.Atan2(x, y) * Mathf.Rad2Deg;
        return Mathf.Sign(angle) < 0 ? 360f + angle : angle;
    }
}
