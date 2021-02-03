using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Controller : MonoBehaviour
{
    //Animation Bools
    [SerializeField] ParticleSystem system;
    [SerializeField] bool stand = true; // <-- Default Value
    [SerializeField] bool shocked = false;
    [SerializeField] bool hooray = false;
    [SerializeField] bool run = false;

    public bool isRagdoll;

    //Character GameObject
    [SerializeField] GameObject NPC;

    //Character Animator
    public Animator anim;

    //Ragdoll Axe
    [SerializeField] GameObject Axe;
    [SerializeField] Mesh axeMesh; // <-- FIXES ERROR OF AXE MESH IN AXE GETTING REPLACED

    //Character's editable Materials and Body Parts
    [SerializeField] GameObject[] BodyParts;
    [SerializeField] Material[] Materials;
    [SerializeField] Color[] materialColors;

    // Start is called before the first frame update
    void Awake()
    {
        //Set the collision
        isRagdoll = false;
        SetRagdollRBs(true);
        SetRagdollCols(false);
        
        //Get and Update the animation
        anim = NPC.GetComponent<Animator>();
        UpdateAnimation();

        //Set Color
        AssignColors();

        //Confirm Axe
        ConfirmAxe();

        
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    ////////////////////////////////////////////////////////////////////////////
    ///Animation Functions

    void UpdateAnimation()
    {
        anim.SetBool("stand", stand);
        anim.SetBool("shocked", shocked);
        anim.SetBool("hooray", hooray);
        anim.SetBool("run", run);
    }

    public void SetStand(bool value)
    {
        ResetAnimationBools();
        stand = value;
        UpdateAnimation();
    }

    public void SetShocked(bool value)
    {
        ResetAnimationBools();
        shocked = value;
        UpdateAnimation();
    }

    public void SetHooray(bool value)
    {
        ResetAnimationBools();
        hooray = value;
        UpdateAnimation();
    }

    public void SetRun(bool value)
    {
        ResetAnimationBools();
        run = value;
        UpdateAnimation();
    }

    public void ResetAnimationBools()
    {
        stand = false;
        shocked = false;
        hooray = false;
        run = false;
    }

    ////////////////////////////////////////////////////////////////////////////
    ///Collision Functions

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Axe")
    //    {
    //        Debug.Log("NPC was hit by an axe.");
    //        anim.enabled = false;
    //        SetRagdollRBs(false);
    //        SetRagdollCols(true);
    //    }
    //}

    //private void OnTriggerEnter(Collider collision)
    //{
    //    if (collision.tag == "Axe")
    //    {
    //        Debug.Log("NPC was hit by an axe.");
    //        anim.enabled = false;
    //        SetRagdollRBs(false);
    //        SetRagdollCols(true);
    //    }
    //}

    //Start Ragdoll
    public void StartRagdoll()
    {
        Debug.Log("NPC was hit by an axe.");
        anim.enabled = false;
        NPC.GetComponent<Event_FinalCharge>().StopCharge(); //Stop the charging animation
        SetRagdollRBs(false);
        SetRagdollCols(true);
    }

    private void SetRagdollRBs(bool setBool)
    {
        //Get RigidBodies
        Rigidbody[] RagdollRBs = GetComponentsInChildren<Rigidbody>();
        
        foreach(Rigidbody rigidbody in RagdollRBs)
        {
            rigidbody.isKinematic = setBool;
        }

        NPC.GetComponent<Rigidbody>().isKinematic = !setBool;
    }

    private void SetRagdollCols(bool setBool)
    {
        Collider[] RagdollCols = GetComponentsInChildren<Collider>();

        foreach (Collider collider in RagdollCols)
        {
            collider.enabled = setBool;
        }

        NPC.GetComponent<Collider>().enabled = !setBool;

        Axe.SetActive(setBool);
    }

    ///////////////////////////////////////////////////////////////////////////
    ///Color Change Functions
    
    public void AssignColors()
    {
        for(int i = 0; i < Materials.Length; i++)
        {
            Materials[i] = Instantiate(Materials[i]);
            Materials[i].color = materialColors[i];
            BodyParts[i].GetComponent<SkinnedMeshRenderer>().material = Materials[i];
        }
    }

    ///////////////////////////////////////////////////////////////////////////
    ///Error Fix Functions
    
    private void ConfirmAxe()
    {
        if(Axe.GetComponent<MeshFilter>().mesh != axeMesh)
        {
            Axe.GetComponent<MeshFilter>().mesh = axeMesh;
        }
    }
    public void StartBlood()
    {
        system.Play();
    }
}
