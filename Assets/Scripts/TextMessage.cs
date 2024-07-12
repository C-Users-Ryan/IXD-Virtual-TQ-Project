using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextMessage : MonoBehaviour, IInteractable
{
 public TextMeshProUGUI textMeshPro;
    public string[] messages;
    private int currentMessageIndex = 0;


public void Interact(){
            currentMessageIndex = (currentMessageIndex + 1) % messages.Length;
            textMeshPro.text = messages[currentMessageIndex];
}

}