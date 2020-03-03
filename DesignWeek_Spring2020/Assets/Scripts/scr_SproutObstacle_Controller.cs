using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_SproutObstacle_Controller : MonoBehaviour
{
    [SerializeField] private float health;
    scr_DeathSproutReset_Controller s_DeathSproutReset_Controller;

    private void Start()
    { s_DeathSproutReset_Controller = GetComponent<scr_DeathSproutReset_Controller>(); }
    public void takeDamage()//called from bullet
    { health -= 1; }

    void Update()
    {
        if (health <= 0)
        {
            s_DeathSproutReset_Controller.Die();
        }
    }
}
