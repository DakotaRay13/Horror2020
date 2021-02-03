using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject Buttons;
    [SerializeField] GameObject ComputerScreen;
    [SerializeField] SceneLoader SL;
    [SerializeField] GameManager GM;
    [SerializeField] GameObject brightnessSlider;
    [SerializeField] GameObject flashLight;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        brightnessSlider.GetComponent<Slider>().value = GM.flashLightIntensity;
        SetFlashLightIntensity();
        GM.ResetGameManager();
    }

    public void StartGame()
    {
        ComputerScreen.SetActive(false);
        Debug.Log("Start Button Works.");
        Buttons.SetActive(false);
        StartCoroutine(SL.ChangeScenes("MainOffice"));
    }
    
    //Function to exit the game
    public void ExitGame()
    {
        Application.Quit();
    }
    //
    public void SetFlashLightIntensity()
    {
        float val = 0f;
        val = brightnessSlider.GetComponent<Slider>().value;
        flashLight.GetComponent<Light>().intensity = val;
        GM.flashLightIntensity = val;
    }
}
