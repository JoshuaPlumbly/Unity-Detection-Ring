using UnityEngine;

public class PatternGenerator
{
    public const float tau = (float)Mathf.PI * 2f;

    public static Vector3[] Ring(int verticesCount, float radius)
    {
        Vector3[] vertices = new Vector3[verticesCount];
        float radiansPerVertex = (1f / verticesCount) * tau;

        for (int i = 0; i < verticesCount; i++)
        {
            float angle = radiansPerVertex * i;
            vertices[i] = new Vector3(Mathf.Sin(angle) * radius, 0f, Mathf.Cos(angle) * radius);
        }

        return vertices;
    }
}