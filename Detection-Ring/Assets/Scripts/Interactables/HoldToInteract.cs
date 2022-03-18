using UnityEngine;
using UnityEngine.InputSystem;

namespace Plumbly.Interactables
{
    public abstract class HoldToInteract : Interactable
    {
        [SerializeField] private string _pomptString = "Interact";
        [SerializeField] private float _holdDuration;
        [SerializeField] private float _holdTimeElapsed;

        private PlayerManager _manager;
        private UserInputAction _inputActions;
        private InputAction _interactAction;

        public float GetProgess()
        {
            return _holdTimeElapsed / _holdDuration;
        }

        public override void OnEnter(PlayerManager manager)
        {
            _inputActions = SingletonUserControls.Get();
            _inputActions.PlayerActions.Activate.performed += _ => StartProgression();
            _inputActions.PlayerActions.Activate.canceled += _ => StopProgression();

            _manager = manager;

            _holdTimeElapsed = 0f;
        }

        public override void OnExit(PlayerManager subject)
        {
            _inputActions.PlayerActions.Activate.performed -= _ => StartProgression();
            _inputActions.PlayerActions.Activate.canceled -= _ => StopProgression();
            _manager.UpdateTick -= ContinueProgression;

            StopProgression();
        }

        private void StartProgression()
        {
            _manager.UpdateTick += ContinueProgression;
        }

        private void ContinueProgression()
        {
            _holdTimeElapsed += Time.deltaTime;

            if (_holdTimeElapsed < _holdDuration)
                return;

            OnExit(_manager);
            Preform();
        }

        private void StopProgression()
        {
            _manager.UpdateTick -= ContinueProgression;
            _holdTimeElapsed = 0f;
        }

        protected abstract void Preform();
    }
}