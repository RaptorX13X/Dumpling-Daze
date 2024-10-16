using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CuttingBoard : MonoBehaviour, IInteractable
{
    [SerializeField] List<Ingredient> ingredientsOnTable = new List<Ingredient>();
    [SerializeField] List<GameObject> gameObjectsToDestroy = new List<GameObject>(); // List to collect GameObjects on the table

    [SerializeField] List<Ingredient> allAdvIngr = new List<Ingredient>();

    [SerializeField] Ingredient completedAdvIng; // Reference to the completed dish SO
    [SerializeField] Transform advIngSpawnPoint;
    [SerializeField] TextMeshProUGUI tableUI;

    //private void Start()
    //{

    //    tableUI.transform.DOShakePosition(1f, 0.1f, 1).SetLoops(-1, LoopType.Incremental);
    //}

    private void UpdateTableUI()
    {
        if (tableUI != null)
        {
            tableUI.text = ingredientsOnTable.Count + "/1";

            tableUI.transform.DOShakePosition(0.1f, 0.1f, 10);

            //DOTween
            if (ingredientsOnTable.Count > 1)
            {
                tableUI.DOColor(Color.red, 1f);
            }
            else
            {
                tableUI.DOColor(Color.green, 1f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ingredient") && other.gameObject.transform.parent == null)
        {
            // Check if the collider has the script attached
            WhatIgredient script = other.GetComponent<WhatIgredient>();
            Ingredient ingredient = script.igredient;
            if (ingredient != null)
            {
                ingredientsOnTable.Add(ingredient);
                gameObjectsToDestroy.Add(other.gameObject);
                UpdateTableUI(); // Update the UI text when an item is added

                if (ingredientsOnTable.Count == 1) // Check if there are 2 ingredients on the table
                {
                    CheckAndCompleteDish();
                }
                else if (ingredientsOnTable.Count > 2)
                {
                    Debug.Log("Too many ingredients");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) //Set up a ingredient layer later.
    {
        if (other.gameObject.CompareTag("Ingredient") && other.gameObject.transform.parent == null)
        {
            // Check if the collider has the script attached
            WhatIgredient script = other.GetComponent<WhatIgredient>();
            Ingredient ingredient = script.igredient;
            if (ingredient != null)
            {
                ingredientsOnTable.Remove(ingredient);
                gameObjectsToDestroy.Remove(other.gameObject);
                UpdateTableUI(); // Update the UI text when an item is added
            }
        }
    }

    private void CheckAndCompleteDish()
    {
        // Check if the required ingredients for any dish are on the table
        foreach (Ingredient advIngridient in allAdvIngr)
        {
            if (CheckDishIngredients(advIngridient))
            {

                Instantiate(advIngridient.prefab, advIngSpawnPoint.position, advIngSpawnPoint.rotation);
                Debug.Log("Dish completed: " + advIngridient.ingredientName);

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

    private bool CheckDishIngredients(Ingredient advIngridient)
    {
        foreach (Ingredient requiredIngredient in advIngridient.requiredAdvIngr)
        {
            if (!ingredientsOnTable.Contains(requiredIngredient))
            {
                return false;
            }
        }
        return true;
    }

        public void Interact()
    {
        Debug.Log("Object interacted!");
        // Implement interaction behavior here
    }
}