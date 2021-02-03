using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_RunnerDownstairs : BaseEvents
{
    [SerializeField] GameObject runner;

    public override void DoEvent()
    {
        
        StartCoroutine(MoveChar());
        GetComponent<Collider>().enabled = false;
    }

    public IEnumerator MoveChar()
    {
        runner.GetComponent<NPC_Controller>().SetRun(true);
        runner.GetComponent<Animator>().Play("CharRun");
        Vector3 targetPos = new Vector3(runner.transform.position.x + 12f, runner.transform.position.y, runner.transform.position.z);
        while (MoveToPosition(targetPos, 6f, runner))
        {
            yield return null;
        }

        runner.transform.Rotate(0f, 90f, 0f);
        targetPos = new Vector3(runner.transform.position.x, runner.transform.position.y, runner.transform.position.z - 1.3f);
        while (MoveToPosition(targetPos, 6f, runner))
        {
            yield return null;
        }

        runner.SetActive(false);
    }
}
