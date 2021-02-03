using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inter_AxeBox : BaseInteractable
{
    [SerializeField] GameObject axe;

    public override void DoActionOnInteract()
    {
        //Enable the player's axe
        GameObject.Find("Player").GetComponent<PlayerManager>().SetAxe(true);
        GameObject.Find("Audio_Manager").GetComponent<audioManagerScript>().PlayStinger4();

        axe.SetActive(false);

        //Disable the AxeBox's trigger collider
        GetComponent<Collider>().enabled = false;
    }
}
