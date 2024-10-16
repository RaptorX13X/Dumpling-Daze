using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixing1Minigame : MonoBehaviour
{
    [SerializeField] private RectTransform spinningTarget;
    [SerializeField] private RectTransform spinningTargetPoint;
    [SerializeField] private RectTransform[] targets;
    [SerializeField] private float speed;
    [SerializeField] private MinigameStarter minigameStarter;
    [SerializeField] private RectTransform[] points4;
    private int score4;
    private float currentRotation;
    private float rotationMin;
    private float rotationMax;

    private void Start()
    {
        score4 = 0;
        currentRotation = 0;
        //minigameStarter.StopMovement();
        foreach (RectTransform point in points4)
        {
            point.gameObject.SetActive(false);
        }
        foreach (RectTransform target in targets)
        {
            target.gameObject.SetActive(false);
        }
        targets[0].gameObject.SetActive(true);
    }

    private void Update()
    {
        currentRotation -= speed * Time.deltaTime;
        spinningTarget.localRotation = Quaternion.Euler(0f, 0f, currentRotation);

        float rotationAngle = spinningTarget.eulerAngles.z;
        float rotationLength = spinningTarget.rect.height /2f;
        float angleInRadians = -rotationAngle * Mathf.Deg2Rad;
        float tipX = spinningTarget.anchoredPosition.x + rotationLength * Mathf.Cos(angleInRadians);
        float tipY = spinningTarget.anchoredPosition.y + rotationLength * Mathf.Sin(angleInRadians);
        Vector2 tipPosition = new Vector2(tipX, tipY);
        
        
        RectTransform target = targets[score4];
        Vector2 targetPosition = target.anchoredPosition;
        Rect targetRect = new Rect(
            targetPosition.x - target.rect.width / 2f,
            targetPosition.y - target.rect.height / 2f,
            target.rect.width,
            target.rect.height);
        Debug.DrawLine(tipPosition, targetPosition, Color.red);
        // Debug.DrawLine(new Vector3(targetRect.x, targetRect.y), new Vector3(targetRect.x + targetRect.width, targetRect.y ),Color.green);
        // Debug.DrawLine(new Vector3(targetRect.x, targetRect.y), new Vector3(targetRect.x , targetRect.y + targetRect.height), Color.red);
        // Debug.DrawLine(new Vector3(targetRect.x + targetRect.width, targetRect.y + targetRect.height), new Vector3(targetRect.x + targetRect.width, targetRect.y), Color.green);
        // Debug.DrawLine(new Vector3(targetRect.x + targetRect.width, targetRect.y + targetRect.height), new Vector3(targetRect.x, targetRect.y + targetRect.height), Color.red);
        if (targetRect.Contains(tipPosition))
        {
            Debug.Log("aaaaaaaaaaaa");
        }
        if (Input.GetMouseButtonDown(0) && targetRect.Contains(tipPosition))
        {
            targets[score4].gameObject.SetActive(false);
            score4 += 1;
            if (score4 < targets.Length)
            {
                targets[score4].gameObject.SetActive(true);
            }
            points4[score4 - 1].gameObject.SetActive(true);
        }
    }
}
