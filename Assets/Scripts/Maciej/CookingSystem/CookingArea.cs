using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using DG.Tweening;
using FMODUnity;

public class CookingArea : MonoBehaviour
{
    [SerializeField] List<Ingredient> ingredientsOnTable = new List<Ingredient>();
    [SerializeField] List<GameObject> gameObjectsToDestroy = new List<GameObject>(); // List to collect GameObjects on the table

    [SerializeField] List<Dish> allDishes = new List<Dish>(); // List of all available dishes

    [SerializeField] Dish completedDish; // Reference to the completed dish SO
    [SerializeField] Transform dishSpawnPoint;
    [SerializeField] TextMeshProUGUI tableUI;

    [SerializeField] private bool allowToCook = false;

    // FMOD Event Reference
    [EventRef] public string correctCombinationSoundEvent;
    [EventRef] public string invalidCombinationSoundEvent;

    [Header("Minigames")]
    [SerializeField] private MinigameStarter minigameStarter;

    private void UpdateTableUI()
    {
        if (tableUI != null)
        {
            tableUI.text = ingredientsOnTable.Count + "/3";

            tableUI.transform.DOShakePosition(0.1f, 0.1f, 10);

            //DOTween
            if (ingredientsOnTable.Count > 2)
            {
                tableUI.DOColor(Color.red, 1f);
                RuntimeManager.PlayOneShot(invalidCombinationSoundEvent, transform.position);
            }
            else
            {
                tableUI.DOColor(Color.green, 1f);
                RuntimeManager.PlayOneShot(correctCombinationSoundEvent, transform.position);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ingredient")) //&& other.gameObject.transform.parent == null)
        {
            // Check if the collider has the script attached
            WhatIgredient script = other.GetComponent<WhatIgredient>();
            Ingredient ingredient = script.igredient;
            if (ingredient != null)
            {
                ingredientsOnTable.Add(ingredient);
                gameObjectsToDestroy.Add(other.gameObject);
                UpdateTableUI(); // Update the UI text when an item is added

                if (ingredientsOnTable.Count == 3) // Check if there are 2 ingredients on the table
                {
                    allowToCook = true;
                }
                else if (ingredientsOnTable.Count > 3)
                {
                    allowToCook = false;
                    Debug.Log("Too many ingredients");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) //Set up a ingredient layer later.
    {
        if (other.gameObject.CompareTag("Ingredient"))// && other.gameObject.transform.parent == null)
        {
            // Check if the collider has the script attached
            WhatIgredient script = other.GetComponent<WhatIgredient>();
            Ingredient ingredient = script.igredient;
            if (ingredient != null)
            {
                ingredientsOnTable.Remove(ingredient);
                gameObjectsToDestroy.Remove(other.gameObject);
                UpdateTableUI(); // Update the UI text when an item is added

                if (ingredientsOnTable.Count == 3) // Check if there are 2 ingredients on the table
                {
                    allowToCook = true;
                }
                else if (ingredientsOnTable.Count != 3)
                {
                    allowToCook = false;
                    Debug.Log("Too many ingredients");
                }
            }
        }
    }

    private void CheckAndCompleteDish()
    {
        // Check if the required ingredients for any dish are on the table
        foreach (Dish dish in allDishes)
        {
            if (CheckDishIngredients(dish))
            {
                completedDish = dish;

                Instantiate(completedDish.prefab, dishSpawnPoint.position, dishSpawnPoint.rotation);
                Debug.Log("Dish completed: " + completedDish.dishName);

                ingredientsOnTable.Clear();

                // Destroy the collected GameObjects
                foreach (GameObject gameObject in gameObjectsToDestroy)
                {
                    Destroy(gameObject);
                }
                gameObjectsToDestroy.Clear(); // Clear the list after destroying all GameObjects
                UpdateTableUI(); // Update the UI text when an item is added
                return;
            }
            else
            {
                Debug.Log("Invalid Ingredient Combination");
            }
        }
    }

    private bool CheckDishIngredients(Dish dish)
    {
        foreach (Ingredient requiredIngredient in dish.requiredIngredients)
        {
            if (!ingredientsOnTable.Contains(requiredIngredient))
            {
                return false;
            }
        }
        return true;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && allowToCook && !minigameStarter.MinigameInProgress())  
        {
            minigameStarter.StartNewMinigame();
            StartCoroutine(MinigameInProgress());
        }
    }

    private IEnumerator MinigameInProgress()
    {
        yield return new WaitWhile(minigameStarter.MinigameInProgress);
        CheckAndCompleteDish();
    }
}