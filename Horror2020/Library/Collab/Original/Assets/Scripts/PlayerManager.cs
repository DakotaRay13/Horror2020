using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Game Manager
    [SerializeField] GameManager gameManager;
    
    //Axe Gameobject
    [SerializeField] GameObject axe;
    [SerializeField] GameObject flashlight;
    Animator anim;

    //Amount of fuses obtained
    public int fuses = 0;

    //Does the player have the axe
    public bool hasAxe;
    public bool hasFlashlight;
    public bool canMove;
    
    void Start()
    {
        //Get Axe setting from gameManager
        SetAxe(gameManager.hasAxe);

        //Get Flashlight setting from gameManager
        SetFlashlight(gameManager.hasFlashlight);

        //Get Movement setting from gameManager
        SetMovement(gameManager.canMove);

        anim = axe.GetComponent<Animator>();
        axe.GetComponent<Collider>().enabled = false;
    }

    public void SetAxe(bool setting)
    {
        hasAxe = setting;
        axe.SetActive(setting);
        gameManager.hasAxe = setting;
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

    public void SetMovement(bool setting)
    {
        canMove = setting;
        gameManager.canMove = setting;
    }

    public void SetFlashlight(bool setting)
    {
        hasFlashlight = setting;
        flashlight.SetActive(setting);
        gameManager.hasFlashlight = setting;
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
