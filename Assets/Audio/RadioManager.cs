using UnityEngine;
using FMODUnity;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class RadioManager : MonoBehaviour
{
    private bool inRangeRadio = false;
    private bool isPlaying = false; // Zmienione na false, żeby muzyka nie grała automatycznie na początku
    private int currentSongIndex = 0;

    [SerializeField] private CanvasGroup radioCanvas;
    [SerializeField] private ParticleSystem notesParticle;

    [FMODUnity.EventRef]
    public string[] songEvents; // Tablica z identyfikatorami zdarzeń FMOD dla poszczególnych piosenek

    private FMOD.Studio.EventInstance currentEventInstance;
    
    public static FMOD.Studio.EventInstance GameAudioInstance;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            inRangeRadio = true;
            radioCanvas.DOFade(1f, 1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            inRangeRadio = false;
            radioCanvas.DOFade(0f, 1f);
        }
    }

    void Start()
    {
        GameAudioInstance = currentEventInstance;
        // Radio muzyka nie startuje automatycznie
        Debug.Log("RadioManager initialized");
    }

    public void StartRadioMusic()
    {
        if (!isPlaying && songEvents.Length > 0)
        {
            isPlaying = true;
            currentEventInstance = FMODUnity.RuntimeManager.CreateInstance(songEvents[currentSongIndex]);
            currentEventInstance.start();
            currentEventInstance.release(); // Zwolnienie instancji, aby można było ją później zatrzymać
            Debug.Log("Radio music started: " + songEvents[currentSongIndex]);
        }
    }

    void Update()
    {
        // if (SceneManager.GetActiveScene().name == "UI")
        // {
        //     Debug.Log("kurwa3");
        //     // Wy��cz muzyk� z menu
        //     StopGameMusic();
        // }
        if (inRangeRadio)
        {
            // Przełączanie między włączaniem/wyłączaniem radia
            if (Input.GetKeyDown(KeyCode.F))
            {
                isPlaying = !isPlaying;

                if (isPlaying)
                {
                    if (currentEventInstance.isValid()) // Sprawdzenie, czy istnieje poprzednia instancja
                    {
                        currentEventInstance.release(); // Zwolnienie poprzedniej instancji
                    }

                    currentEventInstance = FMODUnity.RuntimeManager.CreateInstance(songEvents[currentSongIndex]);
                    currentEventInstance.start(); // Włącz piosenkę
                    notesParticle.Play();
                    Debug.Log("Radio music resumed: " + songEvents[currentSongIndex]);
                }
                else
                {
                    currentEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); // Wyłącz piosenkę z fade-out
                    notesParticle.Stop();
                    Debug.Log("Radio music stopped");
                }
            }

            // Przełączanie piosenek
            if (Input.GetKeyDown(KeyCode.G))
            {
                SwitchSong();
            }
        }
    }

    void SwitchSong()
    {
        if (songEvents.Length > 1)
        {
            currentEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); // Zatrzymaj aktualnie odtwarzaną piosenkę z fade-out
            currentSongIndex = (currentSongIndex + 1) % songEvents.Length; // Przełącz między piosenkami

            currentEventInstance = FMODUnity.RuntimeManager.CreateInstance(songEvents[currentSongIndex]);
            currentEventInstance.start();
            currentEventInstance.release(); // Zwolnienie instancji, aby można było ją później zatrzymać
            Debug.Log("Switched to next song: " + songEvents[currentSongIndex]);
        }
    }
    public void StopGameMusic()
    {
        Debug.Log("kurwa");
        // Sprawd�, czy istnieje instancja d�wi�ku z menu
        if (GameAudioInstance.isValid())
        {
            Debug.Log("kurwa2");
            // Zatrzymaj d�wi�k
            GameAudioInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

            // Zwolnij zasoby
            GameAudioInstance.release();
        }

        // Zniszcz obiekt, kt�ry zawiera skrypt MenuAudio
        Destroy(gameObject);
    }
}
