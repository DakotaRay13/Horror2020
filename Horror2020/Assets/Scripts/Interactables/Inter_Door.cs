using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inter_Door : BaseInteractable
{
    [SerializeField] SceneLoader SceneLoader;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider>().enabled = false;
    }

    public void UnlockDoor()
    {
        Debug.Log("Player unlocked the door");
        GetComponentInChildren<Light>().color = Color.green;
        GetComponent<Collider>().enabled = true;
    }

    public override void DoActionOnInteract()
    {
        Debug.Log("Player opened the door.");
        GameObject.Find("Player").GetComponent<PlayerManager>().SetMovement(false);

        StartCoroutine(SceneLoader.ChangeScenes("EndingScene"));
    }
}
