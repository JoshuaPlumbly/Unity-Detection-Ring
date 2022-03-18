using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plumbly.Interactables
{
    public class PickUpHandHeldItem : Interactable
    {
        [SerializeField] private string _textPrompt = "Pick up";

        public override void OnEnter(PlayerManager playerManager)
        {
            InteractPrompt[] prompts = FindObjectsOfType<InteractPrompt>();

            for (int i = 0; i < prompts.Length; i++)
            {
                prompts[i].SetTextPrompt(_textPrompt);
            }

            SingletonUserControls.Get().PlayerActions.Activate.started += _ => PickUpItem(playerManager);
        }

        public override void OnExit(PlayerManager playerManager)
        {
            InteractPrompt[] prompts = FindObjectsOfType<InteractPrompt>();

            for (int i = 0; i < prompts.Length; i++)
            {
                prompts[i].SetTextPrompt(string.Empty);
            }

            SingletonUserControls.Get().PlayerActions.Activate.started += _ => PickUpItem(playerManager);
        }

        private void PickUpItem(PlayerManager playerManager)
        {
            Inventory inventory = playerManager.GetComponent<Inventory>();
        }
    }
}