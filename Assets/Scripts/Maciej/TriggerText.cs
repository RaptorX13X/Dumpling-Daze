using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TriggerText : MonoBehaviour
{
    [SerializeField] private CanvasGroup triggerCanvas;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            triggerCanvas.DOFade(1f, 1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            triggerCanvas.DOFade(0f, 1f);
        }
    }
}
