using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    public GameObject defaultMain;
    public GameObject infoTextWave;
    public GameObject infoTextTime;
    public GameObject infoTextScore;
    private GameObject lastSelected;
    private TextMeshProUGUI tmpInfoWave;
    private TextMeshProUGUI tmpInfoTime;
    private TextMeshProUGUI tmpInfoScore;

    private void Start()
    {
        tmpInfoWave = infoTextWave.GetComponent<TextMeshProUGUI>();
        tmpInfoTime = infoTextTime.GetComponent<TextMeshProUGUI>();
        tmpInfoScore = infoTextScore.GetComponent<TextMeshProUGUI>();
        tmpInfoWave.text = "wave <color=#FF6B73> " + GameInfo.waveNum;
        //tmpInfoTime.text = "time <color=#FF6B73> " + GameInfo.time;
        tmpInfoScore.text = "score <color=#FF6B73> " + GameInfo.score;
        
        int milliseconds = (int)(GameInfo.time * 1000) % 1000;
        int seconds = (int) GameInfo.time % 60;
        int minutes = (int) GameInfo.time / 60;
		
        //	timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        tmpInfoTime.text = "time <color=#FF6B73> " +  $"{minutes:00}:{seconds:00}:{milliseconds:000}";
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
            EventSystem.current.SetSelectedGameObject(lastSelected);
        lastSelected = EventSystem.current.currentSelectedGameObject;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
