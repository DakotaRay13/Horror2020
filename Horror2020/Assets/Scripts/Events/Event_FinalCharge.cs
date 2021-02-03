using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Event in the final room where the NPC charges the character.
    //Base event not needed since no trigger needed. Called at start.

public class Event_FinalCharge : MonoBehaviour
{
    [SerializeField] Vector3 runEndPosition;
    private float turnSpeed = 30f;
    private float runSpeed = 5f;

    //The NPC who will be charging
    [SerializeField] NPC_Controller chargingNPC;

    public float yRot;

    // Start is called before the first frame update
    void Start()
    {
        chargingNPC = gameObject.GetComponent<NPC_Controller>();

        //Set the Standing Animation to True and the others to false.
        chargingNPC.SetStand(true);

        StartCoroutine(FinalCharge());
    }

    public void StopCharge()
    {
        StopAllCoroutines();

        CutOffVoiceLines();

        StartCoroutine(BadEnding());
    }

    public IEnumerator FinalCharge()
    {
        yield return new WaitForSecondsRealtime(5f);

        while(RotateOnY())
        {
            yield return null;
        }

        yield return new WaitForSecondsRealtime(5f);

        //Stop the standing animation
        chargingNPC.SetRun(true);
        chargingNPC.anim.Play("CharRun");

        while (MoveToPosition())
        {
            yield return null;
        }

        chargingNPC.SetStand(true);
        chargingNPC.anim.enabled = false;
        chargingNPC.anim.enabled = true;
        chargingNPC.anim.Play("CharStand");

        Debug.Log("Character Reached Player.");
        yield return new WaitForSecondsRealtime(1f);

        if (GameObject.Find("Player").GetComponent<PlayerManager>().hasAxe)
        {
            StartCoroutine(BadEndingAxe());
        }
        else
        {
            StartCoroutine(GoodEnding());
        }
    }

    public IEnumerator GoodEnding()
    {
        Debug.Log("Good Ending");
        //have them hooray
        NPC_Controller[] bgChars = GameObject.Find("BackgroundCharacters").GetComponentsInChildren<NPC_Controller>();
        foreach (NPC_Controller NPC in bgChars)
        {
            NPC.SetHooray(true);
            NPC.anim.Play("CharHooray");
        }
        chargingNPC.SetHooray(true);
        chargingNPC.anim.Play("CharHooray");

        yield return new WaitForSecondsRealtime(0.5f);
        //Turn on the lights
        TurnOnLights();

        GameObject.Find("Audio_Manager").GetComponent<audioManagerScript>().PlaySuprise();
        yield return new WaitForSecondsRealtime(1.6f);

        GameObject.Find("Audio_Manager").GetComponent<audioManagerScript>().PlayHappyBirthday();
        yield return new WaitWhile(() => GameObject.Find("Audio_Manager").GetComponent<audioManagerScript>().happyBirthday.isPlaying);
        
        SceneManager.LoadScene("Credits");
    }

    public IEnumerator BadEndingAxe()
    {
        Debug.Log("Bad ending without killing");
        //Have shocked faces on the crowd
        NPC_Controller[] bgChars = GameObject.Find("BackgroundCharacters").GetComponentsInChildren<NPC_Controller>();
        foreach (NPC_Controller NPC in bgChars)
        {
            NPC.SetHooray(true);
            NPC.anim.Play("CharHooray");
        }
        chargingNPC.SetHooray(true);
        chargingNPC.anim.Play("CharHooray");

        yield return new WaitForSecondsRealtime(0.5f);
        //Turn on the lights
        TurnOnLights();
        GameObject.Find("Audio_Manager").GetComponent<audioManagerScript>().PlaySuprise();
        yield return new WaitForSecondsRealtime(1.6f);

        GameObject.Find("Audio_Manager").GetComponent<audioManagerScript>().PlayItWasDark();
        

        yield return new WaitForSecondsRealtime(2f);
        foreach (NPC_Controller NPC in bgChars)
        {
            NPC.SetStand(true);
            NPC.anim.Play("CharStand");
        }
        chargingNPC.SetStand(true);
        chargingNPC.anim.Play("CharStand");

        //Wait for ??? seconds
        yield return new WaitWhile(() => GameObject.Find("Audio_Manager").GetComponent<audioManagerScript>().itWasDark.isPlaying);
        SceneManager.LoadScene("Credits");
    }

    public IEnumerator BadEnding()
    {
        Debug.Log("Bad Ending Acheived");
        
        //Have shocked faces on the crowd
        NPC_Controller[] bgChars = GameObject.Find("BackgroundCharacters").GetComponentsInChildren<NPC_Controller>();
        foreach (NPC_Controller NPC in bgChars)
        {
            NPC.SetShocked(true);
        }

        yield return new WaitForSecondsRealtime(1f);

        //Turn on the lights
        TurnOnLights();
        
        //Wait for ??? seconds
        yield return new WaitForSecondsRealtime(8f);

        //Play sound effect
        GameObject.Find("Audio_Manager").GetComponent<audioManagerScript>().PlayPartyHorn();
        yield return new WaitForSecondsRealtime(3f);
        
        //Go to credits
        SceneManager.LoadScene("Credits");
        
    }

    public void CutOffVoiceLines()
    {
        if (GameObject.Find("Suprise").GetComponent<AudioSource>().isPlaying)
            GameObject.Find("Suprise").GetComponent<AudioSource>().Stop();

        if (GameObject.Find("ItWasDark").GetComponent<AudioSource>().isPlaying)
            GameObject.Find("ItWasDark").GetComponent<AudioSource>().Stop();
    }

    //Rotate the character on the Y axis
    public bool RotateOnY()
    {
        Quaternion Target = Quaternion.Euler(0, 180, 0);
        return Target != (transform.rotation = Quaternion.RotateTowards(transform.rotation, Target, turnSpeed * Time.deltaTime));
    }

    //Move the character to in front of the player
    public bool MoveToPosition()
    {
        return runEndPosition != (transform.position = Vector3.MoveTowards(transform.position, runEndPosition, runSpeed * Time.deltaTime));
    }

    public void TurnOnLights()
    {
        Light[] lights = GameObject.Find("Lights").GetComponentsInChildren<Light>();
        foreach(Light light in lights)
        {
            light.enabled = true;
        }
    }
}
