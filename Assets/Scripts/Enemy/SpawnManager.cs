using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 40;
    public int enemyCount;
    public int waveNumber = 1;
    // Start is called before the first frame update
    void Start()
    {
        spawnEnemyWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<EnemyController>().Length;
        if(enemyCount == 0){
            waveNumber++;
            waveNumber = Mathf.Min(waveNumber,4); // e o msm codigo do que o tutorial 4 e n me aptece mexer
            spawnEnemyWave(waveNumber);
        }
    }

    private Vector3 getRandomPos(){
        float spawnPosX = Random.Range(-spawnRange,spawnRange);
        float spawnPosZ = Random.Range(-spawnRange,spawnRange);
        return new Vector3(spawnPosX + transform.position.x,spawnPosZ + transform.position.y,0);
    }

    private void spawnEnemyWave(int numEnemies){
        for(int i = 0; i < numEnemies; i++){
            Instantiate(enemyPrefab, getRandomPos(),enemyPrefab.transform.rotation);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position,spawnRange);
    }
}
