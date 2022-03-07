using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CameraSwitcher), typeof(InteractSelectionResponse))]
public class InteractionHandler : MonoBehaviour
{
    public Interactable _selected;
    private InteractSelectionResponse _selectionResponse;
    private ISelector<Interactable> _selector;

    private void Awake()
    {
        _selectionResponse = GetComponent<InteractSelectionResponse>();
        _selector = GetComponent<ISelector<Interactable>>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        _selector.Check();
        Interactable interactable = _selector.GetSelection();

        if (interactable != _selected)
        {
            _selected = interactable;
            _selectionResponse.Enter(_selected);
        }
    }
}