using System;
using System.Collections;
using UnityEngine;
using FMODUnity;
using Random = UnityEngine.Random;
using TMPro;
public class MinigameManager : MonoBehaviour
{
    [SerializeField] private RectTransform goal;
    [SerializeField] private RectTransform despawnPoint;
    [SerializeField] private RectTransform[] checks;
    [SerializeField] private float speed;
    private int score1;
    [SerializeField] private float timeBetweenChops = 1f;
    //[SerializeField] private TextMeshProUGUI counter;
    private float goalWidth;
    private float minW;
    private float maxW;
    [SerializeField] private MinigameStarter minigameStarter;
    [SerializeField] private RectTransform[] points1;
    [SerializeField] private TextMeshProUGUI[] inputTexts;
    private int currentInputText;
    
    private int _input;
    private int _previousInput = -1;
    private KeyCode _keyCode;
    [SerializeField] private ParticleSystem emit;

    [FMODUnity.EventRef]
    public string choppingSoundEvent; // FMOD event for chopping sound

    private void OnEnable()
    {
        goalWidth = goal.sizeDelta.x * 0.5f;
        minW = goal.anchoredPosition.x - goalWidth;
        maxW = goal.anchoredPosition.x + goalWidth;
        foreach (RectTransform check in checks)
        {
            check.gameObject.SetActive(false);
            check.anchoredPosition = new Vector2(-2.4f, 0f);
        }

        foreach (RectTransform point in points1)
        {
            point.gameObject.SetActive(false);
        }
        score1 = 0;
        //counter.text = score + " / 6";
        StartCoroutine(Chopping());
        AssignInput();
    }

    private void Start()
    {
        /*goalWidth = goal.sizeDelta.x * 0.5f;
        minW = goal.anchoredPosition.x - goalWidth;
        maxW = goal.anchoredPosition.x + goalWidth;
        foreach (RectTransform check in checks)
        {
            check.gameObject.SetActive(false);
        }

        foreach (RectTransform point in points1)
        {
            point.gameObject.SetActive(false);
        }
        score1 = 0;
        //counter.text = score + " / 6";
        StartCoroutine(Chopping());
        AssignInput();*/
    }

    private void Update()
    {
        for (int i = 0; i < checks.Length; i++)
        {
            if (checks[i].gameObject.activeInHierarchy)
            {
                checks[i].anchoredPosition += Vector2.right * (Time.deltaTime * speed);
                if (checks[i].anchoredPosition.x > despawnPoint.anchoredPosition.x)
                {
                    checks[i].gameObject.SetActive(false);
                }
                else if (inputTexts[i].text == Input.inputString && (Math.Abs(checks[i].anchoredPosition.x - Mathf.Clamp(checks[i].anchoredPosition.x, minW, maxW)) < 0.0001f))
                {
                    checks[i].gameObject.SetActive(false);
                    score1 += 1;
                    points1[score1 - 1].gameObject.SetActive(true);
                    //UpdateText();

                    // Play FMOD sound for chopping
                    FMODUnity.RuntimeManager.PlayOneShot(choppingSoundEvent, checks[i].position);
                }
            }
        }
    }

    private void AssignInput()
    {
        int newInput;
        do
        {
            newInput = Random.Range(0, 4);
        }while (newInput == _previousInput);

        _input = newInput;
        _previousInput = _input;

        switch (_input)
        {
            case 0:
                _keyCode = KeyCode.Alpha1;
                inputTexts[currentInputText].text = "1";
                break;
            case 1:
                _keyCode = KeyCode.Alpha2;
                inputTexts[currentInputText].text = "2";
                break;
            case 2:
                _keyCode = KeyCode.Alpha3;
                inputTexts[currentInputText].text = "3";
                break;
            case 3:
                _keyCode = KeyCode.Alpha4;
                inputTexts[currentInputText].text = "4";
                break;
        }
    }

    IEnumerator Chopping()
    {
        for (int i = 0; i < checks.Length; i++)
        {
            checks[i].gameObject.SetActive(true);
            currentInputText = i;
            AssignInput();
            yield return new WaitForSeconds(timeBetweenChops);
        }
        yield return new WaitWhile(() => checks[5].anchoredPosition.x < Mathf.Clamp(checks[5].anchoredPosition.x, minW, maxW));
        yield return new WaitForSeconds(timeBetweenChops * 2f);

        emit.Emit(25);
        minigameStarter.ChoppingFinished();
    }
}
