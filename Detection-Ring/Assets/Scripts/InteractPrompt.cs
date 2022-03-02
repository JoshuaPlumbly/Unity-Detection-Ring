using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractPrompt : MonoBehaviour
{
    Text _textfeild;

    private void Awake()
    {
        _textfeild = GetComponent<Text>();
    }

    public void TextPrompt(string textPrompt)
    {
        _textfeild.text = textPrompt;
    }
}
