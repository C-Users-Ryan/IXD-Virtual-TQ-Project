using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IInteractable
{
    void Interact();
}

public class MouseRaycast : MonoBehaviour
{
    public float interactionDistance = 200f; 
    [SerializeField] Camera ObserverCamera;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = ObserverCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact();
                }
                else
                {
                    Debug.LogWarning("No IInteractable component found on " + hit.collider.name);
                }
            }
            else
            {
                Debug.Log("Raycast did not hit anything.");
            }
        }
    }
}
