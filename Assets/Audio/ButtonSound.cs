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

        // Dodaj obs³ugê zdarzenia klikniêcia na przycisku
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(PlayButtonSound);
        }
    }

    // Obs³uguje klikniêcie myszk¹
    void PlayButtonSound()
    {
        // Upewnij siê, ¿e mamy przypisany komponent FMOD Event Emitter
        if (fmodEventEmitter != null)
        {
            // Odtwórz zdarzenie dŸwiêkowe
            fmodEventEmitter.Play();
        }
    }
}
