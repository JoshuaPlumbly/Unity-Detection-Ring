using UnityEngine;

public abstract class Intractable : MonoBehaviour
{
    [SerializeField] private string _displayTipText = "Interact";
    public string DisplayTipText => _displayTipText;

    public abstract void OnInteract();
}