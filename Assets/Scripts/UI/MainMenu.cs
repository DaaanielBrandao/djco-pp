using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject defaultMain;
    public GameObject defaultOptions;
    private GameObject lastSelected;
    public GameObject screenShakeToggle;
    public GameObject speedRunClockToggle;
    public bool awakening;
    
    private void Awake()
    {
        awakening = true;
        screenShakeToggle.GetComponent<Toggle>().isOn = (PlayerPrefs.GetInt("ScreenShake",1) != 0);
        speedRunClockToggle.GetComponent<Toggle>().isOn = (PlayerPrefs.GetInt("SpeedRunClock",1) != 0);
        awakening = false;
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
            EventSystem.current.SetSelectedGameObject(lastSelected);
        lastSelected = EventSystem.current.currentSelectedGameObject;
    }

    public void ToMainMenu()
    {
        EventSystem.current.SetSelectedGameObject(defaultMain);
        lastSelected = defaultMain;
    }

    public void ToOptionsMenu()
    {
        EventSystem.current.SetSelectedGameObject(defaultOptions);
        lastSelected = defaultOptions;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void ToggleScreenShake()
    {
        if (!awakening)
        {
            if (PlayerPrefs.GetInt("ScreenShake", 1) != 0)
                PlayerPrefs.SetInt("ScreenShake", 0);
            else PlayerPrefs.SetInt("ScreenShake", 1);
        }

    }

    public void Toggle(String toggle)
    {
        if (!awakening)
        {
            if (PlayerPrefs.GetInt(toggle, 1) != 0)
                PlayerPrefs.SetInt(toggle, 0);
            else PlayerPrefs.SetInt(toggle, 1);
        }
    }
}
