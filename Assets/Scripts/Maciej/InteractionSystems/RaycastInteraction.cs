using UnityEngine;

public class RaycastInteraction : MonoBehaviour
{
    private Camera mainCamera;
    private float maxRaycastDistance = 2f; // Maximum distance for the raycast

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Check for mouse click
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Cast a ray from the mouse position
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Draw a debug ray to visualize the ray in the scene view
            Debug.DrawRay(ray.origin, ray.direction * maxRaycastDistance, Color.yellow, 0.5f);

            // Check if the ray hits something within the maximum distance
            if (Physics.Raycast(ray, out hit, maxRaycastDistance))
            {
                // Check if the hit object implements the IInteractable interface
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    // Call the Interact method on the interactable object
                    interactable.Interact();
                }
            }
        }
    }
}

// Interface for interactable objects
public interface IInteractable
{
    void Interact();
}
