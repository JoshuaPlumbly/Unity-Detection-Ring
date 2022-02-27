using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CameraSwitcher), typeof(IStateMachine<Interactable>))]
public class InteractionHandler : MonoBehaviour
{
    public Interactable _selected;
    private IStateMachine<Interactable> _selectionResponse;
    private ISelector<Interactable> _selector;

    private void Awake()
    {
        _selectionResponse = GetComponent<IStateMachine<Interactable>>();
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

    private void SelectInteractable(Interactable interactable)
    {
        if (_selected == interactable)
            return;

        _selected.OnExit(gameObject);
        _selected = interactable;
        _selected.OnEnter(gameObject);
    }
}