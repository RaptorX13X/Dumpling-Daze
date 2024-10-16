
using UnityEngine;

public class CanvaSwitch : MonoBehaviour
{
    public GameObject canva; // Referencja do obiektu "Settings_menu"

    // Metoda wywo�ywana po klikni�ciu przycisku "Settings"
    public void Togglecanva()
    {
        canva.SetActive(!canva.activeSelf); // W��cz/wy��cz Canvas "Settings_menu"
    }
}
