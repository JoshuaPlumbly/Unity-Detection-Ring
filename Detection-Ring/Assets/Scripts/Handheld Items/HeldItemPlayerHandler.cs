using System;
using UnityEngine;

namespace Plumbly
{
    [RequireComponent(typeof(IHandheldItem))]
    public class HeldItemPlayerHandler : MonoBehaviour
    {
        [SerializeField] private IHandheldItem _handheldItem;

        private void Awake()
        {
            _handheldItem = GetComponent<IHandheldItem>();

            SingletonUserControls.Get().PlayerActions.Fire1.started += _ => _handheldItem.Use();
        }

        public void Use()
        {
            _handheldItem.Use();
        }

        internal void OnUpdate()
        {

        }
    }
}