using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

	public bool spawnOnGround = false;
	public GameObject enemyPrefab;
	
	private Collider2D[] areas;
	
	// Start is called before the first frame update
	private void Start() {
		areas = GetComponents<Collider2D>();
	}

	public GameObject SpawnEnemy()
	{
		Vector2 randomPos = areas.GetValidSpawnPosition();

		if (spawnOnGround) {
			return GroundFinder.SpawnOnGround(enemyPrefab, randomPos);
		}
		return Instantiate(enemyPrefab, randomPos, enemyPrefab.transform.rotation);
	}
}