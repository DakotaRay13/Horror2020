using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_FirstSighting : BaseEvents
{
    [SerializeField] GameObject firstSighting;

    public override void DoEvent()
    {
        StartCoroutine(MoveChar());
        GetComponent<Collider>().enabled = false;
    }

    public IEnumerator MoveChar()
    {
        Vector3 targetPos = new Vector3(firstSighting.transform.position.x, firstSighting.transform.position.y, firstSighting.transform.position.z + 3f);
        while (MoveToPosition(targetPos, 7f, firstSighting))
        {
            yield return null;
        }
        firstSighting.SetActive(false);
    }
}
