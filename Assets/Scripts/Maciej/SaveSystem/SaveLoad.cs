using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour
{
    public MoneySO moneySO;
    public EndOfDayStatsSO endOfDayStatsSO;

    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerSpawn;

    [SerializeField] private TextMeshProUGUI saveText;
    [SerializeField] private TextMeshProUGUI loadText;
    [SerializeField] private Image loadImage;

    public void SaveMe()
    {
        SaveSystem.SaveMoney(moneySO, endOfDayStatsSO);

        saveText.DOFade(1f, 1f).OnComplete(() =>
        {
            saveText.DOFade(0f, 1f);
        });
    }

    public void LoadMe()
    {
        SaveVariables data = SaveSystem.LoadVariables();

        moneySO.currentMoney = data.money;
        endOfDayStatsSO.happyCustomers = data.happyCustomersVar;
        endOfDayStatsSO.unhappyCustomers = data.unhappyCustomersVar;
        endOfDayStatsSO.dumplingsMade = data.dumplingsMadeVar;

        loadText.DOFade(1f, 1f).OnComplete(() =>
        {
            player.transform.position = new Vector3(3 ,2 , -10);
            loadText.DOFade(0f, 1f);
        });

        loadImage.DOFade(1f, 1f).OnComplete(() =>
        {
            loadImage.DOFade(0f, 1f);
        });
    }
}
