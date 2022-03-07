using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MunitionsUI : MonoBehaviour
{
    [SerializeField] Text _loadedMunitionsTextField;
    [SerializeField] Text _unloadedMunitionsTextField;

    private void Awake()
    {
        if (_loadedMunitionsTextField == null || _unloadedMunitionsTextField == null)
            Debug.LogWarning(this + ": is missing Component(s)!");
    }

    public void SetLoadedMunitionText(string text)
    {
        _loadedMunitionsTextField.text = text;
    }

    public void SetUnloadedMunitionText(string text)
    {
        _unloadedMunitionsTextField.text = text;
    }
}
