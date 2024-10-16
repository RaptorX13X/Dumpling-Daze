using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.UI;

public class DishTrigger : MonoBehaviour
{
    [SerializeField] private List<Dish> availableDishes = new List<Dish>(); // List of available dishes as ScriptableObjects
    [SerializeField] private float timeInterval = 5f; // Time interval between giving out dishes
    [SerializeField] public string wantedDishName; // Name of the dish the trigger is looking for
    public Sprite wantedDishImage;

    [SerializeField] private MoneySO Money;

    [SerializeField] private bool canGiveDish = false;
    [SerializeField] private bool isWaitingForOrder = true;

    [SerializeField] public bool canOrderDish = true;

    [SerializeField] ParticleSystem confetti;
    [SerializeField] Transform origin;

    [SerializeField] private UnityEvent dishSold;

    [SerializeField] private Customer customer;

    

    private void Start()
    {
        StartCoroutine(GiveOutDishes());
    }

    private IEnumerator GiveOutDishes()
    {
        yield return new WaitForSeconds(timeInterval);

        isWaitingForOrder = true;

        if (isWaitingForOrder == true && canOrderDish == true)
        {
            // Get a random dish from the available dishes list
            Dish randomDish = GetRandomDish();
            if (randomDish != null)
            {
                wantedDishName = randomDish.dishName; // Set the wanted dish name to the random dish name
                wantedDishImage = randomDish.dishSprite;
                Debug.Log("Random dish given: " + wantedDishName);

                canGiveDish = true;
                isWaitingForOrder = false;

                canOrderDish = false;
                canGiveDish = true;

                // Here you can perform any action with the random dish
            }
        }
    }

    private void AddValue(float price)
    {
        Money.currentMoney += price; // Assuming 'money' is a public field in MoneySO representing the total money
        Debug.Log("Added " + price + " to money.");
        Debug.Log("Your current money is now " + Money.currentMoney);
    }

    private void OnTriggerEnter(Collider other) //CheckForWantedDish()
    {
        if (canGiveDish)
        {
            WhatDish script = other.GetComponent<WhatDish>();
            Dish dish = script.dish;

            if (dish != null && dish.dishName == wantedDishName)
            {
                Debug.Log("Success :)");

                AddValue(dish.price);

                //canOrderDish = true;
                //canGiveDish = false;

                //StartCoroutine(GiveOutDishes());
                customer.rightFood = true;
                customer.StartCoroutine(customer.GotFood());

                dishSold.Invoke();

                confetti.Play();
                StartCoroutine(MoveAndDestroy(other.gameObject)); // Coroutine for moving and destroying the object


            }
            else if (dish != null && dish.dishName != wantedDishName)
            {
                Debug.Log("Failure :(");

                canOrderDish = true;
            }
        }
    }

    private IEnumerator MoveAndDestroy(GameObject objToMove)
    {
        // Move the object to the origin position using DOTween
        objToMove.transform.DOMove(origin.position, 1f);
        objToMove.transform.DOScale(new Vector3(0f,0f, 0f), 1f);

        // Wait for 1 second after the move is completed
        yield return new WaitForSeconds(1f);

        // Destroy the object
        Destroy(objToMove);
    }

    private Dish GetRandomDish()
    {
        if (availableDishes.Count == 0)
            return null;

        int randomIndex = Random.Range(0, availableDishes.Count);
        return availableDishes[randomIndex];
    }
}