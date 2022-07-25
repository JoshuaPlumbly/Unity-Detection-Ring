using System;
using UnityEngine;

public static class CameraManager
{
    public static event Action<Camera> SwitchedCamera;

    public static void SwithCamera(Camera newCamera)
    {
        SwitchedCamera?.Invoke(newCamera);
    }

    public static Vector3 GetCameraForward(Camera camera)
    {
        Vector3 forwrd = camera.transform.forward;
        forwrd.y = 0f;
        return forwrd.normalized;
    }

    public static Vector3 GetCameraRight(Camera camera)
    {
        Vector3 right = camera.transform.right;
        right.y = 0f;
        return right.normalized;
    }
}