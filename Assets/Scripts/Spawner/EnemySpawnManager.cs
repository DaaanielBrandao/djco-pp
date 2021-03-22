using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawnManager : Spawner {
    
    private int remainingEnemies = 0;
    private EnemySpawner[] spawners;
    
	
    public override void OnWave(int waveNumber) {
        spawners = gameObject.GetComponentsInChildren<EnemySpawner>();

        remainingEnemies = 0;
        foreach (EnemySpawner spawner in spawners)
            remainingEnemies += spawner.OnWave(waveNumber);
        //maxAlive = (int) (remainingEnemies / (1 + ((float)waveNumber / 2f)));
        maxAlive = 100;
        Debug.Log("Max Alive: " + maxAlive);

        cooldown = 2;
        
        Debug.Log("Starting wave: " + remainingEnemies + " / " + maxAlive);
        
        ResetCooldown();
    }

    protected override List<GameObject> DoSpawn()
    {
                
        int numToSpawn = Mathf.Min(NumSpawnable(),  remainingEnemies);
        Debug.Log("Spawning " + numToSpawn + " enemies");

        List<GameObject> spawns = new List<GameObject>();
        for (int i = 0; i < numToSpawn; i++)
        {
            EnemySpawner[] validSpawners = spawners.Where(spawner => spawner.GetComponent<EnemySpawner>().CanSpawnEnemies()).ToArray();
            spawns.Add(validSpawners.GetRandom().SpawnEnemy());
        }
        remainingEnemies -= numToSpawn;

        return spawns;
    }

    public bool IsWaveDone()
    {
        return remainingEnemies == 0 && aliveObjs.Count == 0;
    }
}
