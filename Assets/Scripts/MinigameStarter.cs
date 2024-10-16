using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using Random = UnityEngine.Random;

public class MinigameStarter : MonoBehaviour
{
    //[SerializeField] private GameObject choppingStartPrefab;
    //[SerializeField] private GameObject choppingCompletePrefab;
    //[SerializeField] private GameObject rollingStartPrefab;
    //[SerializeField] private GameObject rollingCompletePrefab;
    //[SerializeField] private GameObject gluingStartPrefab;
    //[SerializeField] private GameObject gluingCompletePrefab;
    //[SerializeField] private GameObject mixing2StartPrefab;
    //[SerializeField] private GameObject mixing2CompletePrefab;
    //[SerializeField] private GameObject stuffingStartPrefab;
    //[SerializeField] private GameObject stuffingCompletePrefab;
    [SerializeField] private MinigameManager choppingMinigame;
    [SerializeField] private RollingMinigame rollingMinigame;
    [SerializeField] private GluingMinigame gluingMinigame;
    [SerializeField] private Mixing2Minigame mixing2Minigame;
    [SerializeField] private StuffingMinigame stuffingMinigame;
    private bool hitDetect;
    [SerializeField] private GameObject placingSpot;
    [SerializeField] private GameObject playerSpot;
    private GameObject currentCollision;
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private EndOfDayStatsSO endOfDayStatsSo;
    //[SerializeField] private GameObject minigameCanvas;

    // FMOD events for minigame sounds
    [FMODUnity.EventRef]
    public string choppingCompleteSoundEvent;
    [FMODUnity.EventRef]
    public string rollingCompleteSoundEvent;
    [FMODUnity.EventRef]
    public string gluingCompleteSoundEvent;
    [FMODUnity.EventRef]
    public string mixingCompleteSoundEvent;
    [FMODUnity.EventRef]
    public string stuffingCompleteSoundEvent;
    
    private int randomMinigame;
    private int previousMinigame = -1;
    private int firstMinigame = -1;



    private void Update()
    {
        //playerMovement.enabled = !MinigameInProgress();
    }

    public void ChoppingFinished()
    {
        choppingMinigame.gameObject.SetActive(false);
        endOfDayStatsSo.dumplingsMade += 1;
        MovementScript();
        //currentCollision.SetActive(false);
        //Instantiate(choppingCompletePrefab, placingSpot.transform.position, choppingCompletePrefab.transform.rotation);

        // Play FMOD sound for chopping completion
        FMODUnity.RuntimeManager.PlayOneShot(choppingCompleteSoundEvent, placingSpot.transform.position);
    }

    public void RollingFinished()
    {
        rollingMinigame.gameObject.SetActive(false);
        endOfDayStatsSo.dumplingsMade += 1;
        MovementScript();
        //currentCollision.SetActive(false);
        //Instantiate(rollingCompletePrefab, placingSpot.transform.position, rollingCompletePrefab.transform.rotation);

        // Play FMOD sound for rolling completion
        FMODUnity.RuntimeManager.PlayOneShot(rollingCompleteSoundEvent, placingSpot.transform.position);
    }

    public void GluingFinished()
    {
        
        gluingMinigame.gameObject.SetActive(false);
        endOfDayStatsSo.dumplingsMade += 1;
        MovementScript();
        //currentCollision.SetActive(false);
        //Instantiate(gluingCompletePrefab, placingSpot.transform.position, gluingCompletePrefab.transform.rotation);

        // Play FMOD sound for gluing completion
        FMODUnity.RuntimeManager.PlayOneShot(gluingCompleteSoundEvent, placingSpot.transform.position);
    }
    
    public void Mixing2Finished()
    {
        mixing2Minigame.gameObject.SetActive(false);
        endOfDayStatsSo.dumplingsMade += 1;
        MovementScript();
        //currentCollision.SetActive(false);
        //Instantiate(mixing2CompletePrefab, placingSpot.transform.position, mixing2CompletePrefab.transform.rotation);

        // Play FMOD sound for gluing completion
        FMODUnity.RuntimeManager.PlayOneShot(mixingCompleteSoundEvent, placingSpot.transform.position);
    }
    public void StuffingFinished()
    {
        stuffingMinigame.gameObject.SetActive(false);
        endOfDayStatsSo.dumplingsMade += 1;
        MovementScript();
        //currentCollision.SetActive(false);
        //Instantiate(stuffingCompletePrefab, placingSpot.transform.position, stuffingCompletePrefab.transform.rotation);

        // Play FMOD sound for gluing completion
        FMODUnity.RuntimeManager.PlayOneShot(stuffingCompleteSoundEvent, placingSpot.transform.position);
    }
   

    public void StartNewMinigame()
    {
        if (previousMinigame != -1)
        {
            firstMinigame = previousMinigame;
        }
        playerMovement.transform.position = playerSpot.transform.position;
        //playerMovement.enabled = false;
        MovementScript();
        int newMinigame;
        do
        {
            newMinigame = Random.Range(0, 5);
        } while (newMinigame == previousMinigame && newMinigame == firstMinigame);
        
        randomMinigame = newMinigame;
        previousMinigame = randomMinigame;
       

        switch (randomMinigame)
        {
            case 0:
                stuffingMinigame.gameObject.SetActive(true);
                break;
            case 1:
                mixing2Minigame.gameObject.SetActive(true);
                break;
            case 2:
                gluingMinigame.gameObject.SetActive(true);
                break;
            case 3:
                rollingMinigame.gameObject.SetActive(true);
                break;
            case 4:
                choppingMinigame.gameObject.SetActive(true);
                break;
        }
    }

    public bool MinigameInProgress()
    {
        if (stuffingMinigame.gameObject.activeInHierarchy || mixing2Minigame.gameObject.activeInHierarchy ||
            gluingMinigame.gameObject.activeInHierarchy || rollingMinigame.gameObject.activeInHierarchy ||
            choppingMinigame.gameObject.activeInHierarchy)
            return true;
        return false;
    }

    private void MovementScript()
    {
        playerMovement.enabled = !playerMovement.enabled;
    }
}
