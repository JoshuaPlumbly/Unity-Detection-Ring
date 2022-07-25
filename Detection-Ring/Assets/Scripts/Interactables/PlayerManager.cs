using System;
using UnityEngine;

namespace Plumbly
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private InteractPrompt _interactPrompt;

        public InteractPrompt InteractPrompt => _interactPrompt;

        public event Action UpdateTick;

        private void Start()
        {
            if (_interactPrompt == null)
                Debug.LogWarning(this + ": is missing component");
        }

        public void Update()
        {
            UpdateTick?.Invoke();
        }
    }
}