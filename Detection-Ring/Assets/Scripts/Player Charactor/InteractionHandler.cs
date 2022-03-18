using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Plumbly.Interactables
{
    [RequireComponent(typeof(InteractSelectionResponse), typeof(ISelector<Interactable>))]
    public class InteractionHandler : MonoBehaviour
    {
        [SerializeField] private PlayerManager _playerManager;

        public Interactable _selected;
        private InteractSelectionResponse _selectionResponse;
        private ISelector<Interactable> _selector;

        private void Awake()
        {
            _selectionResponse = GetComponent<InteractSelectionResponse>();
            _selector = GetComponent<ISelector<Interactable>>();
        }

        private void Update()
        {
            _selector.Check();

            Interactable interactable = _selector.GetSelection();

            if (interactable != _selected)
            {
                if (_selected != null)
                    _selected.OnExit(_playerManager);
                
                _selected = interactable;
                
                if (_selected != null)
                    _selected.OnEnter(_playerManager);
            }
        }
    }
}