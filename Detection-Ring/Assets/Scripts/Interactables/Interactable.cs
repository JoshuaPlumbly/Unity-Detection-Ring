using UnityEngine;

public abstract class Interactable : MonoBehaviour, IState
{
    public bool Ignore = false;
    public abstract void OnEnter(GameObject subject);
    public abstract void OnExit(GameObject subject);
    public abstract void OnUpdate(GameObject subject);
}
