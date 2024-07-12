using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Lecture : NetworkBehaviour, WalkInteractable
{
    public float TaskTime;
    private float currentTime;
    private bool isTimerRunning = false;

    public GameObject[] objectsToActivate;

    public float interactionDistance = 3f;

    private MoveToWaypoint moveToWaypoint;

    void Start()
    {
        currentTime = TaskTime;
    }

    public void Interact()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }

        isTimerRunning = true;

        // Call the server method to synchronize activation and timer start
        ActivateObjectsServerRpc();
    }

    [ServerRpc]
    void ActivateObjectsServerRpc()
    {
        // Activate objects on all clients
        foreach (GameObject obj in objectsToActivate)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }

        // Start timer on server
        isTimerRunning = true;

        // Inform clients to start their timers
        ActivateObjectsClientRpc();
    }

    [ClientRpc]
    void ActivateObjectsClientRpc()
    {
        // Start timer on clients
        isTimerRunning = true;
    }

    void Update()
    {
        if (isTimerRunning)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0.0f)
            {
                currentTime = 0;
                isTimerRunning = false;

                // Call server method to reset timer on all clients
                ResetTimerServerRpc();
            }
        }
    }

    [ServerRpc]
    void ResetTimerServerRpc()
    {
        // Reset timer on all clients
        currentTime = TaskTime;
        isTimerRunning = false;

        // Deactivate objects on all clients
        foreach (GameObject obj in objectsToActivate)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }

        // Find all MoveToWaypoint instances and interact with those within the specified distance
        MoveToWaypoint[] allMoveToWaypoints = FindObjectsOfType<MoveToWaypoint>();
        foreach (MoveToWaypoint moveToWaypoint in allMoveToWaypoints)
        {
            if (Vector3.Distance(transform.position, moveToWaypoint.transform.position) <= interactionDistance)
            {
                moveToWaypoint.nextWaypoint();
            }
        }

        // Inform clients to reset timer and deactivate objects
        ResetTimerClientRpc();
    }

    [ClientRpc]
    void ResetTimerClientRpc()
    {
        // Reset timer on clients
        currentTime = TaskTime;
        isTimerRunning = false;

        // Deactivate objects on clients
        foreach (GameObject obj in objectsToActivate)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }
}
