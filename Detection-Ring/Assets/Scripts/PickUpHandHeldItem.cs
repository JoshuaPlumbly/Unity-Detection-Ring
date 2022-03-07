using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHandHeldItem : Interactable
{
    [SerializeField] private string _textPrompt = "Pick up";

    public override void OnUpdate(PlayerManager playerManager)
    {
        if (Input.GetKey(KeyCode.E))
            PickUpItem(playerManager);
    }

    public override void OnEnter(PlayerManager playerManager)
    {
        InteractPrompt[] prompts = FindObjectsOfType<InteractPrompt>();

        for (int i = 0; i < prompts.Length; i++)
        {
            prompts[i].SetTextPrompt(_textPrompt);
        }
    }

    public override void OnExit(PlayerManager playerManager)
    {
        InteractPrompt[] prompts = FindObjectsOfType<InteractPrompt>();

        for (int i = 0; i < prompts.Length; i++)
        {
            prompts[i].SetTextPrompt(string.Empty);
        }
    }
    private void PickUpItem(PlayerManager playerManager)
    {
        Inventory inventory = playerManager.GetComponent<Inventory>();
    }
}