using UnityEngine;

public static class ProximityDetector
{
    public static DetectionKey[] SeachProximity(Vector3 origin, float radius, LayerMask layerMask)
    {
        var detectedObjects = Physics.OverlapSphere(origin,radius,layerMask);
        var sortedList = new SortedList<DetectionKey>();

        for (int i = 0; i < detectedObjects.Length; i++)
        {
            float direction = CalucateAngle(origin, detectedObjects[i].transform.position) / 360f;
            float distance = Vector3.Distance(origin, detectedObjects[i].transform.position);
            sortedList.Add(new DetectionKey(direction, distance));
        }

        return sortedList.ToArray();
    }

    public static float CalucateAngle(Vector3 to, Vector3 from)
    {
        float x = from.x - to.x;
        float y = from.z - to.z;
        float angle = Mathf.Atan2(x, y) * Mathf.Rad2Deg;
        return Mathf.Sign(angle) < 0 ? 360f + angle : angle;
    }
}