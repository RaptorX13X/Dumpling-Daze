using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableWindows : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    public Canvas canvas;

    private RectTransform rectTransform;
    private Vector2 beginStartPositionDrag;

    private void OnEnable()
    {
        DraggableWindowsManager.DraggedWindows.Add(this);
    }

    private void OnDisable()
    {
        DraggableWindowsManager.DraggedWindows.Remove(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void IDragHandler.OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
    {
        DraggableWindowsManager.SetWindowOnTop(this);
        rectTransform.anchoredPosition = beginStartPositionDrag + MouseControl.dragDelta;

    }

    public void CloseWindow()
    {
        canvas.enabled = false;
    }

    public void OpenWindow()
    {
        canvas.enabled = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        beginStartPositionDrag = rectTransform.anchoredPosition;
    }
}