using System;
using System.Collections;
using UnityEngine;
using FMODUnity;
using TMPro;
using Random = UnityEngine.Random;

public class RollingMinigame : MonoBehaviour
{
    [SerializeField] private RectTransform targetL;
    [SerializeField] private RectTransform targetR;
    [SerializeField] private RectTransform bounceL;
    [SerializeField] private RectTransform bounceR;
    [SerializeField] private RectTransform roller;
    [SerializeField] private float speed;
    private int score2;
    private float goalWidthL;
    private float goalWidthR;
    private float minWL;
    private float minWR;
    private float maxWL;
    private float maxWR;
    private float bounceWidthL;
    private float bounceWidthR;
    private float minWBL;
    private float minWBR;
    private float maxWBL;
    private float maxWBR;
    [SerializeField] private MinigameStarter minigameStarter;
    private Vector2 direction;
    [SerializeField] private RectTransform[] points2;

    [SerializeField] private ParticleSystem emit;
    
    private KeyCode _keyCode;
    [SerializeField] private TextMeshProUGUI inputText;
    private int _input;

    private int _previousInput = -1;

    [FMODUnity.EventRef]
    public string rollingSoundEvent; // FMOD event for rolling sound

    private bool mouseClickHandledRight = false;
    private bool mouseClickHandledLeft = false;

    private void OnEnable()
    {
        goalWidthL = targetL.sizeDelta.x;
        goalWidthR = targetR.sizeDelta.x ;
        minWL = targetL.anchoredPosition.x - goalWidthL;
        minWR = targetR.anchoredPosition.x - goalWidthR;
        maxWL = targetL.anchoredPosition.x + goalWidthL;
        maxWR = targetR.anchoredPosition.x + goalWidthR;
        roller.gameObject.SetActive(true);
        roller.anchoredPosition = new Vector2(0, 0);
        score2 = 0;
        direction = Vector2.right;
        bounceWidthL = targetL.sizeDelta.x * 0.01f;
        bounceWidthR = targetR.sizeDelta.x * 0.01f;
        minWBL = targetL.anchoredPosition.x - bounceWidthL;
        minWBR = targetR.anchoredPosition.x - bounceWidthR;
        maxWBL = targetL.anchoredPosition.x + bounceWidthL;
        maxWBR = targetR.anchoredPosition.x + bounceWidthR;
        AssignInput();
        foreach (RectTransform point in points2)
        {
            point.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        roller.anchoredPosition += direction * (Time.deltaTime * speed);

        // Nas�uchiwanie klikni�cia myszk� w odpowiednich miejscach
        if (Input.GetKeyDown(_keyCode) && !mouseClickHandledRight &&
            Math.Abs(roller.anchoredPosition.x - Mathf.Clamp(roller.anchoredPosition.x, minWR, maxWR)) < 0.0001f)
        {
            AssignInput();
            mouseClickHandledRight = true;
            DirLeft();
            score2 += 1;
            points2[score2 - 1].gameObject.SetActive(true);
            //UpdateText();

            // Play FMOD sound for rolling
            FMODUnity.RuntimeManager.PlayOneShot(rollingSoundEvent, roller.position);
        }
        else if (Input.GetKeyDown(_keyCode) && !mouseClickHandledLeft &&
            Math.Abs(roller.anchoredPosition.x - Mathf.Clamp(roller.anchoredPosition.x, minWL, maxWL)) < 0.0001f)
        {
            AssignInput();
            mouseClickHandledLeft = true;
            DirRight();
            score2 += 1;
            points2[score2 - 1].gameObject.SetActive(true);
            //UpdateText();

            // Play FMOD sound for rolling
            FMODUnity.RuntimeManager.PlayOneShot(rollingSoundEvent, roller.position);
        }
        
        if (roller.anchoredPosition.x > bounceR.anchoredPosition.x) // Math.Abs(roller.anchoredPosition.x - Mathf.Clamp(roller.anchoredPosition.x, minWBR, maxWBR +0.5f)) < 0.0001f
        {
            DirLeft();
            mouseClickHandledRight = false;
        }
        else if (roller.anchoredPosition.x < bounceL.anchoredPosition.x) // Math.Abs(roller.anchoredPosition.x - Mathf.Clamp(roller.anchoredPosition.x, minWBL, maxWBL)) < 0.0001f
        {
            DirRight();
            mouseClickHandledLeft = false;
        }

        if (score2 == 6)
        {
            StartCoroutine(Rolling());
        }
    }

    private void DirRight()
    {
        direction = Vector2.right;
    }

    private void DirLeft()
    {
        direction = Vector2.left;
    }

    private void AssignInput()
    {
        int newInput;

        do
        {
            newInput = Random.Range(0, 4);
        } while (newInput == _previousInput);

        _input = newInput;
        _previousInput = _input;

        switch (_input)
        {
            case 0:
                _keyCode = KeyCode.RightArrow;
                inputText.text = "Right";
                break;
            case 1:
                _keyCode = KeyCode.LeftArrow;
                inputText.text = "Left";
                break;
            case 2:
                _keyCode = KeyCode.DownArrow;
                inputText.text = "Down";
                break;
            case 3:
                _keyCode = KeyCode.UpArrow;
                inputText.text = "Up";
                break;
        }
    }
    IEnumerator Rolling()
    {
        yield return new WaitForSeconds(1f);
        emit.Emit(25);
        minigameStarter.RollingFinished();
    }
}
