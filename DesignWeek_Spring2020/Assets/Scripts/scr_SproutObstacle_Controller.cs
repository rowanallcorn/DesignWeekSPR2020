using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_SproutObstacle_Controller : MonoBehaviour
{
    [SerializeField] private float health;
    public void takeDamage()//called from bullet
    {
        health -= 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
