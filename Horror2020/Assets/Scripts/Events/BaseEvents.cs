// Base Script for Event Triggers. Make a script with the function of what you want the event trigger to do.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEvents : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Event Trigger Entered");
        DoEvent();
    }

    //Event Function
    public abstract void DoEvent();

    //Rotate the character on the Y axis
    public bool RotateOnY(float degrees, float turnSpeed)
    {
        Quaternion Target = Quaternion.Euler(0, degrees, 0);
        return Target != (transform.rotation = Quaternion.RotateTowards(transform.rotation, Target, turnSpeed * Time.deltaTime));
    }

    //Move the character to in front of the player
    public bool MoveToPosition(Vector3 runEndPosition, float runSpeed, GameObject npc)
    {
        return runEndPosition != (npc.transform.position = Vector3.MoveTowards(npc.transform.position, runEndPosition, runSpeed * Time.deltaTime));
    }
}
