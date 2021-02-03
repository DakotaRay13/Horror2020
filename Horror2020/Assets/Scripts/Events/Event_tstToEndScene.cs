using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_tstToEndScene : BaseEvents
{
    [SerializeField] SceneLoader SL;
    public override void DoEvent()
    {
        StartCoroutine(SL.ChangeScenes("EndingScene"));
    }
}
