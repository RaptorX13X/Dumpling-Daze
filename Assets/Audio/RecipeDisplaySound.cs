using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

[ExecuteInEditMode]
public class RecipeDisplaySound : MonoBehaviour

{
    public Canvas canvas;
    public string fmodEventPath = "event:/RecipeDisplay";  // Zast�p "YourEventPath" �cie�k� do Twojego zdarzenia FMOD
    //private bool canvasActive = false;

    private FMODUnity.StudioEventEmitter fmodEventEmitter;

    void Start()
    {
        // Na pocz�tku dezaktywuj Canvas
        if (canvas != null)
        {
            canvas.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            // Je�li tak, zmie� stan aktywno�ci Canvasa (w��cz lub wy��cz)
            if (canvas != null)
            {
                canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);

                // Odtw�rz zdarzenie d�wi�kowe FMOD po w��czeniu Canvasa
                if (canvas.gameObject.activeSelf)
                {
                    PlayFMODSound();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            // Je�li tak, zmie� stan aktywno�ci Canvasa (w��cz lub wy��cz)
            if (canvas != null)
            {
                canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);

                // Odtw�rz zdarzenie d�wi�kowe FMOD po w��czeniu Canvasa
                if (canvas.gameObject.activeSelf)
                {
                    PlayFMODSound();
                }
            }
        }
    }


    //void Update()
    //{
    //    // Sprawd�, czy klawisz "R" zosta� naci�ni�ty
    //    if (Input.GetKeyDown(KeyCode.Tab))
    //    {
    //        // Je�li tak, zmie� stan aktywno�ci Canvasa (w��cz lub wy��cz)
    //        if (canvas != null)
    //        {
    //            canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);

    //            // Odtw�rz zdarzenie d�wi�kowe FMOD po w��czeniu Canvasa
    //            if (canvas.gameObject.activeSelf)
    //            {
    //                PlayFMODSound();
    //            }
    //        }
    //    }
    //}


    void PlayFMODSound()
    {
        // Odtw�rz zdarzenie d�wi�kowe FMOD
        if (fmodEventEmitter != null)
        {
            fmodEventEmitter.Play();
        }
    }

#if UNITY_EDITOR


    void OnEnable()
    {
        // Upewnij si�, �e komponent FMOD Event Emitter jest obecny
        EnsureFMODEventEmitter();
    }


    void OnValidate()
    {
        // Weryfikuj komponent FMOD Event Emitter podczas edycji w Unity Editor
        EnsureFMODEventEmitter();
    }

    void EnsureFMODEventEmitter()
    {
        // Sprawd�, czy komponent FMOD Event Emitter jest obecny
        if (fmodEventEmitter == null)
        {
            // Je�li nie, dodaj go
            fmodEventEmitter = gameObject.GetComponent<FMODUnity.StudioEventEmitter>();
            if (fmodEventEmitter == null)
            {
                fmodEventEmitter = gameObject.AddComponent<FMODUnity.StudioEventEmitter>();
            }

            // Przypisz �cie�k� do zdarzenia FMOD
            fmodEventEmitter.Event = fmodEventPath;
        }
    }
#endif
}


