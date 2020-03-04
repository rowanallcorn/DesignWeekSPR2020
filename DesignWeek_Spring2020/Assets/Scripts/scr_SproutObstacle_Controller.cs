using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_SproutObstacle_Controller : MonoBehaviour
{
    [SerializeField] private float health;
    scr_DeathSproutReset_Controller s_DeathSproutReset_Controller;
 
    private Animator anim;
    

    private void Start()
    {
        s_DeathSproutReset_Controller = GetComponent<scr_DeathSproutReset_Controller>();
        anim = GetComponent<Animator>();
    }
    public void takeDamage()//called from bullet
    {
        health -= 1;
        if (health > 0)
        { anim.SetTrigger("Hit"); }
    }


    void Update()
    {
        if (health <= 0)
        {
            s_DeathSproutReset_Controller.Die();
        }
    }
}
