using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveVariables
{
    public float money;
    public int happyCustomersVar;
    public int unhappyCustomersVar;
    public int dumplingsMadeVar;

    public SaveVariables (MoneySO moneySO, EndOfDayStatsSO statsSO)
    {
        money = moneySO.currentMoney;

        happyCustomersVar = statsSO.happyCustomers;
        unhappyCustomersVar = statsSO.unhappyCustomers;
        dumplingsMadeVar = statsSO.dumplingsMade;
    }
}
