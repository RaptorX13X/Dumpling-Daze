using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using DG.Tweening;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class CustomerSpawner : MonoBehaviour
{
    [SerializeField] Transform exitPoint; // Reference to the exit point
    [SerializeField] private GameObject[] customerPrefabs; // Array of customer prefabs
    [SerializeField] private float spawnInterval = 30f; // Time in seconds between customers
    [SerializeField] private float spawnerDownRange = 10f;
    [SerializeField] private float spawnerUpperRange = 20f;


    [SerializeField] private bool isDayStarted = false;
    [SerializeField] private bool canStartDay;
    [SerializeField] private bool canSpawnCustomers = false;

    [SerializeField] private CanvasGroup textStartCanvas;
    [SerializeField] private CanvasGroup textEndCanvas;

    [SerializeField] private UnityEvent DayStarted;

    public List<Customer> customers;

    [SerializeField] private SummaryScreen summaryScreen;

    private bool emptyRestaurant;

    private void Start()
    {
        canStartDay = true;
    }

    public void StartDayRemote()
    {
        if (canStartDay)
        {
            isDayStarted = true;
        }
        if (isDayStarted && canStartDay)
        {
            CustomersIncoming();
            StartDay();
            DayStartedText();

            DayStarted.Invoke();

            canStartDay = false;
        }
    }

    private void CustomersIncoming()
    {
        InvokeRepeating("SpawnCustomer", 0f, spawnInterval);
    }

    private void DayStartedText()
    {
        textStartCanvas.DOFade(1f, 1f);
        textStartCanvas.transform.DOScale(1.5f, 1f).OnComplete(() => {
            textStartCanvas.transform.DOScale(0f, 1f); 
        });
    }

    void StartDay()
    {
        canSpawnCustomers = true;
    }

    private void DayEndedText()
    {
        textEndCanvas.DOFade(1f, 1f);
        textEndCanvas.transform.DOScale(1.5f, 1f).OnComplete(() => {
            textEndCanvas.transform.DOScale(0f, 1f);
        });
    }

    public void EndDay()
    {
        canSpawnCustomers = false;
        StartCoroutine(WaitForEnd());
        

        DayEndedText();
    }

    private void Update()
    {
        if (customers.Count > 0)
        {
            emptyRestaurant = false;
        }
        else
        {
            emptyRestaurant = true;
        }

        foreach (Customer customer in customers)
        {
            if (customer == null)
                customers.Remove(customer);
        }
    }

    private IEnumerator WaitForEnd()
    {
        while (!emptyRestaurant)
        {
            yield return new WaitForSeconds(0.1f);
        }
        summaryScreen.DayFinished();
    }

    private void SpawnCustomer()
    {
        if (canSpawnCustomers)
        {
            // Randomly select a customer prefab from the array
            GameObject customerPrefab = customerPrefabs[Random.Range(0, customerPrefabs.Length)];

            // Instantiate the selected customer prefab
            GameObject customer = Instantiate(customerPrefab, transform.position, Quaternion.identity);

            // Get the Customer script component
            Customer customerScript = customer.GetComponent<Customer>();

            if (customerScript != null)
            {
                // Set the exit point for the customer
                customerScript.SetExitPoint(exitPoint);
            }
            customers.Add(customerScript);
        }
    }
}
