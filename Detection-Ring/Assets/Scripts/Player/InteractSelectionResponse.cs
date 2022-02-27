using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class InteractSelectionResponse : MonoBehaviour, IStateMachine<Interactable>
{
    [SerializeField] public Interactable _selected;

    public void Enter(Interactable interactable)
    {
        if (_selected != null)
            _selected.OnExit(gameObject);

        _selected = interactable;

        if (_selected != null)
            _selected.OnEnter(gameObject);
    }

    public void Exit()
    {
        if (_selected == null)
            return;

        _selected.OnExit(gameObject);
        _selected = null;
    }

    private void Update()
    {
        if (_selected == null)
            return;

        _selected.OnUpdate(gameObject);
    }
}