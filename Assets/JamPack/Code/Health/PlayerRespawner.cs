using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawner : Spawner
{
    public override void SpawnObject()
    {
        base.SpawnObject();

        if (latestSpawnedObject.GetComponent<Health>() == null)
        {
            Debug.LogError("This Player has no Health component.");
            return;
        }
        
    }
}
