using UnityEngine;
using FMODUnity;

public class FootstepManager : MonoBehaviour
{
    [SerializeField] private float distanceT;
    public EventReference woodenSurfaceFootstepEvent;
    public EventReference tileSurfaceFootstepEvent;

    private FMOD.Studio.EventInstance footstepInstance;
    private Vector3 lastPosition;
    public LayerMask LayerMask;

    private void Start()
    {
        lastPosition = transform.position;

        // Utwórz instancjê dŸwiêku na starcie
        footstepInstance = FMODUnity.RuntimeManager.CreateInstance(woodenSurfaceFootstepEvent);
        footstepInstance = FMODUnity.RuntimeManager.CreateInstance(tileSurfaceFootstepEvent);
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

    public void UpdateFootstepEvent()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.0f))
        {
            int layer = hit.collider.gameObject.layer;

            if (layer == LayerMask.NameToLayer("WoodenSurface"))
            {
                Debug.Log("Wooden surface detected!");
                footstepInstance = FMODUnity.RuntimeManager.CreateInstance(woodenSurfaceFootstepEvent);
                footstepInstance.start();
            }
            else if (layer == LayerMask.NameToLayer("TileSurface"))
            {
                Debug.Log("Tile surface detected!");
                footstepInstance = FMODUnity.RuntimeManager.CreateInstance(tileSurfaceFootstepEvent);
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
