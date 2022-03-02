using UnityEngine;

[RequireComponent(typeof(IHandheldItem))]
public class HandheldItemControllerForPlayer : MonoBehaviour
{
    [SerializeField] private IHandheldItem _handheldItem;

    private void Awake()
    {
        _handheldItem = GetComponent<IHandheldItem>();
    }

    public void OnUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
             _handheldItem.Use();
    }
}