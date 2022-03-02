using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disarm : Interactable
{
    [SerializeField] private GameObject _toRemove;
    [SerializeField] private float _holdDuration;
    [SerializeField] private float _holdTimeElapsed;

    public static event Action<Disarm> OnDisarmed;

    public string PromtText => "Disarm";

    public override void OnUpdate(GameObject subject)
    {
        if (Input.GetKey(KeyCode.E))
        {
            _holdTimeElapsed += Time.deltaTime;

            if (_holdTimeElapsed >= _holdDuration)
            {
                OnDisarmed?.Invoke(this);
                _toRemove.SetActive(false);
            }
        }
        else
        {
            _holdTimeElapsed = 0f;
        }
    }

    public override void OnEnter(GameObject subject)
    {
        _holdTimeElapsed = 0f;
        InteractPrompt[] prompts = FindObjectsOfType<InteractPrompt>();

        for (int i = 0; i < prompts.Length; i++)
        {
            prompts[i].TextPrompt(PromtText);
        }
    }

    public override void OnExit(GameObject subject)
    {
        _holdTimeElapsed = 0f;

        InteractPrompt[] prompts = FindObjectsOfType<InteractPrompt>();

        for (int i = 0; i < prompts.Length; i++)
        {
            prompts[i].TextPrompt(string.Empty);
        }
    }
}

public class ButtonProptSystem : MonoBehaviour
{
    private static ButtonProptSystem _instance;
    public GameObject ButtonProptGameObject { get; }

    private void Awake()
    {
        if (_instance == null)
            Debug.LogWarning("More then two ButtonPropt Scripts are present.");

        _instance = this;
    }

    public static void Show()
    {
    }
}
