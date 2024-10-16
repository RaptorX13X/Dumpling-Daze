using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainDoorOpen : MonoBehaviour
{
    [SerializeField] private GameObject mainDoorleft;
    [SerializeField] private GameObject mainDoorright;

    public void AnimateDoors()
    {
        mainDoorleft.transform.DORotate(new Vector3(0f, 30f, 0f), 1f);
        mainDoorright.transform.DORotate(new Vector3(0f, -210f, 0f), 1f);
    }
}
