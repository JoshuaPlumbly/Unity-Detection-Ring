using UnityEngine;

public interface IState
{
    public void OnEnter(GameObject subject);
    public void OnExit(GameObject subject);
    public void OnUpdate(GameObject subject);
}