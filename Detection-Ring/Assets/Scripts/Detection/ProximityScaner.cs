using UnityEngine;

[System.Serializable]
public class ProximityScaner
{
    [SerializeField, Range(0f, 1f)] float _blendRadiusMin = 0.1f;
    [SerializeField, Range(0f, 1f)] float _blendRadiusMax = 0.06f;
    [SerializeField] AnimationCurve _curve;

    public const float Tau = 6.2831853071796f;
    public const float RevPerRad = 0.1591549430919f;

    public float[] Perform(Vector3 origin, int segments, float radius, LayerMask layerMask)
    {
        Collider[] colliders = Physics.OverlapSphere(origin, radius, layerMask, QueryTriggerInteraction.Collide);
        float[] evaluation = new float[segments];
        float maxDistanceSqr = radius * radius;

        for (int i = 0; i < colliders.Length; i++)
        {
            Vector3 p = colliders[i].ClosestPoint(origin);
            float angle = ProjectedAngleFullTurns(origin, p);
            float distance = (origin - p).sqrMagnitude;

            int index = Mathf.RoundToInt(segments * angle);
            float strength = Mathf.InverseLerp(maxDistanceSqr, 0f, distance);
            
            int nodesToBlend = Mathf.RoundToInt(Mathf.Lerp(_blendRadiusMin, _blendRadiusMax, strength) * segments);

            for (int j = -nodesToBlend; j < nodesToBlend; j++)
            {
                int blendIndex = ((index + j % segments) + segments) % segments;
                float blendStrength = Mathf.Lerp(strength, 0f, _curve.Evaluate((float)Mathf.Abs(j) / nodesToBlend));
                evaluation[blendIndex] += blendStrength;
            }
        }

        return evaluation;
    }

    public static float ProjectedAngleRadians(Vector3 from, Vector3 to)
    {
        float x = to.x - from.x;
        float z = to.z - from.z;

        return (Mathf.Atan2(x, z) + Tau) % Tau;
    }

    public static float ProjectedAngleFullTurns(Vector3 from, Vector3 to)
    {
        return ProjectedAngleRadians(from, to) * RevPerRad;
    }
}
