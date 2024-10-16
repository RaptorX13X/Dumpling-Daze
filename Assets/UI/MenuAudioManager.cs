using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

public class MenuAudioManager : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;     // Suwak dla g�o�no�ci muzyki
    [SerializeField] private Slider soundsSlider;    // Suwak dla g�o�no�ci d�wi�k�w
    [SerializeField] private Slider menuSlider;      // Suwak dla g�o�no�ci menu

    // Referencje do autobus�w d�wi�kowych w FMOD Studio
    [SerializeField] private string musicBusName = "Bus:/Music";    // Nazwa autobusu d�wi�kowego dla muzyki
    [SerializeField] private string soundsBusName = "Bus:/Sounds";  // Nazwa autobusu d�wi�kowego dla d�wi�k�w
    [SerializeField] private string menuBusName = "Bus:/MenuMusic"; // Nazwa autobusu d�wi�kowego dla menu

    [FMODUnity.ParamRef]
    public string MenuMusicParameter;
    [FMODUnity.ParamRef]
    public string SoundsParameter;
    [FMODUnity.ParamRef]
    public string MusicParameter;

    private float initialMusicVolume;
    private float initialSoundsVolume;
    private float initialMenuVolume;

    private void Start()
    {
        // Pobierz pocz�tkowe warto�ci g�o�no�ci z FMOD
        initialMusicVolume = GetInitialParameterValue(MusicParameter, musicBusName);
        initialSoundsVolume = GetInitialParameterValue(SoundsParameter, soundsBusName);
        initialMenuVolume = GetInitialParameterValue(MenuMusicParameter, menuBusName);

        // Ustaw pocz�tkowe warto�ci suwak�w
        musicSlider.value = 12f; // Ustaw na �rodku
        soundsSlider.value = 12f;
        menuSlider.value = 12f; // 
    }

    private float GetInitialParameterValue(string paramName, string busName)
    {
        float value = 0;

        // Pobierz opis parametru
        PARAMETER_DESCRIPTION parameterDescription;
        RuntimeManager.StudioSystem.getParameterDescriptionByName(paramName, out parameterDescription);

        // Pobierz warto�� parametru
        RuntimeManager.StudioSystem.getParameterByID(parameterDescription.id, out value);

        return value;
    }

    // Metoda do aktualizacji g�o�no�ci muzyki
    public void SetMusicVolume()
    {

        // Oblicz odwrotn� warto�� suwaka, aby uzyska� warto�� g�o�no�ci w zakresie od 0 do 1
        float volume = 1.0f - musicSlider.value;

        // Logowanie warto�ci suwaka
        Debug.Log("Music Slider Value: " + volume);

        // Ustaw g�o�no�� na warto�� obliczonego suwaka
        RuntimeManager.StudioSystem.setParameterByName(MusicParameter, volume);

        // Logowanie ustawionej g�o�no�ci
        Debug.Log("Set Music Volume: " + volume);
    }

    public void SetMusicVolumeFromSlider(float volume)
    {
        RuntimeManager.GetBus(musicBusName).setVolume(volume);
    }


    // Metoda do aktualizacji g�o�no�ci d�wi�k�w
    public void SetSoundsVolume()
    {
        float volume = soundsSlider.value;

        // Sprawd� czy suwak jest na �rodku
        if (soundsSlider.value == 1f)
        {
            // Je�li suwak jest na �rodku, ustaw g�o�no�� na pocz�tkow� warto��
            volume = initialSoundsVolume;
        }
        else
        {
            // W przeciwnym razie, ustaw g�o�no�� na warto�� suwaka
            volume = soundsSlider.value;
        }

        RuntimeManager.GetBus(soundsBusName).setVolume(volume);

        //RuntimeManager.StudioSystem.setParameterByName(SoundsParameter, volume);
    }

    public void SetSoundsVolumeFromSlider(float volume)
    {
        RuntimeManager.GetBus(soundsBusName).setVolume(volume);
    }

    // Metoda do aktualizacji g�o�no�ci menu
    public void SetMenuVolume()
    {
        float volume = menuSlider.value;

        // Logowanie warto�ci suwaka
        Debug.Log("Menu Slider Value: " + volume);

        // Sprawd� czy suwak jest na �rodku
        if (menuSlider.value == 1f)
        {
            // Je�li suwak jest na �rodku, ustaw g�o�no�� na pocz�tkow� warto��
            volume = initialMenuVolume;
        }
        else
        {
            // W przeciwnym razie, ustaw g�o�no�� na warto�� suwaka
            volume = menuSlider.value;
        }

        // Logowanie ustawionej g�o�no�ci
        Debug.Log("Set Menu Volume: " + volume);

        RuntimeManager.StudioSystem.setParameterByName(MenuMusicParameter, volume);
    }

    public void SetMenuVolumeSlider(float volume) {
        
        RuntimeManager.GetBus(menuBusName).setVolume(volume);
    } 
}






