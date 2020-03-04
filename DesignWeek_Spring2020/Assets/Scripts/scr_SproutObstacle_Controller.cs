using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_SproutObstacle_Controller : MonoBehaviour
{
    [SerializeField] private float health;
    scr_DeathSproutReset_Controller s_DeathSproutReset_Controller;
    [SerializeField] private List<Sprite> sprites;
    private SpriteRenderer sr;

    private void Start()
    {
        s_DeathSproutReset_Controller = GetComponent<scr_DeathSproutReset_Controller>();
        sr = GetComponent<SpriteRenderer>();
        ChangeSprite();
    }
    public void takeDamage()//called from bullet
    {
        health -= 1;
        ChangeSprite();
    }
    void ChangeSprite()
    {
        if (health > 0)
        { sr.sprite = sprites[(int)health - 1]; }
    }

    void Update()
    {
        if (health <= 0)
        {
            s_DeathSproutReset_Controller.Die();
        }
    }
}
