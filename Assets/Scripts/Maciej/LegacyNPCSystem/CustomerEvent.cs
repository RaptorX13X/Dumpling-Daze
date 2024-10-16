using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomerEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent CustomerEnter;
    [SerializeField] private UnityEvent CustomerExit;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CustomerEnter.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CustomerExit.Invoke();
        }
    }
}
