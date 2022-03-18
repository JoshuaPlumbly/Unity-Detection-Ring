using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plumbly.Interactables
{
    [RequireComponent(typeof(PlayerManager))]
    internal class InteractSelectionResponse : MonoBehaviour, IStateMachine<Interactable>
    {
        [SerializeField] public Interactable _selected;

        private PlayerManager _playerManager;

        private void Awake()
        {
            _playerManager = GetComponent<PlayerManager>();
        }

        public void Enter(Interactable interactable)
        {
            if (_selected != null)
                _selected.OnExit(_playerManager);

            _selected = interactable;

            if (_selected != null)
                _selected.OnEnter(_playerManager);
        }

        public void Exit()
        {
            if (_selected == null)
                return;

            _selected.OnExit(_playerManager);
            _selected = null;
        }
    }
}