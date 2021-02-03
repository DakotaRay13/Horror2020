using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool hasAxe = false;
    public bool hasFlashlight = false;
    public bool canMove = false;
    public float flashLightIntensity;


    public void ResetGameManager()
    {
        hasAxe = false;
        hasFlashlight = false;
        canMove = false;
    }
    
}
