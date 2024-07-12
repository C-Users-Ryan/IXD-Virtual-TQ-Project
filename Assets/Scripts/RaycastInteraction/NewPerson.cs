using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NewPerson : MonoBehaviour, IInteractable
{
      public GameObject prefab;
    public Vector3 spawnPosition;
    public Quaternion spawnRotation = Quaternion.identity;
    public float despawnTime = 120f; 

    public void Interact()
    {
    
            GameObject spawnedObject = Instantiate(prefab, spawnPosition, spawnRotation);

            StartCoroutine(DespawnAfterTime(spawnedObject, despawnTime));
        
    }

    private IEnumerator DespawnAfterTime(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (obj != null)
        {
            Destroy(obj);
        }
    }
}
