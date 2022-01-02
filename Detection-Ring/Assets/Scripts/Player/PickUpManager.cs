using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CameraSwitcher))]
public class PickUpManager : MonoBehaviour
{
    [SerializeField] float _reach = 1f;
    [SerializeField] float _threshold = 0.96f;
    [SerializeField] Text text;

    private CameraSwitcher _cameraSwitcher;

    private Interactable[] interactablesInRange;

    private void Awake()
    {
        _cameraSwitcher = GetComponent<CameraSwitcher>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Update()
    {
        interactablesInRange = ComponetRetrieval.FromOverlapSphere<Interactable>(transform.position, _reach);

        Camera cam = _cameraSwitcher.CurrentCamera;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Interactable interactable = GetClosestInteracableFromMouse(ray);
        text.text = interactable == null ? "" : interactable.UseText;

        if (interactable != null && Input.GetKeyDown(KeyCode.E))
        {
            interactable.Use();
        }
    }

    public Interactable GetClosestInteracableFromMouse(Ray ray)
    {
        Interactable selection = null;

        Vector3 v1 = ray.direction.normalized;
        float closest = 0f;

        for (int i = 0; i < interactablesInRange.Length; i++)
        {
            Vector3 v2 = Vector3.Normalize(interactablesInRange[i].transform.position - ray.origin);
            float lookDecimal = Vector3.Dot(v1, v2);

            if (lookDecimal > _threshold && lookDecimal > closest)
            {
                closest = lookDecimal;
                selection = interactablesInRange[i];
            }
        }

        return selection;
    }
}