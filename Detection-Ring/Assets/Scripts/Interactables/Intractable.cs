using UnityEngine;

public abstract class Intractable : MonoBehaviour
{
    public bool Ignore = false;
    public abstract string IntractableText { get; }

    public abstract void OnInteractDown(GameObject subject);
    public abstract void OnInteractUp(GameObject subject);
    public abstract void OnInteract(GameObject subject);
}