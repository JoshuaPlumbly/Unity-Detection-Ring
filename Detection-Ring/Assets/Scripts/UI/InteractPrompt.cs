using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractPrompt : MonoBehaviour
{
    [SerializeField] private Text _textFeild;
    [SerializeField] private GameObject _progressBarParent;
    [SerializeField] private Image _progressBar;

    private void Start()
    {
        if (_textFeild == null || _progressBarParent == null || _progressBar == null)
            Debug.LogWarning(this + ": is missing component(s)");
    }

    public void SetTextPrompt(string textPrompt)
    {
        ShowText(true);
        _textFeild.text = textPrompt;
    }

    public void ShowText(bool visable)
    {
        _textFeild.enabled = visable;

        if (!visable)
            _textFeild.text = string.Empty;
    }

    public void SetProgressBar(float fillAmount)
    {
        ShowProgressBar(fillAmount != 0f);
        _progressBar.fillAmount = fillAmount;
    }

    public void ShowProgressBar(bool visable)
    {
        _progressBarParent.SetActive(visable);
    }
}
