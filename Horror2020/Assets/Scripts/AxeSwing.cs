using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSwing : MonoBehaviour
{
    //private void OnTriggerEnter(Collider collision)
    //{
    //    if (collision.gameObject.tag != "Player")
    //    {
    //        Debug.Log("Axe Hit Something");
    //        GetComponent<Collider>().enabled = false;
    //    }

    //    if (collision.gameObject.tag == "NPC")
    //    {
    //        gameObject.SetActive(false);
    //        collision.gameObject.GetComponent<NPC_Controller>().StartRagdoll();
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Debug.Log("Axe Hit Something");
            GetComponent<Collider>().enabled = false;
        }

        if (collision.gameObject.tag == "NPC")
        {
            gameObject.SetActive(false);
            GameObject.Find("Audio_Manager").GetComponent<audioManagerScript>().PlayAxeHit();
            GameObject.Find("Audio_Manager").GetComponent<audioManagerScript>().PlayScream();
            collision.gameObject.GetComponent<NPC_Controller>().StartRagdoll();
            collision.gameObject.GetComponent<NPC_Controller>().StartBlood();
        }
    }
}
