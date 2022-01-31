using UnityEngine;

public abstract class InGameCamera : MonoBehaviour
{
    public Camera GameCamera;

    protected virtual void Awake()
    {
        GameCamera = GetComponent<Camera>();
    }

    public virtual void OnSwitchedTo()
    {
        
    }

    public virtual void OnSwitchedAwayFrom()
    {

    }
}