using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour
{
       // Array to hold references to cameras
    public Camera[] cameras;

    // Key to switch to the next camera
    public KeyCode switchKey = KeyCode.C;

    // Mouse sensitivity
    public float mouseSensitivity = 100f;

    // Rotation around the Y-axis (left and right)
    private float yaw = 0f;

    // Rotation around the X-axis (up and down)
    private float pitch = 0f;

    // Index of the currently active camera
    private int currentCameraIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure there is at least one camera
        if (cameras.Length == 0)
        {
            Debug.LogError("No cameras assigned.");
            return;
        }

        // Enable the first camera and disable others
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].enabled = (i == currentCameraIndex);
        }

        // Lock the cursor to the center of the screen and make it invisible
        // Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // Handle camera switching
        if (Input.GetKeyDown(switchKey))
        {
            // Disable the current camera
            cameras[currentCameraIndex].enabled = false;

            // Increment the camera index and wrap around if necessary
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

            // Enable the next camera
            cameras[currentCameraIndex].enabled = true;
        }

        // Handle camera rotation with the mouse
        if (cameras[currentCameraIndex] != null)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            yaw += mouseX;
            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, -90f, 90f); // Clamp pitch to prevent flipping

            cameras[currentCameraIndex].transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);
        }
    }
}
