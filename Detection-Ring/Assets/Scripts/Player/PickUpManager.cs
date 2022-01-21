using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CameraSwitcher))]
public class PickUpManager : MonoBehaviour
{
    [SerializeField, Min(0)] float _reach = 1.5f;
    [SerializeField, Range(-1f,1f)] float _threshold = 0.95f;
    [SerializeField] Text _displayText;
    [SerializeField] private Intractable _interactable;
    [SerializeField] private KeyCode _interactKey = KeyCode.E;

    private CameraSwitcher _cameraSwitcher;

    private void Awake()
    {
        _cameraSwitcher = GetComponent<CameraSwitcher>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        CheckForInteractable();
        InteractableInput();
    }

    private void CheckForInteractable()
    {
        _interactable = GetInteractableBeingLookedAt();

        if (_interactable != null)
            _displayText.text = _interactable.DisplayTipText;
        else
            _displayText.text = "";
    }

    private void InteractableInput()
    {
        if (Input.GetKeyDown(_interactKey) && _interactable != null && GetInteractableBeingLookedAt() != null)
            _interactable.OnInteract();
    }

    private Intractable GetInteractableBeingLookedAt()
    {
        Intractable result = null;
        Transform camTransform = _cameraSwitcher.CurrentCamera.transform;

        Ray ray = new Ray(camTransform.position, camTransform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, _reach))
            result = hit.transform.GetComponent<Intractable>();

        return result != null ? result : ResponsiveSelector(ray);
    }

    private Intractable ResponsiveSelector(Ray ray)
    {
        Intractable result = null;
        float nearestAngle = Mathf.Infinity;
        Transform camTransform = _cameraSwitcher.CurrentCamera.transform;

        Collider[] colliders = Physics.OverlapSphere(transform.position, _reach);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent(out Intractable interactable))
            {
                Vector3 toPlayer = colliders[i].transform.position - camTransform.position;
                toPlayer.Normalize();

                float angleToPlayer = Vector3.Dot(toPlayer, ray.direction);

                if (nearestAngle > angleToPlayer && angleToPlayer >= _threshold)
                {
                    nearestAngle = angleToPlayer;
                    result = interactable;
                }
            }
        }

        return result;
    }
}