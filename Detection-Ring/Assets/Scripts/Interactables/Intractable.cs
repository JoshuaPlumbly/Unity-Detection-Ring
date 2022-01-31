using UnityEngine;

public abstract class Intractable : MonoBehaviour
{
    public bool Ignore = false;
    [SerializeField] private string _displayTipText = "Interact";
    public string DisplayTipText => _displayTipText;

    public abstract void OnInteract(GameObject subject);
}