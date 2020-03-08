using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Sound_Manager : MonoBehaviour
{

    public AudioClip stun, walk, water, refill, pumpkinBreak, turretShoot, flowerHit, flowerDeath, spawnablePlacement, dropletUI, turretDeath;
    static AudioSource audioSrc;


    private void Start()
    {
        
    }

    public void PlaySound (string clip)
    {
        switch (clip)
        {
            case "walk":
                audioSrc.PlayOneShot (walk);
                    break;
            case "stun":
                audioSrc.PlayOneShot(stun);
                break;
            case "water":
                audioSrc.PlayOneShot(water);
                break;
            case "refill":
                audioSrc.PlayOneShot(refill);
                break;
            case "pumpkinBreak":
                audioSrc.PlayOneShot(pumpkinBreak);
                break;
        }
    }
}
    
