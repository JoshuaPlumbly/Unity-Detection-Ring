using UnityEngine;

public static class CameraManager
{
    public static IGameCamera CurrentInGameCamera { get; private set; }

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

    public static void SwithCamera(IGameCamera newInGameCamera)
    {
        if (CurrentInGameCamera != null)
            CurrentInGameCamera.OnSwitchedAwayFrom();

        CurrentInGameCamera = newInGameCamera;
        CurrentInGameCamera.OnSwitchedTo();
    }
}