using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Fullscreen : MonoBehaviour
{
   public void Change()
    {
        Screen.fullScreen = !Screen.fullScreen;
        print("change screen modew");
    }
}
