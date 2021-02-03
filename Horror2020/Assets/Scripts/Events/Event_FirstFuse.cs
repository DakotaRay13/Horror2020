using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_FirstFuse : BaseEvents
{
    [SerializeField] GameObject DoorToOpen;
    [SerializeField] Collider NextTrigger;

    public void Start()
    {
        NextTrigger.enabled = false;
    }

    public override void DoEvent()
    {
        Debug.Log("Player is at the first fuse");
    }

    private void OnTriggerStay(Collider Player)
    {
        if(Player.tag == "Player")
        {
            if(Input.GetButton("Interact"))
            {
                //Spawn character behind player

                //Open up door in the lobby
                DoorToOpen.SetActive(false);

                //Set up the next Event Trigger
                NextTrigger.enabled = true;

                GetComponent<Collider>().enabled = false;
            }
        }
    }
}
