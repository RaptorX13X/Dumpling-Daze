using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Events;
using FMODUnity;

public class MoneyShower : MonoBehaviour
{
    [SerializeField] private MoneySO moneySO; // Reference to the Scriptable Object
    [SerializeField] private TextMeshProUGUI uiText;

    [Header("FMOD Events")]
    [FMODUnity.EventRef] public string moneySound;


    private void Start()
    {
        // Check if the Scriptable Object and UI Text element are assigned
        if (moneySO != null && uiText != null)
        {
            // Update the UI Text with the value from the Scriptable Object
            uiText.text = "Account: " + moneySO.currentMoney.ToString() + "$";
        }
    }

    private void ChangeColorBankruptcy()
    {
        if (moneySO.currentMoney == 0)
        {
            uiText.color = Color.red;
        }
        else if (moneySO.currentMoney >= 0)
        {
            uiText.color = Color.green;
        }
    }

    private void ChangeColorBought()
    {
        // Create a sequence of tweens
        Sequence sequence = DOTween.Sequence();

        // Change the color of the UI Text to red over 0.1 seconds
        sequence.Append(uiText.DOColor(Color.red, 0.1f));

        // Wait for 1 second
        sequence.AppendInterval(0.1f);

        // Change the color of the UI Text back to green over 0.1 seconds
        sequence.Append(uiText.DOColor(Color.green, 0.1f));
    }

    private void ChangeAnim()
    {
        this.transform.DOShakePosition(0.1f, 0.1f, 100, 20f);
    }

    public void MoneyChange() //Zapytaæ o action listener bo nie chce ju¿ mi siê pod³¹czaæ tych unity eventów.
    {
        // Check if the Scriptable Object and UI Text element are assigned
        if (moneySO != null && uiText != null)
        {
            // Update the UI Text with the value from the Scriptable Object
            uiText.text = "Account: " + moneySO.currentMoney.ToString() + "$";

            RuntimeManager.PlayOneShot(moneySound, transform.position);
        }

        ChangeColorBought();

        ChangeColorBankruptcy();

        ChangeAnim();
    }
}
