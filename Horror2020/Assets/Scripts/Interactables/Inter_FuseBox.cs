using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inter_FuseBox : BaseInteractable
{
    [SerializeField] GameObject[] fusesInBox = new GameObject[3];
    [SerializeField] GameObject door;
    [SerializeField] GameObject doorLock;
    [SerializeField] Light doorLight;

    public void Awake()
    {
        foreach(GameObject fuse in fusesInBox)
        {
            fuse.SetActive(false);
        }
    }

    public override void DoActionOnInteract()
    {
        InsertFuse();
        GameObject.Find("Audio_Manager").GetComponent<audioManagerScript>().PlayStinger1();

        ////If the 3rd fuse is placed in the box, disable the trigger
        //if (fusesInBox[2].activeSelf == true)
        //{
        //    GetComponent<Collider>().enabled = false;
        //}

        //If second fuse placed in, disable the trigger and unlock the door
        if(fusesInBox[2].activeSelf == true)
        {
            GetComponent<Collider>().enabled = false;
            door.GetComponent<Inter_Door>().UnlockDoor();
            doorLock.GetComponent<Collider>().enabled = false;
            doorLight.enabled = true;
        }
    }

    private void InsertFuse()
    {
        //How many fuses does the player have
        int playerFuses = GameObject.Find("Player").GetComponent<PlayerManager>().fuses;

        //For each fuse in the box, if the player has enough fuses and they aren't already placed in, place the fuse in the box.
        for (int i = 0; i < fusesInBox.Length; i++)
        {
            if(i < playerFuses && fusesInBox[i].activeSelf == false)
            {
                fusesInBox[i].SetActive(true);
            }
        }
    }
}
