using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_ManUpstairsON : BaseEvents
{
    [SerializeField] GameObject ManUpstairs;
    [SerializeField] Collider NextTrigger;
    
    // Start is called before the first frame update
    void Start()
    {
        
        ManUpstairs.SetActive(false);
        NextTrigger.enabled = false;
    }

    public override void DoEvent()
    {
        ManUpstairs.SetActive(true);
        ManUpstairs.GetComponent<NPC_Controller>().SetStand(true);
        ManUpstairs.GetComponent<Animator>().Play("CharStand");
        NextTrigger.enabled = true;
        GetComponent<Collider>().enabled = false;
    }
}
