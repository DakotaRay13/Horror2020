using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Limit the framerate to 60fps
public class FramerateLimiter : MonoBehaviour
{
    void Awake()
    {
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;
    }
}
