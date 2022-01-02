using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public virtual string UseText { get; private set; } = "Interact";

    public virtual void Use()
    {

    }
}