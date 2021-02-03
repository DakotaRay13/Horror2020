using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_ManUpstairsOFF : BaseEvents
{
    [SerializeField] GameObject ManUpstairs;
    [SerializeField] Collider NextTrigger;

    // Start is called before the first frame update
    void Start()
    {
        //NextTrigger.enabled = false;
    }

    public override void DoEvent()
    {
        ManUpstairs.SetActive(false);
        //NextTrigger.enabled = true;
        GetComponent<Collider>().enabled = false;
    }
}
