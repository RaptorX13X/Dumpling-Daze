using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;

public class ComicSystem : MonoBehaviour
{
    [SerializeField] private RawImage[] comicStrips;
    [SerializeField] private CanvasGroup comicMain;

    [SerializeField] private PlayerMovement playerMove;
    [SerializeField] private MouseLook playerMouse;

    [SerializeField] private string comicMusicEvent = "event:/ComicMusic";
    private EventInstance comicMusicInstance;

    [SerializeField] private RadioManager radioManager; // Referencja do RadioManager

    private Color targetColor = Color.white;

    private Sequence alphaChangeSequence;

    public bool comicEnded;

    [SerializeField] private PlayerStats playerStatsSO;


    private void Awake()
    {
        if (playerStatsSO.firstDayDone)
        {
            comicEnded = true;
            enabled = false;
            comicMain.DOFade(0f, 0.01f);
        }
    }

    private void Start()
    {
        ChangeAlphaSequence();

        playerMouse.enabled = false;
        playerMove.enabled = false;

        // Start the comic music
        comicMusicInstance = RuntimeManager.CreateInstance(comicMusicEvent);
        comicMusicInstance.start();
        comicEnded = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SkipFade();
        }
    }

    void ChangeAlphaSequence()
    {
        alphaChangeSequence = DOTween.Sequence();

        foreach (RawImage img in comicStrips)
        {
            alphaChangeSequence.Append(img.DOColor(targetColor, 2f));
            alphaChangeSequence.AppendInterval(2f);
        }

        alphaChangeSequence.Play();

        // Add a callback at the end of the sequence to hide the canvas
        alphaChangeSequence.OnComplete(() => HideComic());
    }

    void SkipFade()
    {
        if (alphaChangeSequence != null && alphaChangeSequence.IsActive() && alphaChangeSequence.IsPlaying())
        {
            alphaChangeSequence.Complete(true); // Complete the current tween and move to the next one
        }
    }

    void HideComic()
    {
        comicMain.DOFade(0f, 1f);

        // Stop the comic music
        comicMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        comicMusicInstance.release();

        //Not necessary, the comic ends with a tutorial which also stops time.

        //playerMouse.enabled = true;
        //playerMove.enabled = true;

        // Start the radio music after the comic ends
        if (radioManager != null)
        {
            radioManager.StartRadioMusic();
        }

        comicEnded = true;
    }
}
