using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cullTrigger_OutsideLobby : MonoBehaviour
{
    //If the objects in this area are culled out, bring them back in.

    [SerializeField] GameObject[] ObjectsToActivate;

    private void OnTriggerEnter(Collider Player)
    {
        if(Player.tag == "Player")
        {
            foreach (GameObject obj in ObjectsToActivate)
            {
                if(obj.activeSelf == false)
                {
                    obj.SetActive(true);
                }
            }
        }
    }
}
