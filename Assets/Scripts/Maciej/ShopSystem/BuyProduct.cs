using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class BuyProduct : MonoBehaviour
{
    [SerializeField] private GameObject product;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private ComputerTrigger computerScript;

    [SerializeField] private Ingredient ingredient;

    [SerializeField] private MoneySO moneySO;
    [SerializeField] private UnityEvent productBought;

    //private void OnMouseDown()
    //{
    //    if (computerScript.isComputering == true && (moneySO.currentMoney >= ingredient.price))
    //    {
    //        Instantiate(product, spawnPoint.position, spawnPoint.rotation);

    //        moneySO.currentMoney -= ingredient.price;

    //        productBought.Invoke();

    //        ProductAnim();
    //    }
    //}

    public void BuyProductClick()
    {
        if (computerScript.isComputering == true && (moneySO.currentMoney >= ingredient.price))
        {
            Instantiate(product, spawnPoint.position, spawnPoint.rotation);

            moneySO.currentMoney -= ingredient.price;

            productBought.Invoke();

            //ProductAnim();
        }
    }

    private void ProductAnim()
    {
        this.transform.DOShakePosition(0.1f, 0.1f, 100, 20f);
        this.transform.DOShakeScale(1f, 0.1f, 100, 20f);
    }
}
