using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using FMODUnity;
using TMPro;
using Vector3 = UnityEngine.Vector3;

public class Mixing2Minigame : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private RectTransform mixingIndicator;
    [SerializeField] private MinigameStarter minigameStarter;
    [SerializeField] private ParticleSystem emit;

    private KeyCode _keyCode;
    private int _input;
    [SerializeField] private TextMeshProUGUI inputText;
    
    [FMODUnity.EventRef]
    public string loopedSoundEvent; // FMOD Event for looped sound

    private bool hasStartedLoop = false;
    private FMOD.Studio.EventInstance loopedSoundInstance;
    
    

    private void OnEnable()
    {
        // Play the looped FMOD sound when the minigame starts
        loopedSoundInstance = RuntimeManager.CreateInstance(loopedSoundEvent);
        loopedSoundInstance.start();
        mixingIndicator.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        hasStartedLoop = false;
        AssignInput();
    }

    private void Update()
    {
        if (Input.GetKey(_keyCode) && mixingIndicator.localScale.x < 1)
        {
            mixingIndicator.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            AssignInput();

            if (!hasStartedLoop && mixingIndicator.localScale.x >= 0.99f)
            {
                hasStartedLoop = true;
                StartCoroutine(Mixing2());
            }
        }
    }

    private void AssignInput()
    {
        if (_input < 3)
        {
            _input += 1;
        }
        else
        {
            _input = 0;
        }
        switch (_input)
        {
            case 0:
                _keyCode = KeyCode.Q;
                inputText.text = "Q";
                break;
            case 1:
                _keyCode = KeyCode.W;
                inputText.text = "W";
                break;
            case 2:
                _keyCode = KeyCode.S;
                inputText.text = "S";
                break;
            case 3:
                _keyCode = KeyCode.A;
                inputText.text = "A";
                break;
        }
    }
    IEnumerator Mixing2()
    {
        yield return new WaitForSeconds(1f);

        // Zatrzymaj d�wi�k po zako�czeniu minigry
        loopedSoundInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

        emit.Emit(25);
        minigameStarter.Mixing2Finished();
    }
}




