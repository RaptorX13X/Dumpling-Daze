using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour
{
    private bool isOccupied = false;

    public bool IsOccupied
    {
        get { return isOccupied; }
    }

    public void OccupySeat()
    {
        isOccupied = true;
    }

    public void VacateSeat()
    {
        isOccupied = false;
    }
}
