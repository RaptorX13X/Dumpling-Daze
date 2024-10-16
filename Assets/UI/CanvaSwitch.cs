
using UnityEngine;

public class CanvaSwitch : MonoBehaviour
{
    public GameObject canva; // Referencja do obiektu "Settings_menu"

    // Metoda wywo³ywana po klikniêciu przycisku "Settings"
    public void Togglecanva()
    {
        canva.SetActive(!canva.activeSelf); // W³¹cz/wy³¹cz Canvas "Settings_menu"
    }
}
