using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : Spawner {
    
    private int remainingEnemies = 0;
    private EnemySpawner[] spawners;
	
    // Start is called before the first frame update
    private void Start() {
        spawners = gameObject.GetComponentsInChildren<EnemySpawner>();
    }

    public override void OnWave(int waveNumber) {
        remainingEnemies = 10 * waveNumber + 40; // podia haver formula maluca
        maxAlive = remainingEnemies / 4;
        
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
            EnemySpawner spawner = spawners.GetRandom();
            spawns.Add(spawner.SpawnEnemy());
        }
        remainingEnemies -= numToSpawn;

        return spawns;
    }

    public bool IsWaveDone()
    {
        return remainingEnemies == 0 && aliveObjs.Count == 0;
    }
}
