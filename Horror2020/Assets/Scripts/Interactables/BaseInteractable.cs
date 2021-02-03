using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The base class for any interactable object.
//Attach to the empty parent with the trigger collider
public abstract class BaseInteractable : MonoBehaviour
{
    public string interactMessage;
    private bool isActive = false;

    private void OnTriggerEnter(Collider Player)
    {
        if(Player.gameObject.tag == "Player")
        {
            isActive = true;
            Debug.Log("Player is in the [" + interactMessage + "] collider");
            GameObject.Find("UIManager").GetComponent<UIManager>().FillButtonPrompt("[E] to " + interactMessage);
        }
    }

    private void OnTriggerStay(Collider Player)
    {
        if(Input.GetButton("Interact") && isActive == true)
        {
            DeactivateThis();
            DoActionOnInteract();
        }
    }

    private void OnTriggerExit(Collider Player)
    {
        if (Player.gameObject.tag == "Player")
        {
            DeactivateThis();
            Debug.Log("Player is out of the [" + interactMessage + "] collider");
        }
    }

    private void DeactivateThis()
    {
        isActive = false;
        GameObject.Find("UIManager").GetComponent<UIManager>().EmptyButtonPrompt();
    }

    public abstract void DoActionOnInteract();
}
