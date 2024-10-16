using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    private int resNumber;

    private int resWidth = 1920;
    private int resHeight = 1080;
    
    [SerializeField] private TextMeshProUGUI resolutionText;

    private void Awake()
    {
        resNumber = 3;
        resolutionText.text = resWidth + " x " + resHeight;
    }

    private void ChangeDisplayText()
    {
        switch (resNumber)
        {
            case 0:
                resWidth = 1024;
                resHeight = 768;
                resolutionText.text = resWidth + " x " + resHeight;
                break;
            case 1:
                resWidth = 1200;
                resHeight = 1024;
                resolutionText.text = resWidth + " x " + resHeight;
                break;
            case 2:
                resWidth = 1366;
                resHeight = 768;
                resolutionText.text = resWidth + " x " + resHeight;
                break;
            case 3:
                resWidth = 1920;
                resHeight = 1080;
                resolutionText.text = resWidth + " x " + resHeight;
                break;
            case 4:
                resWidth = 2560;
                resHeight = 1440;
                resolutionText.text = resWidth + " x " + resHeight;
                break;
        }
    }

    public void ToggleLeft()
    {
        if (resNumber > 0)
        {
            resNumber -= 1;
        }
        else
        {
            resNumber = 4;
        }
        ChangeDisplayText();
    }

    public void ToggleRight()
    {
        if (resNumber < 4)
        {
            resNumber += 1;
        }
        else
        {
            resNumber = 0;
        }
        ChangeDisplayText();
    }

    public void ApplyButton()
    {
        Screen.SetResolution(resWidth, resHeight, Screen.fullScreen);
   
    }
}