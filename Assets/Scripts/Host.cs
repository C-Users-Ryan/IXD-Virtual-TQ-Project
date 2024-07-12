using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Host : MonoBehaviour
{

    public Camera[] camerasToDisable; 
    public Camera[] camerasToEnable; 

    private void CameraSwitch(){
        DisableAllCameras();
        EnableAllCameras();
    }

    public void DisableAllCameras()
    {
        foreach (Camera cam in camerasToDisable)
        {
            if (cam != null)
            {
                cam.enabled = false;
            }
        }
    }

    // This method should be called to enable the specified cameras
    public void EnableAllCameras()
    {
        foreach (Camera cam in camerasToEnable)
        {
            if (cam != null)
            {
                cam.enabled = true;
            }
        }
    }

    // This method disables and enables cameras as specified
    public void SwitchCameras()
    {
        DisableAllCameras();
        EnableAllCameras();
    }
}


