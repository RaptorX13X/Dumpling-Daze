using UnityEngine;
using UnityEngine.UI;

public class VSyncSettings : MonoBehaviour
{
    public Toggle vSyncToggle;

    void Start()
    {
        // Ustaw odpowiedni toggle zgodnie z aktualnym ustawieniem VSync
        UpdateToggle();
    }

    public void ToggleVSync(bool value)
    {
        if (value)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }

    private void UpdateToggle()
    {
        vSyncToggle.isOn = QualitySettings.vSyncCount != 0;
    }
}