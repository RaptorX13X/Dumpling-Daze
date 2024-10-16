using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeAnimation : MonoBehaviour
{
    private Animator mAnimator;
    private bool inRange = false;
    private bool isOpen = false;

    [FMODUnity.EventRef]
    public string openSoundEvent;  // Event dla d�wi�ku otwierania
    public string closeSoundEvent; // Event dla d�wi�ku zamykania

    private FMOD.Studio.EventInstance openSoundInstance;
    private FMOD.Studio.EventInstance closeSoundInstance;

    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();

        // Inicjalizacja event�w d�wi�kowych
        openSoundInstance = FMODUnity.RuntimeManager.CreateInstance(openSoundEvent);
        closeSoundInstance = FMODUnity.RuntimeManager.CreateInstance(closeSoundEvent);

        // Dodanie obserwator�w zdarze� animacji
        AnimationEvent openEvent = new AnimationEvent();
        openEvent.functionName = "PlayOpenSound";
        openEvent.time = 0.5f;  // Ustaw czas zdarzenia w zale�no�ci od animacji
        AnimationEvent closeEvent = new AnimationEvent();
        closeEvent.functionName = "PlayCloseSound";
        closeEvent.time = 0.5f; // Ustaw czas zdarzenia w zale�no�ci od animacji

        AnimationClip animationClip = mAnimator.runtimeAnimatorController.animationClips[0]; // Za��my, �e masz jedn� animacj�
        animationClip.AddEvent(openEvent);
        animationClip.AddEvent(closeEvent);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            inRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mAnimator != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && inRange)
            {
                if (isOpen == false)
                {
                    mAnimator.SetTrigger("Open");
                    isOpen = true;
                }
                else if (isOpen == true)
                {
                    mAnimator.SetTrigger("Close");
                    isOpen = false;
                }
            }
        }
    }

    // Odtwarzanie d�wi�ku otwierania
    void PlayOpenSound()
    {
        if (openSoundInstance.isValid())
        {
            openSoundInstance.start();
        }
    }

    // Odtwarzanie d�wi�ku zamykania
    void PlayCloseSound()
    {
        if (closeSoundInstance.isValid())
        {
            closeSoundInstance.start();
        }
    }
}

