using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    [SerializeField] private RectTransform goal;
    [SerializeField] private GameObject despawnPoint;
    [SerializeField] private RectTransform[] checks;
    [SerializeField] private float speed;
    private int score;
    [SerializeField] private float timeBetweenChops = 1f;
    [SerializeField] private TextMeshProUGUI counter;

    private void Start()
    {
        foreach (RectTransform check in checks)
        {
            check.gameObject.SetActive(false);
        }
        StartCoroutine(Chopping());
        score = 0;
        counter.text = score + " / 6";
    }

    private void Update()
    {
        float goalWidth = goal.sizeDelta.x * 0.5f;
        float minW = goal.transform.position.x - goalWidth;
        float maxW = goal.transform.position.x + goalWidth;
        for (int i = 0; i < checks.Length; i++)
        {
            if (checks[i].gameObject.activeInHierarchy)
            {
                checks[i].gameObject.transform.position += Vector3.right * (Time.deltaTime * speed);
                if (checks[i].gameObject.transform.position.x > despawnPoint.transform.position.x)
                {
                    checks[i].gameObject.SetActive(false);
                }
                else if (Input.GetMouseButtonDown(0) && (checks[i].gameObject.transform.position.x == Mathf.Clamp(checks[i].gameObject.transform.position.x ,minW , maxW)))
                {
                    checks[i].gameObject.SetActive(false);
                    score += 1;
                    UpdateText();
                }
            }
        }
    }

    private void UpdateText()
    {
        counter.text = score + " / 6";
    }

    IEnumerator Chopping()
    {
        for (int i = 0; i < checks.Length; i++)
        {
            checks[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(timeBetweenChops);
        }
    }
    
}
