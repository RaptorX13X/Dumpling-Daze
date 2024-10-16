using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Dodaj to, aby korzysta� z funkcji SceneManager

public class MenuAudio : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string ambientEvent;

    private FMOD.Studio.EventInstance ambientInstance;

    // U�ywamy statycznej zmiennej, aby przechowa� instancj� d�wi�ku z menu
    public static FMOD.Studio.EventInstance MenuAudioInstance;

    void Start()
    {
        ambientInstance = FMODUnity.RuntimeManager.CreateInstance(ambientEvent);
        ambientInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
        ambientInstance.start();

        // Przypisz instancj� do statycznej zmiennej
        MenuAudioInstance = ambientInstance;

        // Zachowaj obiekt mi�dzy scenami
        DontDestroyOnLoad(gameObject);
    }

    // Dodaj dodatkowe funkcje do kontroli d�wi�ku zgodnie z potrzebami gry.

    void Update()
    {
        // Sprawd�, czy za�adowano scen� z levelem
        if (SceneManager.GetActiveScene().name == "MainScene5")
        {
            // Wy��cz muzyk� z menu
            StopMenuMusic();
        }
    }

    void StopMenuMusic()
    {
        // Sprawd�, czy istnieje instancja d�wi�ku z menu
        if (MenuAudioInstance.isValid())
        {
            // Zatrzymaj d�wi�k
            MenuAudioInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

            // Zwolnij zasoby
            MenuAudioInstance.release();
        }

        // Zniszcz obiekt, kt�ry zawiera skrypt MenuAudio
        Destroy(gameObject);
    }
}


