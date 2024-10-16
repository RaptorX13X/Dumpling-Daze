using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform playerBody;
    [SerializeField] private PlayerStats playerSensitivity;
    private float xRotation = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * playerSensitivity.mouseSensitivity * Time.deltaTime; 
        float mouseY = Input.GetAxis("Mouse Y") * playerSensitivity.mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
