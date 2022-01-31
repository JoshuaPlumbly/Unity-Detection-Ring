using UnityEngine;

public static class CameraManager
{
    public static InGameCamera CurrentInGameCamera { get; private set; }

    public static Camera Current
    {
        get
        {
            if (_current == null)
                _current = Camera.main;

            return _current;
        }
    }

    private static Camera _current = null;

    public static void SwithCamera(InGameCamera newInGameCamera)
    {
        if (CurrentInGameCamera != null)
            CurrentInGameCamera.OnSwitchedAwayFrom();

        CurrentInGameCamera = newInGameCamera;
        CurrentInGameCamera.OnSwitchedTo();
        _current = newInGameCamera.GameCamera;
    }
}