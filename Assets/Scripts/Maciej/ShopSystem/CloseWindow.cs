using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWindow : MonoBehaviour
{
    [SerializeField] private ComputerTrigger computerScript;
    [SerializeField] private Canvas app;

    private void OnMouseDown()
    {
        if (computerScript.isComputering)
        {
            app.enabled = false;
        }
    }
}
