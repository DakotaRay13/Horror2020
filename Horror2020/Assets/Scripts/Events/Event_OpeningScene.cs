using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_OpeningScene : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Camera;
    [SerializeField] Light[] lights;
    [SerializeField] GameObject ComputerScreen;
    
    
    void Start()
    {
        //Turn on the lights
        SetLights(true);

        //Lower the camera
        Camera.transform.localPosition = new Vector3(0f, 1.5f, 0f);

        //Start cutscene
        StartCoroutine(OpeningScene());
    }

    public IEnumerator OpeningScene()
    {
        //Give player plenty of time to read the screen
        yield return new WaitForSecondsRealtime(10f);

        //Power Outage
        SetLights(false);
        ComputerScreen.SetActive(false);

        //Turn off computer screen

        yield return new WaitForSecondsRealtime(3f);

        //Get up
        while(Camera.transform.localPosition.y != 2f)
        {
            Camera.transform.localPosition += new Vector3(0f, 0.01f, 0f);
            if (Camera.transform.localPosition.y > 2f)
                Camera.transform.localPosition = new Vector3(0f, 2f, 0f);
            yield return null;
        }

        yield return new WaitForSecondsRealtime(0.5f);

        //Unlock the movement and enable the flashlight
        Player.GetComponent<PlayerManager>().SetFlashlight(true);
        Player.GetComponent<PlayerManager>().SetMovement(true);
    }

    public void SetLights(bool setting)
    {
        foreach(Light light in lights)
        {
            light.enabled = setting;
        }
    }
}
