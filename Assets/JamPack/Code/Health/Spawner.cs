using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private bool DEBUG_MODE;

    [Header("Spawner Settings")]
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] protected GameObject latestSpawnedObject;
    
    [Header("Unity Event")]
    public UnityEvent OnSpawn;

    // Redundant Method
    public virtual void SpawnObject()
    {
        // Checks to see if there is a reference to a preFab, if not return an error.
        if (objectToSpawn == null)
        {
            Debug.LogError("There is no reference to a preFab.");
            return;
        }

        // Sets a reference from the instantiated object.
        latestSpawnedObject = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
        
        OnSpawn?.Invoke();
        
        if (DEBUG_MODE) Debug.Log("Spawned Object");
    }
}


