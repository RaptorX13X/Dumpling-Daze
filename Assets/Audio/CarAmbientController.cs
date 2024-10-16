using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity; 

public class CarAmbientController : MonoBehaviour
{
    [EventRef]
    public string ambientEvent;

    private FMOD.Studio.EventInstance ambientInstance;

    void Start()
    {
        ambientInstance = FMODUnity.RuntimeManager.CreateInstance(ambientEvent);
        ambientInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
        ambientInstance.start();
    }

    // Dodaj dodatkowe funkcje do kontroli dŸwiêku zgodnie z potrzebami gry.
}
