using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EndOfDayStats", menuName = "Stats/EndOfDay")]
public class EndOfDayStatsSO : ScriptableObject
{
    public float startingMoney;
    public float endOfDayMoney;
    public int happyCustomers;
    public int unhappyCustomers;
    public int dumplingsMade;
}
