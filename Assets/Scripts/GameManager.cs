using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public enum GameState {
		Menu,
		Starting,
		Fight,
		Shopping
	}

	public GameState gameState = GameState.Menu;

	public GameObject enemySpawner;
	public GameObject ammoSpawner;
	
	void Start()
	{
		
	}





	// Update is called once per frame
	void Update() {
		enemySpawner.SetActive(gameState == GameState.Fight);
		ammoSpawner.SetActive(gameState == GameState.Fight);
		
		switch (gameState) {
			case GameState.Menu:
				if (Input.GetKeyDown(KeyCode.M)) {
					gameState = GameState.Starting;
					Debug.Log("Starting Wave...");
				}

				break;
			case GameState.Starting:
				// Tell spawners to begin

				enemySpawner.GetComponent<SpawnManager>().AdvanceWave();
				gameState = GameState.Fight;
				break;
			case GameState.Fight:
				// Ask spawners enemy count (they know what they spawn)
				if (enemySpawner.GetComponent<SpawnManager>().IsWaveDone()) {
					gameState = GameState.Shopping;
					Debug.Log("Ending wave!");
				}
				break;
			case GameState.Shopping:
				if (Input.GetKeyDown(KeyCode.S)) {
					gameState = GameState.Starting;
					Debug.Log("Starting Wave...");
				}

				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}
}