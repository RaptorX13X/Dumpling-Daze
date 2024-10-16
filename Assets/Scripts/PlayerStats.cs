using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/Stats", fileName = "PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [Range(100, 1500)] public float mouseSensitivity;
    public float minValue = 300;
    public float maxValue = 1500;

    public bool firstDayDone = false;
}
