using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
 public float mouseSensitivity = 100f;
    public Transform playerBody;

    private float xRotation = 0f;

    public Camera[] cameras; // Array to hold different cameras
    private int currentCameraIndex = 0;

    void Start()
    {
        // Lock the cursor to the center of the screen
        // Cursor.lockState = CursorLockMode.Locked;

        // Initialize all cameras
        SwitchToCamera(0);
    }

    void Update()
    {
        // Mouse look logic
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        playerBody.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply the vertical rotation to the active camera
        cameras[currentCameraIndex].transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Check for camera switch input (e.g., press 'C' to switch)
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchToNextCamera();
        }
    }

    void SwitchToCamera(int index)
    {
        // Deactivate all cameras
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }

        // Activate the selected camera
        cameras[index].gameObject.SetActive(true);
        currentCameraIndex = index;
    }

    void SwitchToNextCamera()
    {
        int nextIndex = (currentCameraIndex + 1) % cameras.Length;
        SwitchToCamera(nextIndex);
    }
}
