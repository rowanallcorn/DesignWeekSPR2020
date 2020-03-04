using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Projectile_Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
  
    private void Update()
    { 
        CheckForWall();
        rb.velocity = transform.up * speed * Time.deltaTime;
    }

    void CheckForWall()
    {
        RaycastHit2D obstacleCheck = Physics2D.Raycast(transform.position, transform.up, .2f, LayerMask.GetMask("SproutObstacle") + LayerMask.GetMask("Flower"));
        bool hit = obstacleCheck.collider != null ? true : false;
        if (hit)
        {
            if (obstacleCheck.collider.gameObject.layer == LayerMask.NameToLayer("SproutObstacle"))
            {
                obstacleCheck.collider.GetComponent<scr_SproutObstacle_Controller>().takeDamage();
            }
            if (obstacleCheck.collider.gameObject.layer == LayerMask.NameToLayer("Flower"))
            {
                obstacleCheck.collider.GetComponent<scr_Flower_Controller>().TakeDamage();
            }
            Destroy(gameObject);
        }
    }
}
