# Dumpling Daze

### About the project
Dumpling Daze was a 8 month long university project finished in June 2024. In this game player operates their own dumpling restaurant - they can buy ingredients for cooking, make dumplings and serve them to the customers to earn money for more ingredients. 
When the right ingredients are on the cooking table players can start a randomly selected minigame to make a dumpling. 

### How does it work
```csharp
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
```

This minigame represents the mixing of ingredients. The UI elements pops up above the cooking station and player has to follow the prompts in order QWSA to indicate mixing in a circle. 
- The indicator showing progress is a green translucent circle growing in size until it matches the bowl. It starts with the scale value of 0.1f
- The assign input method is responsible for changing the inputs between Q W S and A in correct order. Every time its called the _input int is increased by 1 as long as its smaller than 3, otherwise it gets reset to 0, then the switch checks the _input value and assigns the correct key code.
- In the update method the if statement checks for the correct input when the scale of the indicator is smaller than 1. Then the scale is increased by a vector3 with values of 0.1f.
- If the indicator scale reaches bigger or equal to 0.99f the Mixing2 coroutine gets called
- The Mixing2 coroutine is responsible for finishing the minigame. It waits for one second, emits the particles and calls the finish method from the minigame manager, which releases the players controls, disables the minigame UI and resets it.
