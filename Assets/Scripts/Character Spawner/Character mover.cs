using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactermover : MonoBehaviour
{

    public GameObject[] objectsToActivate;
    public float delayBetweenActivations = 1f;

    void Start()
    {

        StartCoroutine(ActivateObjectsCoroutine());
    }

    IEnumerator ActivateObjectsCoroutine()
    {
        foreach (GameObject obj in objectsToActivate)
        {

            yield return new WaitForSeconds(delayBetweenActivations);

            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
    }
}
