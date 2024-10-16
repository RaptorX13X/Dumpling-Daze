using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Canvases")] 
    [SerializeField] private GameObject pauseMenuCanvas;
    [SerializeField] private GameObject settingsMenuCanvas;
    [SerializeField] private GameObject audioMenuCanvas;
    [SerializeField] private GameObject videoMenuCanvas;
    [SerializeField] private GameObject controlsMenuCanvas;
    [SerializeField] private GameObject controls2MenuCanvas;
    [SerializeField] private GameObject controls3MenuCanvas;
    [SerializeField] private int gameSceneBuildNumber = 0;
    
    [Header("Other")]
    [SerializeField] private PlayerMovement player;
    [SerializeField] private GameObject cursor;
    [SerializeField] private MinigameStarter minigameStarter;
    [SerializeField] private SummaryScreen summaryScreen;
    [SerializeField] private ControlInstructions tutorial;
    [SerializeField] private ComicSystem comic;
    
    private bool paused;

    [SerializeField] private RadioManager radio;
    
    private void Awake()
    {
        cursor.SetActive(true);
        paused = false;
        Time.timeScale = 1f;
    }
    private void Start()
    {
        pauseMenuCanvas.SetActive(false);
        settingsMenuCanvas.SetActive(false);
        audioMenuCanvas.SetActive(false);
        videoMenuCanvas.SetActive(false);
        controlsMenuCanvas.SetActive(false);
        controls2MenuCanvas.SetActive(false);
        controls3MenuCanvas.SetActive(false);
    }

    private void Pause()
    {
        pauseMenuCanvas.SetActive(true);
        settingsMenuCanvas.SetActive(false);
        audioMenuCanvas.SetActive(false);
        videoMenuCanvas.SetActive(false);
        controlsMenuCanvas.SetActive(false);
        controls2MenuCanvas.SetActive(false);
        controls3MenuCanvas.SetActive(false);
    }

    private void Unpause()
    {
        pauseMenuCanvas.SetActive(false);
        settingsMenuCanvas.SetActive(false);
        audioMenuCanvas.SetActive(false);
        videoMenuCanvas.SetActive(false);
        controlsMenuCanvas.SetActive(false);
        controls2MenuCanvas.SetActive(false);
        controls3MenuCanvas.SetActive(false);
    }
    private void Update()
    {
        if (minigameStarter.MinigameInProgress() || summaryScreen.gameFinished || !comic.comicEnded || !tutorial.tutorialEnded)
            return;
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            Time.timeScale = 0f;
            Pause();
            cursor.SetActive(false);
            paused = true;
            Cursor.lockState = CursorLockMode.Confined;
            player.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            Time.timeScale = 1f;
            Unpause();
            cursor.SetActive(true);
            paused = false;
            Cursor.lockState = CursorLockMode.Locked;
            player.enabled = true;
        }
    }

    public void BackToMenu()
    {
        pauseMenuCanvas.SetActive(true);
        settingsMenuCanvas.SetActive(false);
        audioMenuCanvas.SetActive(false);
        videoMenuCanvas.SetActive(false);
    }

    public void BackToSettings()
    {
        pauseMenuCanvas.SetActive(false);
        settingsMenuCanvas.SetActive(true);
        audioMenuCanvas.SetActive(false);
        videoMenuCanvas.SetActive(false);
        controlsMenuCanvas.SetActive(false);
        controls2MenuCanvas.SetActive(false);
        controls3MenuCanvas.SetActive(false);
    }

    public void QuitToMenu()
    {
        //radio.StopGameMusic();
        SceneManager.LoadScene(gameSceneBuildNumber);
    }

    public void AudioSettings()
    {
        pauseMenuCanvas.SetActive(false);
        settingsMenuCanvas.SetActive(false);
        audioMenuCanvas.SetActive(true);
        videoMenuCanvas.SetActive(false);
    }

    public void VideoSettings()
    {
        pauseMenuCanvas.SetActive(false);
        settingsMenuCanvas.SetActive(false);
        audioMenuCanvas.SetActive(false);
        videoMenuCanvas.SetActive(true);
        controlsMenuCanvas.SetActive(false);
    }

    public void Controls()
    {
        controlsMenuCanvas.SetActive(true);
        controls2MenuCanvas.SetActive(false);
        controls3MenuCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(false);
        settingsMenuCanvas.SetActive(false);
        audioMenuCanvas.SetActive(false);
        videoMenuCanvas.SetActive(false);
    }

    public void Controls2()
    {
        controlsMenuCanvas.SetActive(false);
        controls2MenuCanvas.SetActive(true);
        controls3MenuCanvas.SetActive(false);
    }

    public void Controls3()
    {
        controlsMenuCanvas.SetActive(false);
        controls2MenuCanvas.SetActive(false);
        controls3MenuCanvas.SetActive(true);
    }
    
}
