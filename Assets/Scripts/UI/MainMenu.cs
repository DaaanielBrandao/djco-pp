﻿using System;
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
    public GameObject defaultHTP;
    private GameObject lastSelected;
    public GameObject screenShakeToggle;
    public GameObject speedRunClockToggle;
    public GameObject GMEModeToggle;
    public GameObject volumeSlider;
    public bool awakening;
    
    private void Awake()
    {
        awakening = true;
        screenShakeToggle.GetComponent<Toggle>().isOn = (PlayerPrefs.GetInt("ScreenShake",1) != 0);
        speedRunClockToggle.GetComponent<Toggle>().isOn = (PlayerPrefs.GetInt("SpeedRunClock",1) != 0);
        GMEModeToggle.GetComponent<Toggle>().isOn = (PlayerPrefs.GetInt("GMEMode",0) != 0);
        volumeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Volume",1f);
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

    public void ToHowToPlay()
    {
        EventSystem.current.SetSelectedGameObject(defaultHTP);
        lastSelected = defaultHTP;
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

    public void updateVolume(float value)
    {
        PlayerPrefs.SetFloat("Volume", value);
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
    
    public void ToggleDefZero(String toggle)
    {
        if (!awakening)
        {
            if (PlayerPrefs.GetInt(toggle, 0) != 0)
                PlayerPrefs.SetInt(toggle, 0);
            else PlayerPrefs.SetInt(toggle, 1);
        }
    }
}
