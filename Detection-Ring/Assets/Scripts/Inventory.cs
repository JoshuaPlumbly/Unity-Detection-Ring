using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] Transform _handTransform;
    [SerializeField] Tool _main;

    public Transform HandTransform => _handTransform;

    public void PickUp(Tool tool)
    {
        if (tool == null)
            return;

        _main = tool;
        _main.OnSelectedAsMain(this);
    }
}
