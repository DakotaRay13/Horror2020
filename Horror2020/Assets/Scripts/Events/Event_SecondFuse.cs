using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_SecondFuse : BaseEvents
{
    [SerializeField] Collider nextTrigger;
    
    // Start is called before the first frame update
    void Start()
    {
        nextTrigger.enabled = false;
    }

    public override void DoEvent()
    {
        Debug.Log("At Second Fuse");
    }

    private void OnTriggerStay(Collider Player)
    {
        if (Player.tag == "Player")
        {
            if (Input.GetButton("Interact"))
            {
                //Set up the next Event Trigger
                nextTrigger.enabled = true;

                GetComponent<Collider>().enabled = false;
            }
        }
    }
}
