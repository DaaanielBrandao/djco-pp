using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{

    private enum LightState { Red, Yellow, Green }
    private LightState lightState;

    public GameObject green;
    public GameObject yellow;
    public GameObject red;

    private SpriteRenderer greenRenderer;
    private SpriteRenderer yellowRenderer;
    private SpriteRenderer redRenderer;

    private GameObject lightGreen;
    private GameObject lightYellow;
    private GameObject lightRed;

    private GameObject tooltip;
    private GameManager gameManager;
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        greenRenderer = green.GetComponent<SpriteRenderer>();
        yellowRenderer = yellow.GetComponent<SpriteRenderer>();
        redRenderer = red.GetComponent<SpriteRenderer>();
        lightGreen = green.transform.GetChild (0).gameObject;
        lightYellow = yellow.transform.GetChild (0).gameObject;
        lightRed = red.transform.GetChild (0).gameObject;

        lightState = LightState.Red;

        gameManager = transform.parent.GetComponent<GameManager>();
        
        tooltip = transform.Find("Tooltip").gameObject;
        tooltip.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        greenRenderer.color = lightState == LightState.Green ? Color.white : Color.grey;
        yellowRenderer.color = lightState == LightState.Yellow ? Color.white : Color.grey;
        redRenderer.color = lightState == LightState.Red ? Color.white : Color.grey;
        lightGreen.SetActive(lightState == LightState.Green);
        lightYellow.SetActive(lightState == LightState.Yellow);
        lightRed.SetActive(lightState == LightState.Red);
        
        if (player && Input.GetKeyDown(KeyCode.K)) {
            gameManager.OnTrafficLightPush();
        }
        
        tooltip.SetActive(player && lightState == LightState.Red);
    }
    

    private void TurnGreen() {
        lightState = LightState.Green;
    }

    private void TurnYellow() {
        lightState = LightState.Yellow;
    }

    private void TurnRed() {
        lightState = LightState.Red;
    }

    public void StartWave() {
        TurnYellow();
        StartCoroutine(WaitToSwitch(LightState.Green));
    }

    public void EndWave() {
        TurnYellow();
        StartCoroutine(WaitToSwitch(LightState.Red));
    }

    IEnumerator WaitToSwitch(LightState newState)
    {
        yield return new WaitForSeconds(1f);
        lightState = newState;
    }
    
    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            player = other.gameObject;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            player = null;
        }
    }

}
