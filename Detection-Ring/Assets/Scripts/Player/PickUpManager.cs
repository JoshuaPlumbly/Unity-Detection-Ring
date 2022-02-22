using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CameraSwitcher))]
public class PickUpManager : MonoBehaviour
{
    [SerializeField] private Image _progressBar;
    [SerializeField] private KeyCode _interactKey = KeyCode.E;
    [SerializeField, Min(0)] float _reach = 1.5f;
    [SerializeField, Min(0)] float _casualReach = 1f;
    [SerializeField, Range(-1f,1f)] float _threshold = 0.95f;
    [SerializeField] Text _displayText;


    public Intractable _bestInteractable;
    private CameraSwitcher _cameraSwitcher;
    public float _progress = 0f;

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
        SelectBestInteractable();
        InteractableInput();
    }

    private void SelectBestInteractable()
    {
        var newBestInteractable = GetInteractable();

        if (newBestInteractable != _bestInteractable)
        {
            _bestInteractable = newBestInteractable;

            if (_bestInteractable != null)
                _displayText.text = _bestInteractable.IntractableText;
            else
                _displayText.text = "";
        }
    }

    private void InteractableInput()
    {
        if (_bestInteractable == null)
            return;

        if (Input.GetKeyDown(_interactKey))
            _bestInteractable.OnInteractDown(gameObject);
        else if (Input.GetKeyUp(_interactKey))
            _bestInteractable.OnInteractUp(gameObject);
        else if (Input.GetKey(_interactKey))
            _bestInteractable.OnInteract(gameObject);
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