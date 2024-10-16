using UnityEngine;
using FMODUnity;

public class WoodenFootstepManager : MonoBehaviour
{
    [SerializeField] private float distanceW;
    public EventReference woodenSurfaceFootstepEvent;

    private FMOD.Studio.EventInstance footstepInstance;
    private Vector3 lastPosition;
    public LayerMask LayerMask;
    string surfaceType = PlayerMovement.surfaceType;

    private void Start()
    {
        lastPosition = transform.position;
        footstepInstance = FMODUnity.RuntimeManager.CreateInstance(woodenSurfaceFootstepEvent);
    }

    private void Update()
    {
        float distanceMoved = Vector3.Distance(transform.position, lastPosition);
        float footstepDistanceThreshold = distanceW;

        if (distanceMoved > footstepDistanceThreshold)
        {
            UpdateFootstepEvent();
            PlayFootstepSound();
            lastPosition = transform.position;
        }
    }

    public void UpdateFootstepEvent()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.0f))
        {
            int layer = hit.collider.gameObject.layer;

            if (layer == LayerMask.NameToLayer("WoodenSurface"))
            {
                Debug.Log("Wooden surface detected!");
                footstepInstance.start();
            }
        }
    }

    private void PlayFootstepSound()
    {
        // Odtwórz zdarzenie dŸwiêkowe
        footstepInstance.start();
    }
}
