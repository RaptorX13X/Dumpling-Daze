using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using FMODUnity;

public class TextInputController : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private RawImage messageImage;
    [SerializeField] private GameObject cmdWindow;
    [SerializeField] private MoneySO money;

    [Header("FMOD Events")]
    [FMODUnity.EventRef] public string typingSound;

    public bool IsTextSelected = false;



    void Start()
    {
        // Access the TMP Input Field component
        inputField = GetComponent<TMP_InputField>();

        // Subscribe to the inputField's onEndEdit event
        inputField.onEndEdit.AddListener(OnInputEndEdit);
    }

    // Method called when user finishes editing the input field
    void OnInputEndEdit(string userInput)
    {
        // Convert user input to lowercase for case-insensitive comparison
        userInput = userInput.ToLower();

        // Check user input against trigger words and perform corresponding actions
        switch (userInput)
        {
            case "kosmo":
                // Action for "kosmo" trigger word
                messageImage.DOColor(Color.black, 1f);
                break;
            case "motherlode":
                money.currentMoney += 50000;

                Debug.Log("You triggered the sims action!");
                break;
            case "hidden":
                // Action for "hidden" trigger word
                Debug.Log("You triggered the hidden action!");
                break;
            case "otherword":
                // Action for "otherword" trigger word
                Debug.Log("You triggered the otherword action!");
                break;
            // Add more cases for additional trigger words and their corresponding actions
            default:
                // Default action if no trigger word matches
                ResetUI();
                break;
        }
    }

    // Method to reset UI
    void ResetUI()
    {
        // Reset the color of messageImage
        messageImage.DOColor(Color.white, 1f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            inputField.ActivateInputField();
        }

        if (Input.anyKeyDown && IsTextSelected);
        {
            RuntimeManager.PlayOneShot(typingSound, transform.position);
        }
    }

    public void OnSelected()
    {
        IsTextSelected = true;
    }

    public void OnDeselected()
    {
        IsTextSelected = false;
    }
}