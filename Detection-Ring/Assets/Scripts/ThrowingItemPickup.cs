using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ThrowingItem))]
public class ThrowingItemPickup : Interactable
{
    [SerializeField] private string _textPrompt = "Pick up";

    public override void OnUpdate(GameObject subject)
    {


        //if (Input.GetKey(KeyCode.E))
        //{
        //    if (subject.TryGetComponent<Inventory>(out var inventory))
        //    {
        //        inventory.EqiuptPrimaryHand(GetComponent<ThrowingItem>());
        //    }
        //}
    }

    public override void OnEnter(GameObject subject)
    {
        InteractPrompt[] prompts = FindObjectsOfType<InteractPrompt>();

        for (int i = 0; i < prompts.Length; i++)
        {
            prompts[i].TextPrompt(_textPrompt);
        }
    }

    public override void OnExit(GameObject subject)
    {
        InteractPrompt[] prompts = FindObjectsOfType<InteractPrompt>();

        for (int i = 0; i < prompts.Length; i++)
        {
            prompts[i].TextPrompt(string.Empty);
        }
    }
}
