using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectRotate : MonoBehaviour
{
    void Start()
    {
        RotateIndefinitely();
    }

    void RotateIndefinitely()
    {
        // Rotate 360 degrees in 1 second and set the loop type to Infinite
        transform.DORotate(new Vector3(0f, 360f, 0f), 1f, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear);
    }
}
