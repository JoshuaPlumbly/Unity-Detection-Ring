using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Plumbly.Interactables
{
    public class PickUpHandHeldItem : Interactable
    {
        [SerializeField] private string _textPrompt = "Pick up";

        private PlayerManager _manager;

        public override void OnEnter(PlayerManager playerManager)
        {
            InteractPrompt[] prompts = FindObjectsOfType<InteractPrompt>();

            for (int i = 0; i < prompts.Length; i++)
            {
                prompts[i].SetTextPrompt(_textPrompt);
            }

            _manager = playerManager;
            SingletonUserControls.Get().PlayerActions.Activate.started += _ => PickUpItem(playerManager);
        }

        public override void OnExit()
        {
            InteractPrompt[] prompts = FindObjectsOfType<InteractPrompt>();

            for (int i = 0; i < prompts.Length; i++)
            {
                prompts[i].SetTextPrompt(string.Empty);
            }

            SingletonUserControls.Get().PlayerActions.Activate.started += _ => PickUpItem(_manager);
        }

        private void PickUpItem(PlayerManager playerManager)
        {
            Inventory inventory = playerManager.GetComponent<Inventory>();
        }
    }
}