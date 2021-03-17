using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject shopDoor;
	private enum GameState {
		Menu,
		Starting,
		Fight,
		Shopping
	}
	private int waveNumber = 0;
	private GameState gameState;

	private EnemySpawnManager spawnManager;
	private AmmoSpawner ammoManager;
	private PowerupSpawner powerUpManager;

	
	void Start()
	{
		gameState = GameState.Menu;
		spawnManager = GetComponentInChildren<EnemySpawnManager>(true);
		ammoManager = GetComponentInChildren<AmmoSpawner>(true);
		powerUpManager = GetComponentInChildren<PowerupSpawner>(true);
	}

	// Update is called once per frame
	void Update() {
		spawnManager.gameObject.SetActive(gameState == GameState.Fight);
		ammoManager.gameObject.SetActive(gameState == GameState.Fight);
		powerUpManager.gameObject.SetActive(gameState == GameState.Fight);
		
		switch (gameState) {
			case GameState.Menu:
				if (Input.GetKeyDown(KeyCode.Z)) {
					waveNumber = 0;
					gameState = GameState.Starting;
				}

				break;
			case GameState.Starting:
				StartWave();

				gameState = GameState.Fight;
				break;
			case GameState.Fight:
				// Ask spawners enemy count (they know what they spawn)
				if (spawnManager.IsWaveDone()) {
					EndWave();
				}
				break;
			case GameState.Shopping:
				if (Input.GetKeyDown(KeyCode.Z)) {
					gameState = GameState.Starting;
				}

				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	private void EndWave()
	{
		gameState = GameState.Shopping;
		spawnManager.ClearSpawned();
		ammoManager.ClearSpawned();
		powerUpManager.ClearSpawned();
		
		shopDoor.SetActive(false);


		GameObject.Find("HUD").GetComponent<HUD>().announceEndWave();
		
		Debug.Log("Ending wave!");
	}

	private void StartWave()
	{
		waveNumber++;
		
		spawnManager.OnWave(waveNumber);
		ammoManager.OnWave(waveNumber);
		powerUpManager.OnWave(waveNumber);
		
		shopDoor.SetActive(true);


		GameObject.Find("HUD").GetComponent<HUD>().announceWaveStart(waveNumber);
		GameObject.Find("Sky").GetComponent<skyController>().changeColor(waveNumber);
		
		Debug.Log("Starting wave!");

	}
}