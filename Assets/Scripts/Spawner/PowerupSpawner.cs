using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : Spawner
{
    private Collider2D[] areas;
    public GameObject[] powerUps;

    // Start is called before the first frame update
    private void Start()
    {
        areas = GetComponents<Collider2D>();

    }
    
    public override void OnWave(int waveNumber)
    {
        cooldown = 1;
        maxAlive = 5;
        
        Debug.Log("Starting Powerup wave: " + maxAlive);
        
        ResetCooldown();
    }

    protected override List<GameObject> DoSpawn() {
        GameObject powerUp = GroundFinder.SpawnOnGround(powerUps.GetRandom(), areas.GetValidSpawnPosition());
            
        return new List<GameObject> { powerUp };
    }


}
 