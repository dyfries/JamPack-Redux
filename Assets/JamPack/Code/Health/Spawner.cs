using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A class that spawns objects into the Scene at the transform of the object with the Spawner class.
/// </summary>
public class Spawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private bool spawnOnStart = true;
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] protected GameObject latestSpawnedObject;
    
    [Header("Events")]
    public UnityEvent OnSpawn = new UnityEvent();
    
    [Header("Settings")]
    [SerializeField] private bool DEBUG_MODE;

    // Start is called before the first frame update.
    private void Start()
    {
        // If the object is to spawn on start of the game, spawn an object into the scene.
        if (spawnOnStart) SpawnObject();
    }
    
    /// <summary>
    /// Spawn an Object into the Scene at the position & rotation of the Spawner.
    /// </summary>
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
        
        
        // Calls OnSpawn Event while Object has spawned.
        OnSpawn?.Invoke();
        
        
        if (DEBUG_MODE) Debug.Log("Spawned : " + latestSpawnedObject.name);
    }
}


