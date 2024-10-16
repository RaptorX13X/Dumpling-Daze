using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class DropDownTest : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] RawImage shutdownScreen;

    public void OnDropdownValueChanged()
    {
        int selectedOption = dropdown.value;

        switch (selectedOption)
        {
            case 0:
                Debug.Log("Exit Option Chosen");

                shutdownScreen.DOFade(1f, 1f);

                // Perform actions for option 1
                break;
            case 1:
                Debug.Log("Option 2 chosen");
                // Perform actions for option 2
                break;
            // Add cases for other options as needed
            case 2:
                Debug.Log("Option 3 chosen");
                // Perform actions for option 2
                break;
            default:
                Debug.LogWarning("Unhandled option chosen");
                break;
        }
    }
}
