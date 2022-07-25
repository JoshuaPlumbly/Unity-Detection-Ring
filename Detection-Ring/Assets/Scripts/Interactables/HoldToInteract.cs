using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace Plumbly.Interactables
{
    public abstract class HoldToInteract : Interactable
    {
        [SerializeField] private string _pomptString = "Interact";
        [SerializeField] private float _holdDuration;
        [SerializeField] private float _holdTimeElapsed;

        private bool _isSelected = false;
        private InteractPrompt _interactPrompt;
        private UserInputAction _inputActions;
        private IEnumerator _currentProgressionCoroutine;

        public override void OnEnter(PlayerManager manager)
        {
            _inputActions = SingletonUserControls.Get();
            _inputActions.PlayerActions.Activate.started += StartProgression;
            _inputActions.PlayerActions.Activate.canceled += StopProgression;

            _interactPrompt = manager.InteractPrompt;
            _interactPrompt.SetTextPrompt(_pomptString);

            ResetProgress();
            _isSelected = true;
        }

        public override void OnExit()
        {
            if (!_isSelected)
                return;

            _inputActions.PlayerActions.Activate.started -= StartProgression;
            _inputActions.PlayerActions.Activate.canceled -= StopProgression;

            EndProgressionCoroutine();
            _interactPrompt.ShowText(false);
            _isSelected = false;
        }

        public float GetProgress()
        {
            return _holdTimeElapsed / _holdDuration;
        }

        public void ResetProgress()
        {
            _holdTimeElapsed = 0f;
            _interactPrompt.SetProgressBar(0f);
        }
        
        private void StartProgression(CallbackContext ctx)
        {
            if (_currentProgressionCoroutine != null)
                StopCoroutine(_currentProgressionCoroutine);

            _currentProgressionCoroutine = ContineProgress();
            StartCoroutine(_currentProgressionCoroutine);
        }

        public IEnumerator ContineProgress()
        {
            ResetProgress();
            _interactPrompt.ShowProgressBar(true);

            while (_holdTimeElapsed < _holdDuration)
            {
                _holdTimeElapsed += Time.deltaTime;
                _interactPrompt.SetProgressBar(GetProgress());
                yield return null;
            }

            Preform();
        }

        private void StopProgression(CallbackContext obj)
        {
            EndProgressionCoroutine();
        }

        private void EndProgressionCoroutine()
        {
            if (_currentProgressionCoroutine != null)
                StopCoroutine(_currentProgressionCoroutine);

            _interactPrompt.ShowProgressBar(false);
            ResetProgress();
        }

        private void OnDisable()
        {
            OnExit();
        }

        protected abstract void Preform();
    }
}