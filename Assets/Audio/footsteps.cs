using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class footsteps : MonoBehaviour
{
    [SerializeField] private float distanceT;
    public EventReference FootstepEvent;

    private FMOD.Studio.EventInstance footstepInstance;
    private Vector3 lastPosition;

    private void Start()
    {
        lastPosition = transform.position;
        footstepInstance = FMODUnity.RuntimeManager.CreateInstance(FootstepEvent); // Default to wood footstep event
    }

    private void Update()
    {
        float distanceMoved = Vector3.Distance(transform.position, lastPosition);
        float footstepDistanceThreshold = distanceT;

        if (distanceMoved > footstepDistanceThreshold)
        {
            UpdateFootstepEvent();
            PlayFootstepSound();
            lastPosition = transform.position;
        }
    }

    private void UpdateFootstepEvent()
    {
        // Raycast down to detect the surface beneath the player
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.0f))
        {
            int layer = hit.collider.gameObject.layer;

            // Check the layer and update the footstep event accordingly
            if (layer == LayerMask.NameToLayer("Wood"))
            {
                footstepInstance.setParameterByNameWithLabel("Surface", "Wood");
            }
            else if (layer == LayerMask.NameToLayer("Normal"))
            {
                footstepInstance.setParameterByNameWithLabel("Surface", "Normal");
            }
            // Add more cases for other layers if needed
        }
    }

    /*private IEnumerator FootstepStart()
    {
        yield return new WaitForSeconds(1.5f);
        footstepInstance.start();
    }
    */
    private void PlayFootstepSound()
    {
        // Play the FMOD event
        //start after every delay
        footstepInstance.start();
        //StartCoroutine(FootstepStart());
    }
}