using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppChange : MonoBehaviour
{
    [SerializeField] private ComputerTrigger computerScript;
    [SerializeField] private Canvas app;

    private bool isCanvasEnabled = true;

    private void OnMouseDown()
    {
        if (computerScript.isComputering)
        {
            // Toggle the enabled state of the canvas
            isCanvasEnabled = !isCanvasEnabled;
            app.enabled = isCanvasEnabled;
        }
    }
}
