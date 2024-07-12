using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDisplay : MonoBehaviour, IInteractable
{
    [SerializeField] private int view = 0; // Default to the first display (0)
    public Camera _camera;

 public void Interact()
    {
        Debug.Log("hit");
            _camera.targetDisplay = view;

    }
}
