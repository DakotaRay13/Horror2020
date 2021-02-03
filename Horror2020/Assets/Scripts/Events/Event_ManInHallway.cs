using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_ManInHallway : BaseEvents
{
    [SerializeField] GameObject ManInHall;
    [SerializeField] GameObject door;
    
    // Start is called before the first frame update
    void Start()
    {
        door.SetActive(false);
    }

    public override void DoEvent()
    {
        StartCoroutine(MoveChar());
        GetComponent<Collider>().enabled = false;
    }

    public IEnumerator MoveChar()
    {
        ManInHall.GetComponent<NPC_Controller>().SetRun(true);
        ManInHall.GetComponent<Animator>().Play("CharRun");
        Vector3 targetPos = new Vector3(ManInHall.transform.position.x - 3f, ManInHall.transform.position.y, ManInHall.transform.position.z);
        ManInHall.transform.Rotate(0f, -90f, 0);
        while (MoveToPosition(targetPos, 6f, ManInHall))
        {
            yield return null;
        }

        door.SetActive(true);
        GameObject.Find("Audio_Manager").GetComponent<audioManagerScript>().PlayAxeHit();
        ManInHall.SetActive(false);
    }
}
