using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plumbly
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private HandheldImplement _primaryHandheldItem;


        [SerializeField] private PlayerImplement[] _handheldItems = new PlayerImplement[2];
        [SerializeField] private PlayerImplement _currentlyHeldItem;

        private int _currentlyHeldItemIndex = 0;
        private HandheldItemPlayerManager _itemManager;

        private void Awake()
        {
            _itemManager = GetComponentInChildren<HandheldItemPlayerManager>();
            SingletonUserControls.Get().PlayerActions.CycleHeldItemsForwards.started += _ => CycleHeldItemsForwards();
        }

        private void Start()
        {
            ChangeHeldItemTo(_currentlyHeldItemIndex);
        }

        public void ChangeHeldItemTo(int index)
        {
            _currentlyHeldItemIndex = index;

            if (_handheldItems[_currentlyHeldItemIndex] != null)
            {
                _currentlyHeldItem = _handheldItems[_currentlyHeldItemIndex];
                _itemManager.Equip(_currentlyHeldItem);
            }
        }

        public void CycleHeldItemsForwards()
        {
            for (int i = _currentlyHeldItemIndex + 1; i < _handheldItems.Length; i++)
            {
                if (_handheldItems[i] == null)
                    continue;

                ChangeHeldItemTo(i);
                return;
            }

            for (int i = 0; i < _currentlyHeldItemIndex; i++)
            {
                if (_handheldItems[i] == null)
                    continue;

                ChangeHeldItemTo(i);
                return;
            }
        }
    }
}