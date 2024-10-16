using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class ButtonSoundController : MonoBehaviour
{
    // Referencja do komponentu FMOD Event Emitter przypisanego do przycisku
    private FMODUnity.StudioEventEmitter fmodEventEmitter;

    void Start()
    {
        // Pobierz komponent FMOD Event Emitter przypisany do tego samego obiektu
        fmodEventEmitter = GetComponent<FMODUnity.StudioEventEmitter>();

        // Dodaj obs�ug� zdarzenia klikni�cia na przycisku
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(PlayButtonSound);
        }
    }

    // Obs�uguje klikni�cie myszk�
    void PlayButtonSound()
    {
        // Upewnij si�, �e mamy przypisany komponent FMOD Event Emitter
        if (fmodEventEmitter != null)
        {
            // Odtw�rz zdarzenie d�wi�kowe
            fmodEventEmitter.Play();
        }
    }
}
