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


    private void Start()
    {
        s_DeathSproutReset_Controller = GetComponent<scr_DeathSproutReset_Controller>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (health <= 0&&!wasShot)
        { StartCoroutine(Die(0)); }
        waterGunned+= 1/ lifeInSeconds * Time.deltaTime;
        if (health == 3 && waterGunned > .33f) DieABit();
        if (health == 2 && waterGunned > .66f) DieABit();
        if (health == 1 && waterGunned > .99f) DieABit();
    }
    public void takeDamage()//called from bullet
    {  waterGunned += 1 * Time.deltaTime;
        if (!wasShot)
        {
            wasShot = true;
            StartCoroutine(Die(1));
        }

    }
    void DieABit()
    {
        health -= 1;
        if (health > 0)
        { anim.SetTrigger("Hit"); }
    }
    IEnumerator Die(float delay)
    {
        yield return new WaitForSeconds(delay);
        s_DeathSproutReset_Controller.Die();
    }


}
