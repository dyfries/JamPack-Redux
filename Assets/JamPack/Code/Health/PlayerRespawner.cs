using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A subclass of Spawner dedicated to spawn & respawn a Player preFab by subscribing to the spawned Player's OnDeath Event.
/// </summary>
public class PlayerRespawner : Spawner
{
    [Header("Player Respawn Settings")] 
    public bool RespawnOnDeath = true;
    
    /// <summary>
    /// SpawnObject override from base class Spawner, by spawning a Player.
    /// If RespawnOnDeath is true, SpawnObject subscribes to spawned Player's OnDeath Event.
    /// </summary>
    public override void SpawnObject()
    {
        base.SpawnObject();

        if (latestSpawnedObject.GetComponent<Health>() == null)
        {
            Debug.LogError("This Player preFab has no Health component.");
            return;
        }

        if (RespawnOnDeath) 
            latestSpawnedObject.GetComponent<Health>().OnDeath.AddListener(SpawnObject);

    }
}
