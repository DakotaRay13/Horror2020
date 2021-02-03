using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Axe Gameobject
    [SerializeField] GameObject axe;
    [SerializeField] GameObject flashlight;
    Animator anim;

    //Amount of fuses obtained
    public int fuses = 0;

    //Does the player have the axe
    private bool hasAxe;
    
    void Start()
    {
        flashlight.SetActive(true);
        DisableAxe();
        anim = axe.GetComponent<Animator>();
        axe.GetComponent<Collider>().enabled = false;

        //For Testing
        //EnableAxe();
    }

    public void DisableAxe()
    {
        hasAxe = false;
        axe.SetActive(false);
    }

    public void EnableAxe()
    {
        hasAxe = true;
        axe.SetActive(true);
    }

    public void UseAxe()
    {
        if(hasAxe)
        {
            if(Input.GetButtonDown("Attack"))
            {
                anim.SetBool("isAttacking", true);
                axe.GetComponent<Collider>().enabled = true;
            }

            else if (Input.GetButtonUp("Attack"))
            {
                anim.SetBool("isAttacking", false);
                axe.GetComponent<Collider>().enabled = false;
            }
        }
    }

    public void CheckFlashlight()
    {
        if(Input.GetButtonDown("Flashlight"))
        {
            if(flashlight.activeSelf == true)
            {
                flashlight.SetActive(false);
            }
            else if (flashlight.activeSelf == false)
            {
                flashlight.SetActive(true);
            }
        }
    }

    public void GetFuse()
    {
        fuses++;
    }
}
