using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CameraSwitcher))]
public class PickUpManager : MonoBehaviour
{
    [SerializeField, Min(0)] float _reach = 1f;
    [SerializeField, Range(0,360)] float _threshold = 30f;
    [SerializeField] Text _displayText;

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
        Interactable interactable = FindInteractableBeingLookedAt();
        _displayText.text = interactable == null ? "" : interactable.UseText;

        if (interactable != null && Input.GetKeyDown(KeyCode.E))
            interactable.Use();
    }

    public Interactable FindInteractableBeingLookedAt()
    {
        Interactable nearestInteractable = null;
        float nearestInteractableAngle = Mathf.Infinity;

        Vector3 playerLookDirection = _cameraSwitcher.CurrentCamera.ViewportPointToRay(new Vector3(Screen.width*0.5f, Screen.height *0.5f)).direction.normalized;

        Collider[] colliders = Physics.OverlapSphere(transform.position, _reach);

        for (int i = 0; i < colliders.Length; i++)
        {
            Interactable interactable = colliders[i].GetComponent<Interactable>();

            if (interactable == null)
                continue;

            Vector3 playerToInteractableDirection = (_cameraSwitcher.CurrentCamera.transform.position - interactable.transform.position).normalized;
            float playerToInteractableAngle = Vector3.Angle(playerToInteractableDirection, playerLookDirection);
            
            if (nearestInteractableAngle < playerToInteractableAngle)
            {
                nearestInteractableAngle = playerToInteractableAngle;
                nearestInteractable = interactable;
            }
        }

        return nearestInteractableAngle < _threshold ? nearestInteractable : null;
    }
}