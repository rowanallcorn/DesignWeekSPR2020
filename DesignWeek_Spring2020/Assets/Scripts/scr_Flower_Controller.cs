using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Flower_Controller : MonoBehaviour
{
    [SerializeField] private int playerId;
    [SerializeField] private float minY, maxY, speed;
    [SerializeField] private bool movingDown;
    [SerializeField] private float health;
    private Animator anim;
    private Collider2D myColl;
    [SerializeField] private SpriteRenderer sr;
    private bool isDead;

    private void Start()
    {
        anim = GetComponent<Animator>();
        myColl = GetComponent<Collider2D>();
    }
    void Update()
    {
        if (movingDown)
        {
            if (transform.position.y > minY)
            { transform.Translate(-transform.up * speed * Time.deltaTime); }
            else { movingDown = false; }
        }
        else
        {
            if (transform.position.y < maxY)
            { transform.Translate(transform.up * speed * Time.deltaTime); }
            else { movingDown = true; }
        }
        if (health <= 0)
        {
            if (speed > .2f)
            { speed -= 6 * Time.deltaTime; print("hmmm"); }
            else if (speed < -.2f)
            { speed += 6 * Time.deltaTime; print("hmmm"); }
            else { speed = 0; }

            if (!isDead)
            {
                isDead = true;
                anim.SetTrigger("Hit");
            }
        }
    }
    public void TakeDamage()
    {
        health -= 1;
        anim.SetTrigger("Hit");
        speed *= 1.7f;
        StartCoroutine(Flash());
        StartCoroutine(StopFlash(1.4f));
    }
    public void Die()//called from animation event
    {
        scr_GameState_Manager.GameOver(playerId);
        Destroy(gameObject);
    }
    IEnumerator Flash()
    {
        sr.color = Color.clear;
        yield return new WaitForSeconds(.07f);
        sr.color = Color.white;
        yield return new WaitForSeconds(.07f);
        sr.color = Color.clear;
        if (myColl.enabled)
        { sr.color = Color.white; }
        else { StartCoroutine(Flash()); }
    }
    IEnumerator StopFlash(float delay)
    {
        myColl.enabled = false;
        yield return new WaitForSeconds(delay);
        myColl.enabled = true;
    }
}
