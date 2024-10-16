using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PCSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] private UnityEvent PCturnedOn;
    [SerializeField] private UnityEvent PCturnedOff;

    private bool isPCturned = false; // Variable to keep track of the switch state
    private bool canToggle = true; // Variable to control toggling
                                   // 
    [Header("FMOD Events")]
    [FMODUnity.EventRef] public string exitComputerSound;

    // Function to toggle the device on/off
    public void Interact()
    {
        if (!canToggle)
            return; // If cooldown is active, do nothing

        isPCturned = !isPCturned; // Flip the switch state

        if (isPCturned)
        {
            PCturnedOn.Invoke();
            // Activate the device

        }
        else
        {
            PCturnedOff.Invoke();
            // Deactivate the device
            // Play exit computer sound
            RuntimeManager.PlayOneShot(exitComputerSound, transform.position);
        }

        StartCoroutine(ToggleCooldown());
    }

    IEnumerator ToggleCooldown()
    {
        canToggle = false; // Disable toggling
        yield return new WaitForSeconds(1f); // Adjust cooldown duration as needed
        canToggle = true; // Enable toggling again
    }
}
