using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "CookingSystem/Ingredient")]
public class Ingredient : ScriptableObject
{
    public string ingredientName;
    public GameObject prefab;

    public float price;

    // Optionally if Advanced Ingridient

    public Ingredient[] requiredAdvIngr;
}