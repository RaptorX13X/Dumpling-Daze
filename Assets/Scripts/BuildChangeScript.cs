using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildChangeScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene(3);
        }
    }
}
