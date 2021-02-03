using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_DoorLocked : MonoBehaviour
{
    private void OnTriggerEnter(Collider Player)
    {
        if (Player.gameObject.tag == "Player")
        {
            GameObject.Find("UIManager").GetComponent<UIManager>().FillButtonPrompt("The door is locked. I need to turn on the power.");
        }
    }

    private void OnTriggerExit(Collider Player)
    {
        if (Player.gameObject.tag == "Player")
        {
            GameObject.Find("UIManager").GetComponent<UIManager>().EmptyButtonPrompt();
        }
    }
}
