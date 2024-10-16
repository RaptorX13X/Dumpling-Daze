using UnityEngine;
using FMODUnity;
using DG.Tweening;
using UnityEngine.Events;

public class ControlInstructions : MonoBehaviour, IInteractable
{
    private bool inRangeInstructions = false;
    [SerializeField] private UnityEvent buttonPressed;
    [SerializeField] private PlayerMovement playerMove;
    [SerializeField] private MouseLook mouseLook;

    [SerializeField] private CanvasGroup[] instructionCanvases;
    [SerializeField] private CanvasGroup mainInstructionCanvas;
    public int currentInstructionIndex = 0;

    [Header("FMOD Events")]
    [FMODUnity.EventRef] public string startDayButton;

    public bool tutorialEnded;
    [SerializeField] private PlayerStats playerStatsSO;


    private void Start()
    {
        if (playerStatsSO.firstDayDone)
        {
            HideAllTutorial();
            tutorialEnded = true;
            return;
        }
        
        // Hide all instruction canvases except the first one
        for (int i = 1; i < instructionCanvases.Length; i++)
        {
            instructionCanvases[i].alpha = 0f;
            instructionCanvases[i].blocksRaycasts = false;
        }

        ShowInstruction(currentInstructionIndex);
        playerMove.enabled = false;
        mouseLook.enabled = false;
        tutorialEnded = false;
    }

    public void Interact()
    {
        this.gameObject.transform.DOMoveZ(4.3f, 1);
        RuntimeManager.PlayOneShot(startDayButton, transform.position);
        buttonPressed.Invoke();
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.T))
    //    {
    //        if (currentInstructionIndex < instructionCanvases.Length)
    //        {
    //            // Show the current instruction
    //            ShowInstruction(currentInstructionIndex);
    //            currentInstructionIndex++;
    //        }
    //        else
    //        {
    //            // All instructions are shown, reset to the first instruction
    //            currentInstructionIndex = 0;
    //        }
    //    }
    //}

    public void NextTutorial()
    {
        if (currentInstructionIndex < instructionCanvases.Length)
        {
            // Show the current instruction
            ShowInstruction(currentInstructionIndex);
            currentInstructionIndex++;
        }
        else
        {
            // All instructions are shown, reset to the first instruction
            currentInstructionIndex = 0;
            playerMove.enabled = true;
            mouseLook.enabled = true;

            mainInstructionCanvas.alpha= 0f;
            mainInstructionCanvas.blocksRaycasts= false;
        }
    }

    public void HideAllTutorial()
    {
        currentInstructionIndex = 0;
        playerMove.enabled = true;
        mouseLook.enabled = true;

        mainInstructionCanvas.alpha = 0f;
        mainInstructionCanvas.blocksRaycasts = false;   
        tutorialEnded = true;
    }

    private void ShowInstruction(int index)
    {
        if (index > 0)
        {
            // Fade out the previous instruction
            instructionCanvases[index - 1].DOFade(0f, 1f);
            instructionCanvases[index - 1].blocksRaycasts = false;

        }

        // Fade in the current instruction
        instructionCanvases[index].DOFade(1f, 1f);
        instructionCanvases[index].blocksRaycasts = true;
    }
}


