using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CameraSwitcher))]
public class PickUpManager : MonoBehaviour
{
    [SerializeField, Min(0)] float _reach = 1.5f;
    [SerializeField, Min(0)] float _casualReach = 1f;
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
        _interactable = GetInteractable();

        if (_interactable != null)
            _displayText.text = _interactable.DisplayTipText;
        else
            _displayText.text = "";
    }

    private void InteractableInput()
    {
        if (Input.GetKeyDown(_interactKey) && _interactable != null)
            _interactable.OnInteract(gameObject);
    }

    private Intractable GetInteractableBeingLookedAt()
    {
        Intractable result = null;
        Transform camTransform = _cameraSwitcher.CurrentCamera.transform;

        Ray ray = new Ray(camTransform.position, camTransform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, _reach))
            result = hit.transform.GetComponent<Intractable>();

        return result;
    }

    private Intractable GetInteractable()
    {
        Intractable result = null;
        Transform camTransform = CameraManager.Current.transform;

        Vector3 playerPosition = transform.position;
        Vector3 cameraPosition = camTransform.position;
        Vector3 cameraDirection = Vector3.Normalize(camTransform.forward);

        // Get interactable being directly looked at.
        if (Physics.Raycast(new Ray(cameraPosition, cameraDirection), out RaycastHit hit, _reach))
            result = hit.transform.GetComponent<Intractable>();

        if (result != null)
            return result;

        Collider[] colliders = Physics.OverlapSphere(playerPosition, _reach);
        float nearestAngle = Mathf.Infinity;
        float nearestInteractable = Mathf.Infinity;

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent(out Intractable interactable))
            {
                if (interactable.Ignore)
                    continue;

                Vector3 collidersPosition = colliders[i].transform.position;

                // Get interactable with the neastest to the center of screen.
                Vector3 toCamera = Vector3.Normalize(collidersPosition - cameraPosition);
                float angleToCamera = Vector3.Dot(toCamera, cameraDirection);

                if (angleToCamera < nearestAngle && angleToCamera >= _threshold)
                {
                    nearestAngle = angleToCamera;
                    result = interactable;
                }

                // Get nearest interactable to player.
                if (result != null)
                    continue;

                float distanceToPlayer = Vector3.Distance(collidersPosition, playerPosition);

                if (distanceToPlayer < nearestInteractable && distanceToPlayer <= _casualReach)
                {
                    nearestInteractable = distanceToPlayer;
                    result = interactable;
                }
            }
        }

        return result;
    }
}