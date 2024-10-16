using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using FMODUnity;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public float timeToLeave = 10f; // Time in seconds before the customer leaves
    [SerializeField] private float timeToDespawn = 5f;
    [SerializeField] private DishTrigger dishTrigger;
    private Transform exitPoint;

    [SerializeField] private Animator walkAnimator;
    [SerializeField] private MeshRenderer faceRenderer;
    [SerializeField] private Material happyFace;
    [SerializeField] private Material eatingFace;
    [SerializeField] private Material angryFace;
    [SerializeField] private TextMeshProUGUI dishNameUI;
    [SerializeField] private Image dishImage;
    [SerializeField] private GameObject dishGameObject;

    public bool rightFood;

    private NavMeshAgent navMeshAgent;
    private Seat currentSeat;

    [SerializeField] private EndOfDayStatsSO endOfDayStatsSo;

    [Header("FMOD Events")]
    [FMODUnity.EventRef] public string angrySound;

    [Header("FMOD Events")]
    [FMODUnity.EventRef] public string happySound;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentSeat = null; // The customer starts without a seat
        Invoke("LeaveRestaurant", timeToLeave);
        MoveToEmptySpot();
        faceRenderer.material = happyFace;
        dishTrigger.enabled = false;
        rightFood = false;
        dishGameObject.SetActive(false);
    }

    private void MoveToEmptySpot()
    {
        Seat[] emptySeats = FindEmptySeats();

        if (emptySeats.Length > 0)
        {
            walkAnimator.SetBool("walk", true);

            // Get the closest empty seat
            Seat closestEmptySeat = FindClosestSeat(emptySeats);

            // Occupy the seat
            OccupySeat(closestEmptySeat);

            // Move the customer to the seat
            navMeshAgent.SetDestination(closestEmptySeat.transform.position);
        }
        else
        {
            // Handle the case where there are no available seats (optional)
            Debug.LogWarning("No available seats!");
            LeaveRestaurant();
        }
    }

    private void Update()
    {
        //wszystko do wyrzucenia z update
        StartCoroutine(Pathing());
        if (rightFood)
        {
            dishGameObject.SetActive(false);
            dishTrigger.enabled = false;
        }
        else if (dishTrigger.enabled && dishTrigger.wantedDishImage != null)
        {
            dishGameObject.SetActive(true);
            dishImage.overrideSprite = dishTrigger.wantedDishImage;
        }
    }

    private IEnumerator Pathing()
    {
        yield return new WaitForSeconds(1f);
        if (navMeshAgent.enabled && currentSeat != null && navMeshAgent.remainingDistance < 1f && !navMeshAgent.pathPending)
        {
            // Set walk animation to false
            walkAnimator.SetBool("walk", false);
            walkAnimator.SetBool("sit", true);
            navMeshAgent.enabled = false;
            SitDown();
        }
    }

    private Seat[] FindEmptySeats()
    {
        // Find all seats in the scene
        Seat[] allSeats = FindObjectsOfType<Seat>();

        // Filter out the occupied seats
        Seat[] emptySeats = Array.FindAll(allSeats, seat => !seat.IsOccupied);

        return emptySeats;
    }

    private Seat FindClosestSeat(Seat[] seats)
    {
        // Find the closest seat based on the customer's position
        Seat closestSeat = null;
        float closestDistance = Mathf.Infinity;

        foreach (var seat in seats)
        {
            float distance = Vector3.Distance(transform.position, seat.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestSeat = seat;
            }
        }

        return closestSeat;
    }

    public IEnumerator GotFood()
    {
        faceRenderer.material = eatingFace;
        rightFood = true;
        dishGameObject.SetActive(false);
        RuntimeManager.PlayOneShot(happySound, transform.position);
        yield return new WaitForSeconds(5f);
        LeaveRestaurant();
    }

    private void OccupySeat(Seat seat)
    {
        // Occupy the seat and update the currentSeat reference
        seat.OccupySeat();
        currentSeat = seat;
    }

    private void LeaveRestaurant()
    {
        if (rightFood == false)
        {
            faceRenderer.material = angryFace;
            endOfDayStatsSo.unhappyCustomers += 1;

            // Odtwórz dźwięk zły tylko wtedy, gdy klient nie jest zadowolony
            RuntimeManager.PlayOneShot(angrySound, transform.position);
        }
        else
        {
            endOfDayStatsSo.happyCustomers += 1;
        }

        // Vacate the seat before leaving
        if (currentSeat != null)
        {
            currentSeat.VacateSeat();
            navMeshAgent.enabled = true;
            walkAnimator.SetBool("sit", false);
        }

        // Move to the assigned exit point
        if (exitPoint != null)
        {
            navMeshAgent.SetDestination(exitPoint.position);
            walkAnimator.SetBool("walk", true);
        }
        else
        {
            Debug.LogWarning("Exit point not set for the customer!");
        }

        Destroy(gameObject, timeToDespawn);
    }

    private void SitDown()
    {
        transform.position = currentSeat.transform.GetChild(0).position;
        transform.rotation = currentSeat.transform.GetChild(0).rotation;
        dishTrigger.enabled = true;
    }

    public void SetExitPoint(Transform point)
    {
        exitPoint = point;
    }
}
