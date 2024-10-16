using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void LoadTargetScene()
    {
        // Zatrzymaj odtwarzanie d�wi�ku z menu przed przej�ciem do nowej sceny
        if (MenuAudio.MenuAudioInstance.isValid())
        {
            MenuAudio.MenuAudioInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            MenuAudio.MenuAudioInstance.release();
        }

        // Za�aduj okre�lon� scen�
        SceneManager.LoadScene("CustomerImplementation");
    }
}

