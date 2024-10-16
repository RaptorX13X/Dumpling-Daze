using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseControl : MonoBehaviour
{
    [SerializeField] private Canvas smallCanvas; // The canvas where the player is looking
    [SerializeField] private Canvas bigCanvas;   // The bigger canvas where the cursor will move

    private RectTransform smallCanvasRectTransform;
    private RectTransform bigCanvasRectTransform;

    public static Vector3 cursorPos;
    public static Vector3 mousePos;
    public static Vector2 dragDelta => (mousePos - cursorPos);
    [Header("FMOD Events")]
    [FMODUnity.EventRef] public string mouseSound;
    private void Start()
    {
        smallCanvasRectTransform = smallCanvas.GetComponent<RectTransform>();
        bigCanvasRectTransform = bigCanvas.GetComponent<RectTransform>();

        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        // Convert mouse position to local point on big canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            bigCanvasRectTransform, mousePosition, bigCanvas.worldCamera, out Vector2 localPoint
        );

        // Clamp the cursor position within the boundaries of the big canvas
        float clampedX = Mathf.Clamp(localPoint.x, bigCanvasRectTransform.rect.xMin, bigCanvasRectTransform.rect.xMax);
        float clampedY = Mathf.Clamp(localPoint.y, bigCanvasRectTransform.rect.yMin, bigCanvasRectTransform.rect.yMax);
        Vector2 clampedCursorPosition = new Vector2(clampedX, clampedY);
        mousePos = transform.localPosition;
        if (Input.GetMouseButtonDown(0))
        {
            cursorPos = transform.localPosition;
            // Play enter computer sound
            RuntimeManager.PlayOneShot(mouseSound, transform.position);
        }
        //Debug.Log(dragDelta);
        // Set the cursor position on the big canvas
        transform.localPosition = clampedCursorPosition;
        
    }
}
