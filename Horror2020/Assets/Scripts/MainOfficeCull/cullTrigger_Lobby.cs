using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cullTrigger_Lobby : MonoBehaviour
{
    //Sets active on every prop and light outside the lobby to false when player enters the lobby

    [SerializeField] GameObject[] ObjectsToCull;

    private void OnTriggerEnter(Collider Player)
    {
        if(Player.tag == "Player")
        {
            Debug.Log("Player entered the Lobby Cull Trigger");

            foreach (GameObject obj in ObjectsToCull)
            {
                if(obj.activeSelf == true)
                {
                    obj.SetActive(false);
                }
            }
        }
    }
}
