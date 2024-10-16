using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void LoadTargetScene()
    {
        // Zatrzymaj odtwarzanie dŸwiêku z menu przed przejœciem do nowej sceny
        if (MenuAudio.MenuAudioInstance.isValid())
        {
            MenuAudio.MenuAudioInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            MenuAudio.MenuAudioInstance.release();
        }

        // Za³aduj okreœlon¹ scenê
        SceneManager.LoadScene("CustomerImplementation");
    }
}

