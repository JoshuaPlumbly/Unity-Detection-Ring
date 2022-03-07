using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameHUDHandler : MonoBehaviour
{
    [SerializeField] private GameObject _ingameHUD;

    public void SetEnabled(bool value)
    {
        _ingameHUD.SetActive(value);
    }
}
