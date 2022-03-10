using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disarm : Interactable
{
    [SerializeField] private GameObject _toRemove;
    [SerializeField] private float _holdDuration;
    [SerializeField] private float _holdTimeElapsed;
    [SerializeField] private string _pomptString = "Hold [E] Defuse Explosive Mine";

    private InteractPrompt[] _interactPrompt;

    public override void OnUpdate(PlayerManager subject)
    {
        UpdateHeldProgress();
        UpdateProgressBar();
    }

    private void UpdateHeldProgress()
    {
        if (Input.GetKey(KeyCode.E))
        {
            _holdTimeElapsed += Time.deltaTime;

            if (_holdTimeElapsed >= _holdDuration)
                _toRemove.SetActive(false);
        }
        else
        {
            _holdTimeElapsed = 0f;
        }
    }

    public override void OnEnter(PlayerManager subject)
    {
        _holdTimeElapsed = 0f;
        _interactPrompt = FindObjectsOfType<InteractPrompt>();

        SetTextPrompt(_pomptString);
    }

    public override void OnExit(PlayerManager subject)
    {
        _holdTimeElapsed = 0f;

        ShowProgressBar(false);
        SetTextPrompt(string.Empty);
    }

    private void SetTextPrompt(string prompt)
    {
        for (int i = 0; i < _interactPrompt.Length; i++)
            _interactPrompt[i].SetTextPrompt(prompt);
    }

    private void UpdateProgressBar()
    {
        for (int i = 0; i < _interactPrompt.Length; i++)
            _interactPrompt[i].SetProgressBar(GetProgess());
    }

    public float GetProgess()
    {
        return _holdTimeElapsed / _holdDuration;
    }

    public void ShowProgressBar(bool visable)
    {
        for (int i = 0; i < _interactPrompt.Length; i++)
            _interactPrompt[i].ShowProgressBar(visable);
    }
}