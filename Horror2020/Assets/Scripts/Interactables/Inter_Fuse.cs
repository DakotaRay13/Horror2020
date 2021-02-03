using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inter_Fuse : BaseInteractable
{
    public override void DoActionOnInteract()
    {
        GameObject.Find("Audio_Manager").GetComponent<audioManagerScript>().PlayStinger2();
        //Give the Player a fuse
        GameObject.Find("Player").GetComponent<PlayerManager>().GetFuse();

        //Destroy this fuse
        Destroy(gameObject);
    }
}
