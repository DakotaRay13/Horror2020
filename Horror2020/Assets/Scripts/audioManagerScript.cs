using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManagerScript : MonoBehaviour
{
    [SerializeField] private AudioSource partyHorn;
    [SerializeField] private AudioSource disapointment;
    [SerializeField] private AudioSource scream;
    [SerializeField] private AudioSource axeHit;
    [SerializeField] private AudioSource stinger1;
    [SerializeField] private AudioSource stinger2;
    [SerializeField] private AudioSource stinger3;
    [SerializeField] private AudioSource stinger4;
    [SerializeField] private AudioSource heroMotif1;
    [SerializeField] private AudioSource heroMotif2;
    [SerializeField] public AudioSource suprise;
    [SerializeField] public AudioSource happyBirthday;
    [SerializeField] public AudioSource itWasDark;

    public void PlayStinger1()
    {
        stinger1.Play();
    }
    public void PlayStinger2()
    {
        stinger2.Play();
    }
    public void PlayStinger3()
    {
        stinger3.Play();
    }
    public void PlayStinger4()
    {
        stinger4.Play();
    }
    public void PlayHeroMotif1()
    {
        heroMotif1.Play();
    }
    public void PlayHeroMotif2()
    {
        heroMotif2.Play();
    }
    public void PlayScream()
    {
        scream.Play();
    }
    public void PlayAxeHit()
    {
        axeHit.Play();
    }
    public void PlayPartyHorn()
    {
        partyHorn.Play();
    }
    public void PlayDisapointment()
    {
        disapointment.Play();
    }
    public void PlaySuprise()
    {
        suprise.Play();
    }
    public void PlayHappyBirthday()
    {
        happyBirthday.Play();
    }
    public void PlayItWasDark()
    {
        itWasDark.Play();
    }
}
