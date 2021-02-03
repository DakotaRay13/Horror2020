using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    //Script contains functions required to fade into scenes

    public Image transition;
    public Animator anim;

    public IEnumerator ChangeScenes(string scene)
    {
        Debug.Log("Scene Transition Started.");
        anim = transition.GetComponent<Animator>();
        anim.SetBool("fade", true);
        yield return new WaitUntil(() => transition.color.a == 1);
        SceneManager.LoadScene(scene);
    }


}
