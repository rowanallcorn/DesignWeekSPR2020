using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_SproutObstacle_Controller : MonoBehaviour
{
    [SerializeField] private float health;
    scr_DeathSproutReset_Controller s_DeathSproutReset_Controller;
    private float waterGunned;
    [SerializeField] private float lifeInSeconds;
    private bool wasShot;
    private Animator anim;
    private AudioSource audio;
    [SerializeField] private AudioClip hitAudio;
    [SerializeField] private AudioClip destroyedAudio;


    private void Start()
    {
        audio = GetComponent<AudioSource>();
        s_DeathSproutReset_Controller = GetComponent<scr_DeathSproutReset_Controller>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (health <= 0&&!wasShot)
        { StartCoroutine(Die()); wasShot = true; }
        waterGunned+= 1/ lifeInSeconds * Time.deltaTime;
        if (health == 3 && waterGunned > .5f) DieABit();
        if (health == 2 && waterGunned > .8f) DieABit();
        if (health == 1 && waterGunned > .99f) DieABit();
    }
    public void takeDamage()//called from bullet
    {  waterGunned += 1 * Time.deltaTime;
        if (!wasShot)
        {
            audio.PlayOneShot(hitAudio);
            wasShot = true;
            StartCoroutine(Die());
        }
    }
    void DieABit()
    {
        health -= 1;
        if (health > 0)
        { anim.SetTrigger("Hit"); }
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(.8f);
        audio.PlayOneShot(destroyedAudio);
        //print("die");
        yield return new WaitForSeconds(.2f);
        s_DeathSproutReset_Controller.Die();
    }


}
