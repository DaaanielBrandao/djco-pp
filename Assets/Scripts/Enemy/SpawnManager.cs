using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRangeX = 200;
    public float spawnRangeY = 20;


    public float cooldownRemaining = 0;
    public float cooldown = 10.0f;
    public int maxAlive = 5;

    private int waveNumber = 0;
    private int remainingEnemies = 0;
    private List<GameObject> aliveEnemies;
    
    // Start is called before the first frame update
    void Start()
    {
       // spawnEnemyWave(waveNumber);
    }

    public void OnGameReset()
    {
        waveNumber = 0;
    }

    public void AdvanceWave()
    {
        waveNumber++;
        remainingEnemies = waveNumber * 10; // podia haver formula maluca
        aliveEnemies = new List<GameObject>();
        cooldownRemaining = 0;
    }

    public bool IsWaveDone()
    {
        return remainingEnemies == 0 && aliveEnemies.Count == 0;
    }

    // Update is called once per frame
    void Update()
    {
        cooldownRemaining -= Time.deltaTime;
        if (cooldownRemaining <= 0) {
            aliveEnemies.RemoveAll(item => item == null);
            SpawnEnemies(Mathf.Min(maxAlive - aliveEnemies.Count, remainingEnemies));
            cooldownRemaining = cooldown;
        }
    }



    private Vector3 getRandomPos(){
        float spawnPosX = Random.Range(-spawnRangeX,spawnRangeX);
        float spawnPosZ = Random.Range(-spawnRangeY,spawnRangeY);
        return new Vector3(spawnPosX + transform.position.x,spawnPosZ + transform.position.y,0);
    }

    private void SpawnEnemies(int numEnemies){
        Debug.Log("Spawning " + numEnemies + " enemies");
        for(int i = 0; i < numEnemies; i++){
            aliveEnemies.Add(Instantiate(enemyPrefab, getRandomPos(),enemyPrefab.transform.rotation));
        }

        remainingEnemies -= numEnemies;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnRangeX*2,spawnRangeY*2,0));
    }
}
