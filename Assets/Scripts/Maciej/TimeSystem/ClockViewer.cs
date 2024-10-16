using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TimeSO time;

    private void Update()
    {
        text.text = time.Time;
    }
}
