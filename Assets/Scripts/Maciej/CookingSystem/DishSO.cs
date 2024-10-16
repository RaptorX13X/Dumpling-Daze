using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Dish", menuName = "CookingSystem/Dish")]
public class Dish : ScriptableObject
{
    public string dishName;

    public Ingredient[] requiredIngredients;

    public float price; // Value to add after delivering this dish
    public GameObject prefab;
    public Sprite dishSprite;
}