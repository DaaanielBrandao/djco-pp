using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour {
	public int maxAlive = 5;
	public float cooldown = 2.0f;
	private float cooldownRemaining = 0;

	public abstract void OnWave(int waveNumber);
	
	protected abstract List<GameObject> DoSpawn();

	protected List<GameObject> aliveObjs = new List<GameObject>();
	
	private void Update()
	{
		cooldownRemaining -= Time.deltaTime;
		if (cooldownRemaining <= 0) {
			aliveObjs.RemoveAll(item => item == null);

 
			if (aliveObjs.Count < maxAlive)
				aliveObjs.AddRange(DoSpawn());


			cooldownRemaining = cooldown; // += ?
		}

	}

	protected int NumSpawnable() {
		return maxAlive - aliveObjs.Count;
	}

	public void ClearSpawned() {
		foreach (GameObject obj in aliveObjs)
			if (obj)
				Destroy(obj);

		aliveObjs = new List<GameObject>();
	}

	protected void ResetCooldown() {
		cooldownRemaining = 0;
	}

	public List<GameObject> GetAliveObjects()
	{
		return aliveObjs;
	}
}