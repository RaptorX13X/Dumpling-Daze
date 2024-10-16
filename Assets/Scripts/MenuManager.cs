using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Canvases")] 
    [SerializeField] private GameObject startMenuCanvas;
    [SerializeField] private GameObject settingsMenuCanvas;
    [SerializeField] private GameObject audioMenuCanvas;
    [SerializeField] private GameObject videoMenuCanvas;
    [SerializeField] private GameObject authorsMenuCanvas;
    [SerializeField] private GameObject controlsPg1MenuCanvas;
    [SerializeField] private GameObject controlsPg2MenuCanvas;
    [SerializeField] private GameObject controlsPg3MenuCanvas;
    [SerializeField] private int gameSceneBuildNumber = 2;
    [SerializeField] private bool inGame;

    private void Start()
    {
        if (inGame)
        {
            startMenuCanvas.SetActive(false);
        }
        else
        {
            startMenuCanvas.SetActive(true); 
        }
        settingsMenuCanvas.SetActive(false);
        audioMenuCanvas.SetActive(false);
        videoMenuCanvas.SetActive(false);
        authorsMenuCanvas.SetActive(false);
        controlsPg1MenuCanvas.SetActive(false);
        controlsPg2MenuCanvas.SetActive(false);
        controlsPg3MenuCanvas.SetActive(false);

        
    }
    public void StartButton()
    {
        SceneManager.LoadScene(gameSceneBuildNumber);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void SettingsButton()
    {
        startMenuCanvas.SetActive(false);
        settingsMenuCanvas.SetActive(true);
    }

    public void AudioButton()
    {
        settingsMenuCanvas.SetActive(false);
        audioMenuCanvas.SetActive(true);
    }

    public void VideoButton()
    {
        settingsMenuCanvas.SetActive(false);
        videoMenuCanvas.SetActive(true);
    }

    public void AuthorsButton()
    {
        startMenuCanvas.SetActive(false);
        authorsMenuCanvas.SetActive(true);
    }

 

    public void ControlsPg1Button()
    {
        settingsMenuCanvas.SetActive(false);
        controlsPg2MenuCanvas.SetActive(false);
        controlsPg1MenuCanvas.SetActive(true);
    }

    public void ControlsPg2Button()
    {
        controlsPg1MenuCanvas.SetActive(false);
        controlsPg3MenuCanvas.SetActive(false);
        controlsPg2MenuCanvas.SetActive(true);
    }

    public void ControlsPg3Button()
    {
        controlsPg2MenuCanvas.SetActive(false);
        controlsPg3MenuCanvas.SetActive(true);
    }

    public void ToMenuBackButton()
    {
        settingsMenuCanvas.SetActive(false);
        startMenuCanvas.SetActive(true);
        authorsMenuCanvas.SetActive(false);
      
    }

    public void ToSettingsBackButton()
    {
        controlsPg1MenuCanvas.SetActive(false);
        controlsPg2MenuCanvas.SetActive(false);
        controlsPg3MenuCanvas.SetActive(false);
        audioMenuCanvas.SetActive(false);
        videoMenuCanvas.SetActive(false);
        settingsMenuCanvas.SetActive(true);
    }
}
