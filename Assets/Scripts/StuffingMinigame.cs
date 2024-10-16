using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffingMinigame : MonoBehaviour
{
    [SerializeField] private RectTransform[] arrows;
    [SerializeField] private RectTransform target;
    [SerializeField] private float speed;
    [SerializeField] private RectTransform bounceD;
    [SerializeField] private RectTransform bounceU;
    private int score5;
    [SerializeField] private RectTransform[] points5;
    private Vector2 direction;
    private bool mouseClickHandled = false;
    [SerializeField] private MinigameStarter minigameStarter;
    [SerializeField] private ParticleSystem emit;

    // Dodaj referencj� do zdarzenia d�wi�kowego w FMOD
    [FMODUnity.EventRef]
    public string stuffingSoundEvent;

    private void OnEnable()
    {
        direction = Vector2.down;
        score5 = 0;
        foreach (RectTransform arrow in arrows)
        {
            arrow.gameObject.SetActive(false);
        }
        foreach (RectTransform point in points5)
        {
            point.gameObject.SetActive(false);
        }

        target.anchoredPosition = new Vector2(0, 1.4f);
        arrows[0].gameObject.SetActive(true);
        //mouseClickHandled = false;
    }

    private void Update()
    {
        target.anchoredPosition += direction * (speed * Time.deltaTime);

        if (!mouseClickHandled)
        {
            if (Input.GetMouseButtonDown(0) && (Math.Abs(target.anchoredPosition.y -
                                                         Mathf.Clamp(target.anchoredPosition.y, (arrows[score5].anchoredPosition.y - arrows[score5].sizeDelta.y * 0.5f),
                                                             (arrows[score5].anchoredPosition.y + arrows[score5].sizeDelta.y * 0.5f))) < 0.0001f))


            {
                arrows[score5].gameObject.SetActive(false);
                score5 += 1;
                if (score5 < arrows.Length)
                {
                    arrows[score5].gameObject.SetActive(true);
                }
                points5[score5 - 1].gameObject.SetActive(true);

                // Odtw�rz d�wi�k FMOD dla klejenia
                FMODUnity.RuntimeManager.PlayOneShot(stuffingSoundEvent, target.position);

                if (score5 == arrows.Length)
                {
                    StartCoroutine(Stuffed());
                }

                mouseClickHandled = false; // Ustawione na true, aby unikn�� wielokrotnego wywo�ania w jednym naci�ni�ciu myszk�
            }
        }

        if (target.anchoredPosition.y <= bounceD.anchoredPosition.y)
        {
            direction = Vector2.up;
        }
        if (target.anchoredPosition.y >= bounceU.anchoredPosition.y)
        {
            direction = Vector2.down;
        }
    }

    IEnumerator Stuffed()
    {
        yield return new WaitForSeconds(1f);
        emit.Emit(25);
        minigameStarter.StuffingFinished();
    }
}
