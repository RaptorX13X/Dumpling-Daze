using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Cinemachine;
using UnityEngine.Video;
using FMODUnity;
using System.Collections;


public class ComputerTrigger : MonoBehaviour, IInteractable
{
    [Header("Bools")]
    public bool isComputering = false;
    [SerializeField] private bool isPCTurned = false;
    [SerializeField] private bool isTyping = false; // Dodaj zmienn� do �ledzenia, czy aktualnie piszesz

    [Header("Enablers")]
    [SerializeField] private MouseControl mouseController;
    [SerializeField] private MouseLook mouseLook;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private RawImage shutdownScreen;
    [SerializeField] private CinemachineVirtualCamera cmCam;

    [Header("Button")]
    [SerializeField] private GameObject button;
    [SerializeField] private Material OnMat;
    [SerializeField] private Material OffMat;
    [SerializeField] private VideoPlayer LoadVid;
    private bool canToggle = true;
    [FMODUnity.EventRef] public string enterComputerSound;

    [Header("FMOD Events")]
    [FMODUnity.EventRef] public string typingSound;

    public bool IsTextSelected = false;
    public TMP_InputField cmdInputField; // Referencja do pola tekstowego

    private void Update()
    {
        LeavePC();

        // Sprawd�, czy piszesz w cmdInputField, a nast�pnie odtw�rz d�wi�k pisania
        if (isTyping && Input.anyKeyDown)
        {
            RuntimeManager.PlayOneShot(typingSound, transform.position);
        }
    }

    public void Interact()
    {
        Debug.Log("AAA");

        if (!canToggle)
            return; // If cooldown is active, do nothing

        if (isPCTurned && !isComputering)
        {
            cmCam.Priority = 30;
            isComputering = true;
            mouseLook.enabled = false;
            playerMovement.enabled = false;
            mouseController.enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;
            IsTextSelected = true; // Ustaw pole tekstowe jako aktywne przy wej�ciu do komputera
        }

        StartCoroutine(ToggleCooldown());
    }

    private void LeavePC()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isComputering)
            {
                isComputering = false;
                cmCam.Priority = 0;
                Invoke("DelayedMovement", 2f);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Locked;
                IsTextSelected = false; // Ustaw pole tekstowe jako nieaktywne po opuszczeniu komputera
            }
        }
    }

    void DelayedMovement()
    {
        mouseController.enabled = false;
        mouseLook.enabled = true;
        playerMovement.enabled = true;
    }

    IEnumerator ToggleCooldown()
    {
        canToggle = false; // Disable toggling
        yield return new WaitForSeconds(1f); // Adjust cooldown duration as needed
        canToggle = true; // Enable toggling again
    }

    public void MoveButtonOn()
    {
        Invoke("DelayedStartup", 3f);
        LoadVid.gameObject.SetActive(true);
        LoadVid.Play();
        button.GetComponent<Renderer>().material = OnMat;
    }

    private void DelayedStartup()
    {
        Debug.Log("PC ON");
        shutdownScreen.DOFade(0f, 1f);
        LoadVid.gameObject.SetActive(false);
        isPCTurned = true;

        // Play enter computer sound
        RuntimeManager.PlayOneShot(enterComputerSound, transform.position);
    }

    public void MoveButtonOff()
    {
        Debug.Log("PC OFF");
        shutdownScreen.DOFade(1f, 1f);
        button.GetComponent<Renderer>().material = OffMat;
        isPCTurned = false;
    }

    // Metoda wywo�ywana, gdy pole tekstowe jest aktywne
    public void OnSelected()
    {
        isTyping = true;
    }

    // Metoda wywo�ywana, gdy pole tekstowe jest nieaktywne
    public void OnDeselected()
    {
        isTyping = false;
    }
}
