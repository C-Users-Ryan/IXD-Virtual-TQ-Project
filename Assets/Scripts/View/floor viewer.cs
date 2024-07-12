using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorviewer : MonoBehaviour
{
    // Array of GameObjects to switch through
    public GameObject[] gameObjects;

    // Index to keep track of the current position in the array
    private int currentIndex = 0;

    void Start()
    {
        // Initially set all game objects to active
        UpdateVisibility();
        // Initially set all game objects to active
        for (int i = 0; i < gameObjects.Length; i++)
        {
            SetMeshRendererState(gameObjects[i], true);
        }
        currentIndex = gameObjects.Length -1;

    }

    void Update()
    {
        // Check for key inputs
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentIndex < gameObjects.Length - 1)
            {
                currentIndex++;
                UpdateVisibility();
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                UpdateVisibility();
            }
        }
    }

    void UpdateVisibility()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            SetMeshRendererState(gameObjects[i], i <= currentIndex);
        }
    }

    void SetMeshRendererState(GameObject obj, bool state)
    {
        // Get the MeshRenderer component of the GameObject
        MeshRenderer meshRenderer = obj.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.enabled = state;
        }

        // Get all child MeshRenderer components
        MeshRenderer[] childMeshRenderers = obj.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer childMeshRenderer in childMeshRenderers)
        {
            childMeshRenderer.enabled = state;
        }
    }
}
