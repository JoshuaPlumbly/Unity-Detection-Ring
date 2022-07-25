using System;
using UnityEngine;

namespace Plumbly
{
    [RequireComponent(typeof(HandheldImplement))]
    public class PlayerImplement : MonoBehaviour
    {
        [SerializeField] private HandheldImplement _handheldItem;

        private void OnEnable()
        {
            _handheldItem = GetComponent<HandheldImplement>();

            SingletonUserControls.Get().PlayerActions.Fire1.started += _ => _handheldItem.Use();
        }

        private void OnDisable()
        {
            
        }

        public void Use()
        {
            _handheldItem.Use();
        }
    }
}