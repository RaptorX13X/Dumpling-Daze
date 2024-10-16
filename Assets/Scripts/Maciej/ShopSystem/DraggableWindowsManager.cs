using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DraggableWindowsManager
{
    public static List<DraggableWindows> DraggedWindows { get; private set; } = new List<DraggableWindows>();

    public static void SetWindowOnTop(DraggableWindows window)
    {
        if (window == null)
            return;
        int indexOf = DraggedWindows.IndexOf(window);

        DraggableWindows temp = DraggedWindows[indexOf];
        DraggedWindows.RemoveAt(indexOf);
        DraggedWindows.Insert(0, window);
        ReorderWindows();
    }

    public static void ReorderWindows()
    {
        for (int i = 0; i < DraggedWindows.Count; i++)
        {
            DraggedWindows[i].canvas.sortingOrder = 100 - (10 * i);
        }
    }
}