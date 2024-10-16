using System;
using System.Collections;
using UnityEngine;
using FMODUnity;
using TMPro;
using Random = UnityEngine.Random;

public class GluingMinigame : MonoBehaviour
{
    [SerializeField] private RectTransform check;
    [SerializeField] private RectTransform[] goals;
    [SerializeField] private RectTransform[] points3;
    [SerializeField] private RectTransform bounceL;
    [SerializeField] private RectTransform bounceR;
    [SerializeField] private MinigameStarter minigameStarter;
    [SerializeField] private float speed;
    private int score3;
    private Vector2 direction;
    private int _input;
    private int _previousInput = -1;
    [SerializeField] private ParticleSystem emit;
    private KeyCode _keyCode;
    [SerializeField] private TextMeshProUGUI inputText;

    [FMODUnity.EventRef]
    public string gluingSoundEvent; // FMOD event for gluing sound

    private bool mouseClickHandled = false;

    private void OnEnable()
    {
        foreach (RectTransform goal in goals)
        {
            goal.gameObject.SetActive(false);
        }
        foreach (RectTransform point in points3)
        {
            point.gameObject.SetActive(false);
        }

        check.anchoredPosition = new Vector2(0, 0);
        score3 = 0;
        direction = Vector2.right;
        goals[0].gameObject.SetActive(true);
        AssignInput();
    }

    private void Update()
    {
        check.anchoredPosition += direction * (Time.deltaTime * speed);

        if (check.anchoredPosition.x > bounceR.anchoredPosition.x)
        {
            DirLeft();
        }
        if (check.anchoredPosition.x < bounceL.anchoredPosition.x)
        {
            DirRight();
        }

        // Sprawd�, czy klikni�cie myszk� w odpowiednich miejscach
        if (!mouseClickHandled)
        {
            if (Input.GetKeyDown(_keyCode) && (Math.Abs(check.anchoredPosition.x -
                                                       Mathf.Clamp(check.anchoredPosition.x, (goals[score3].anchoredPosition.x - goals[score3].sizeDelta.x * 0.5f),
                                                           (goals[score3].anchoredPosition.x + goals[score3].sizeDelta.x * 0.5f))) < 0.0001f))
            {
                AssignInput();
                goals[score3].gameObject.SetActive(false);
                score3 += 1;
                if (score3 < goals.Length)
                {
                    goals[score3].gameObject.SetActive(true);
                }
                points3[score3 - 1].gameObject.SetActive(true);

                // Play FMOD sound for gluing
                FMODUnity.RuntimeManager.PlayOneShot(gluingSoundEvent, check.position);

                if (score3 == goals.Length)
                {
                    StartCoroutine(Glued());
                }

                mouseClickHandled = false;
            }
        }
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
                _keyCode = KeyCode.Q;
                inputText.text = "Q";
                break;
            case 1:
                _keyCode = KeyCode.W;
                inputText.text = "W";
                break;
            case 2:
                _keyCode = KeyCode.E;
                inputText.text = "E";
                break;
            case 3:
                _keyCode = KeyCode.R;
                inputText.text = "R";
                break;
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

    IEnumerator Glued()
    {
        yield return new WaitForSeconds(1f);
        emit.Emit(25);
        minigameStarter.GluingFinished();
    }
}
