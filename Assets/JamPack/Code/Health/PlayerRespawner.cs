using UnityEngine;

/// <summary>
/// A subclass of Spawner dedicated to spawn & respawn a Player preFab by subscribing to the spawned Player's OnDeath Event.
/// NOTE: A Player does not need to be in the scene if a PlayerRespawner is used, it will spawn its own Player.
/// </summary>
public class PlayerRespawner : Spawner
{
    [Header("Player Respawn Settings")] 
    public bool RespawnOnDeath = true;
    
    [Header("Health UI Reference")]
    [SerializeField] private HealthBarUI healthBarUI;
    
    /// <summary>
    /// SpawnObject override from base class Spawner, by spawning a Player.
    /// If RespawnOnDeath is true, SpawnObject subscribes to spawned Player's OnDeath Event.
    /// </summary>
    public override void SpawnObject()
    {
        // Runs the base class method of SpawnObject then runs the next added functionality.
        base.SpawnObject();

        
        // If the player preFab has no Health component, return an error.
        if (latestSpawnedObject.GetComponent<Health>() == null)
        {
            Debug.LogError("This Player preFab has no Health component.");
            return;
        }
        
        
        // If Player needs to respawn on its death, subscribe the SpawnObject method to the OnDeath Event.
        if (RespawnOnDeath) 
            latestSpawnedObject.GetComponent<Health>().OnDeath.AddListener(SpawnObject);

        
        // If a Health Bar UI Component is referenced, then track the Player's Health with the UI.
        if (healthBarUI != null)
        {
            healthBarUI.healthComponentToTrack = latestSpawnedObject.GetComponent<Health>();
            healthBarUI.healthComponentToTrack.OnDamage.AddListener(healthBarUI.SetPercentFilled);
            healthBarUI.SetPercentFilled();
        }
            
    }
}
