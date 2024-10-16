using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SensitivitySlider : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        slider.minValue = playerStats.minValue;
        slider.maxValue = playerStats.maxValue;
        slider.value = playerStats.mouseSensitivity;
        text.text = playerStats.mouseSensitivity.ToString();
    }

    public void SetSoundsVolumeFromSlider(float sensitivity)
    {
        playerStats.mouseSensitivity = sensitivity;
        text.text = playerStats.mouseSensitivity.ToString();
    }
}
