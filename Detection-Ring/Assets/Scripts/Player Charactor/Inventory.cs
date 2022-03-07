using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private IHandheldItem _primaryHandheldItem;

    private HandheldItemPlayerManager _itemManager;
    [SerializeField] private HeldItemPlayerHandler[] _handheldItems = new HeldItemPlayerHandler[2];
    [SerializeField] private HeldItemPlayerHandler _currentlyHeldItem;
    private int _currentlyHeldItemIndex = 0;

    private void Awake()
    {
        _itemManager = GetComponentInChildren<HandheldItemPlayerManager>();
    }

    private void Start()
    {
        ChangeHeldItemTo(_currentlyHeldItemIndex);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            CycleHeldItemsForwards();
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