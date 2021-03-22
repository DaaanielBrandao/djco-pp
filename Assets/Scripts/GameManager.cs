﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public GameObject shopDoor;
	private enum GameState {
		Menu,
		Starting,
		Fight,
		Shopping
	}
	public int finalWaveNum;
	private int waveNumber = 0;
	private GameState gameState;

	private EnemySpawnManager spawnManager;
	private AmmoSpawner ammoManager;
	private PowerupSpawner powerUpManager;


	
	void Start()
	{
		GameInfo.isOver = false;
		GameInfo.waveNum = 0;
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
		if (allDead() && !GameInfo.isOver)
		{
			Debug.Log("alldead");
			GameInfo.isOver = true;
			StartCoroutine(EndGame("GameOver"));
		}
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
		if (waveNumber == (finalWaveNum))
		{
			GameInfo.isOver = true;
			Debug.Log("win!");
			StartCoroutine(EndGame("GameWin"));
		}
		gameState = GameState.Shopping;
		spawnManager.ClearSpawned();
		ammoManager.ClearSpawned();
		powerUpManager.ClearSpawned();
		
		shopDoor.SetActive(false);
		refilPlayersResources();
		FindObjectOfType<ShopManager>().RefreshShop();


		GameObject.Find("HUD").GetComponent<HUD>().announceEndWave();
		TrafficLight[] trafficLights = FindObjectsOfType<TrafficLight>();
		foreach (TrafficLight trafficLight in trafficLights)
		{
			trafficLight.endWave();
		}
		
		Debug.Log("Ending wave!");
	}

	private void StartWave()
	{
		waveNumber++;
		GameInfo.waveNum = waveNumber;
		spawnManager.OnWave(waveNumber);
		ammoManager.OnWave(waveNumber);
		powerUpManager.OnWave(waveNumber);
		
		shopDoor.SetActive(true);
		refilPlayersResources(); // this is nice for testing the weapons you just bought without worrying about ammo 
		
		GameObject.Find("HUD").GetComponent<HUD>().announceWaveStart(waveNumber);
		GameObject.Find("Sky").GetComponent<SkyController>().changeColor(waveNumber);
		TrafficLight[] trafficLights = FindObjectsOfType<TrafficLight>();
		foreach (TrafficLight trafficLight in trafficLights)
		{
			trafficLight.startWave();
		}
		
		
		Debug.Log("Starting wave!");

	}

	private void refilPlayersResources()
	{
		PlayerHP[] playerHps = FindObjectsOfType<PlayerHP>();
		WeaponSwitch[] ammoSwitchers = FindObjectsOfType<WeaponSwitch>();
		
		foreach (PlayerHP playerHp in playerHps)
		{
			playerHp.RefillHp();
		}

		foreach (WeaponSwitch ammoSwitcher in ammoSwitchers)
		{
			ammoSwitcher.RefillAmmo();
		}
	}

	private bool allDead()
	{
		PlayerHP[] playerHps = FindObjectsOfType<PlayerHP>();
		return playerHps.Length == 0;
	}
	
	IEnumerator EndGame(String scene) {
		yield return new WaitForSeconds(3f);
		SceneManager.LoadScene(scene);      
	}
}