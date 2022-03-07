using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InteractPrompt : MonoBehaviour
{
    [SerializeField] private Text _textfeild;
    [SerializeField] private GameObject _progressBarParent;
    [SerializeField] private Image _progressBar;

    private void Start()
    {
        if (_textfeild == null || _progressBarParent == null || _progressBar == null)
            Debug.LogWarning(this + ": is missing component(s)");
    }

    public void SetTextPrompt(string textPrompt)
    {
        _textfeild.text = textPrompt;
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
