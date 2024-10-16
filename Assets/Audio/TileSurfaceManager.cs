using UnityEngine;
using FMODUnity;

public class TileFootstepManager : MonoBehaviour
{
    [SerializeField] private float distanceT;
    public EventReference tileSurfaceFootstepEvent;

    private FMOD.Studio.EventInstance footstepInstance;
    private Vector3 lastPosition;
    public LayerMask LayerMask;
    string surfaceType = PlayerMovement.surfaceType;

    private void Start()
    {
        lastPosition = transform.position;
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

            if (layer == LayerMask.NameToLayer("TileSurface"))
            {
                Debug.Log("Tile surface detected!");
                footstepInstance.start();
            }
        }
    }

    private void PlayFootstepSound()
    {
        // Odtw�rz zdarzenie d�wi�kowe
        footstepInstance.start();
    }
}

