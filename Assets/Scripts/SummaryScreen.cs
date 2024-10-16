using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SummaryScreen : MonoBehaviour
{
    [SerializeField] private EndOfDayStatsSO endOfDayStatsSo;
    [SerializeField] private MoneySO moneySo;
    [SerializeField] private GameObject dayFinishedUI;
    [SerializeField] private TextMeshProUGUI money;
    [SerializeField] private TextMeshProUGUI dumplings;
    [SerializeField] private TextMeshProUGUI happy;
    [SerializeField] private TextMeshProUGUI unhappy;
    private float endMoney;
    [SerializeField] public int menuSceneNumber;
    [SerializeField] private PlayerMovement player;

    [Header("FMOD Events")]
    [FMODUnity.EventRef] public string endSound;

    public bool gameFinished;

    [SerializeField] private PlayerStats playerStatsSO;
    [SerializeField] private GameObject crosshair;
    private void Start()
    {
        endOfDayStatsSo.startingMoney = moneySo.currentMoney;
        dayFinishedUI.SetActive(false);
        endOfDayStatsSo.happyCustomers = 0;
        endOfDayStatsSo.unhappyCustomers = 0;
        endOfDayStatsSo.dumplingsMade = 0;
        gameFinished = false;
    }
    public void DayFinished()
    {
        endOfDayStatsSo.endOfDayMoney = moneySo.currentMoney;
        endMoney = endOfDayStatsSo.endOfDayMoney - endOfDayStatsSo.startingMoney;
        dayFinishedUI.SetActive(true);
        dumplings.text = endOfDayStatsSo.dumplingsMade.ToString();
        happy.text = endOfDayStatsSo.happyCustomers.ToString();
        unhappy.text = endOfDayStatsSo.unhappyCustomers.ToString();
        endMoney = endOfDayStatsSo.endOfDayMoney - endOfDayStatsSo.startingMoney;
        money.text = endMoney + "$";
        money.color = endMoney > 0 ? Color.green : Color.red;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0f;
        player.enabled = false;
        crosshair.SetActive(false);
        RuntimeManager.PlayOneShot(endSound, transform.position);
        gameFinished = true;
        // remember to invoke it :)
    }

    public void MenuButton()
    {
        SceneManager.LoadScene(menuSceneNumber);
    }

    public void NextDayButton()
    {
        playerStatsSO.firstDayDone = true;
        SceneManager.LoadScene(1);
    }
}
