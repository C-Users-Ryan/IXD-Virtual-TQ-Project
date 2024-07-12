using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCamera : MonoBehaviour, IInteractable
{
     public GameObject targetObject; // Reference to the object to activate and deactivate

    public void Interact()
    {
            targetObject.SetActive(true);
            Invoke("disable", 5f);

    }
    void disable(){
        targetObject.SetActive(false);
    }
}
