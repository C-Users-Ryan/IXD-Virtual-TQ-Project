using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorviewercopy : MonoBehaviour
{
// Array of GameObjects to switch through
    public GameObject[] gameObjects;

    // Index to keep track of the current position in the array
    private int currentIndex = 0;

    // Duration of the move animation
    public float moveDuration = 1.0f;

    // Height to move the floors to "disappear"
    public float moveHeight = 10.0f;

    void Start()
    {
        // Initially set all game objects to active
        UpdateVisibility();
        for (int i = 0; i < gameObjects.Length; i++)
        {
            SetPosition(gameObjects[i], true);
        }
        currentIndex = gameObjects.Length - 1;
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
            if (i <= currentIndex)
            {
                StartCoroutine(MoveToPosition(gameObjects[i], true));
            }
            else
            {
                StartCoroutine(MoveToPosition(gameObjects[i], false));
            }
        }
    }

    IEnumerator MoveToPosition(GameObject obj, bool visible)
    {
        Vector3 startPosition = obj.transform.position;
        Vector3 endPosition = visible ? startPosition : startPosition + new Vector3(0, moveHeight, 0);
        Vector3 targetPosition = visible ? startPosition : startPosition - new Vector3(0, moveHeight, 0);
        for (float t = 0; t < moveDuration; t += Time.deltaTime)
        {
            obj.transform.position = Vector3.Lerp(startPosition, targetPosition, t / moveDuration);
            yield return null;
        }
        obj.transform.position = targetPosition;
    }

    void SetPosition(GameObject obj, bool state)
    {
        Vector3 targetPosition = state ? obj.transform.position : obj.transform.position + new Vector3(0, moveHeight, 0);
        obj.transform.position = targetPosition;
    }
}
