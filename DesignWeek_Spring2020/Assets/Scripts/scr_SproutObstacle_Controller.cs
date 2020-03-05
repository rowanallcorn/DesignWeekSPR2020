using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_SproutObstacle_Controller : MonoBehaviour
{
    [SerializeField] private float health;
    scr_DeathSproutReset_Controller s_DeathSproutReset_Controller;
    private float waterGunned;

    private Animator anim;


    private void Start()
    {
        s_DeathSproutReset_Controller = GetComponent<scr_DeathSproutReset_Controller>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (health <= 0)
        { s_DeathSproutReset_Controller.Die(); }
    }
    public void takeDamage()//called from bullet
    {
        waterGunned += 1 * Time.deltaTime;
        if (health == 3&&waterGunned>.1f) DieABit();
        if (health == 2 && waterGunned > .62f) DieABit();
        if (health == 1 && waterGunned > .98f) DieABit();
    }
    void DieABit()
    {
        health -= 1;
        if (health > 0)
        { anim.SetTrigger("Hit"); }
    }


}
