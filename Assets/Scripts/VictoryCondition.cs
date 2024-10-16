using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryCondition : MonoBehaviour
{
    [SerializeField] private GameObject finishedProduct;
    [SerializeField] private GameObject placingPoint;
    [SerializeField] private GameObject victoryUI;

    private void Start()
    {
        victoryUI.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag(finishedProduct.tag))
        {
            collision.transform.position = placingPoint.transform.position;
            victoryUI.SetActive(true);
        }
    }
}
