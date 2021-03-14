using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.PostProcessing;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using DepthOfField = UnityEngine.Rendering.Universal.DepthOfField;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject pauseMenuUI;
    public GameObject HUD;

    public GameObject pfx;
    private Volume volume;

    private DepthOfField depthOfField;
    // Start is called before the first frame update
    void Start()
    {
        volume = pfx.GetComponent<Volume>();
        Debug.Log(volume);
        volume.profile.TryGet(out depthOfField);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        HUD.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
        depthOfField.active = false;
    }
    
    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        HUD.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
        depthOfField.active = true;
    }

    public void loadMenu()
    {
        Resume();
        Debug.Log("Loading Menu");
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
    
}
