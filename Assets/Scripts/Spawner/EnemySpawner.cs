using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

	public bool spawnOnGround = false;
	public GameObject enemyPrefab;

	public int[] spawnAmounts;
	
	private Collider2D[] areas;
	private int enemiesLeft;
	
	// Start is called before the first frame update
	private void Start() {
		areas = GetComponents<Collider2D>();
	}
	
	public int OnWave(int waveNumber)
	{
		enemiesLeft = spawnAmounts[Math.Min(waveNumber - 1, spawnAmounts.Length - 1)];

		return enemiesLeft;
	}

	public GameObject SpawnEnemy()
	{
		enemiesLeft--;
		
		Vector2 randomPos = areas.GetValidSpawnPosition();

		if (spawnOnGround) {
			return GroundFinder.SpawnOnGround(enemyPrefab, randomPos);
		}

		return Instantiate(enemyPrefab, randomPos, enemyPrefab.transform.rotation);
	}

	public bool CanSpawnEnemies()
	{
		return enemiesLeft > 0;
	}
}