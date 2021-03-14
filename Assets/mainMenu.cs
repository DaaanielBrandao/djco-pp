using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public GameObject defaultMain;
    public GameObject defaultOptions;
    private GameObject lastSelected;
    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
            EventSystem.current.SetSelectedGameObject(lastSelected);
        lastSelected = EventSystem.current.currentSelectedGameObject;
    }

    public void MainMenu()
    {
        EventSystem.current.SetSelectedGameObject(defaultMain);
        lastSelected = defaultMain;
    }

    public void OptionsMenu()
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
}
