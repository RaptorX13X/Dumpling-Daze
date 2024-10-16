using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageChange : MonoBehaviour
{
    [SerializeField] private GameObject MainPage;
    [SerializeField] private GameObject OtherPage;
    [SerializeField] private ComputerTrigger computerScript;

    private void OnMouseDown()
    {
        if (computerScript.isComputering == true)
        {
            MainPage.transform.position = new Vector3(5f, -5f, 0f);
            OtherPage.transform.position = new Vector3(5f, 2.5f, 0f);
        }
    }
}
