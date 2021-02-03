using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject mainMenuUI;
    [SerializeField] GameObject brightnessMenu;
    [SerializeField] GameObject brightnessMenuUI;
    // Start is called before the first frame update
    void Awake()
    {
        goToMainMenu();
    }

    // Update is called once per frame
    public void goToMainMenu()
    {
        mainMenu.SetActive(true);
        brightnessMenu.SetActive(false);
        mainMenuUI.SetActive(true);
        brightnessMenuUI.SetActive(false);
    }
    public void goToBrightnessMenu()
    {
        mainMenu.SetActive(false);
        brightnessMenu.SetActive(true);
        mainMenuUI.SetActive(false);
        brightnessMenuUI.SetActive(true);
    }

}
