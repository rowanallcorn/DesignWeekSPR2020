using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Turret_Controller : MonoBehaviour
{
    scr_DeathSproutReset_Controller s_DeathSproutReset_Controller;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float cooldown;
    [SerializeField] private int maxShots;
    [SerializeField] private GameObject shootingPos;

    private void Start()
    {
        s_DeathSproutReset_Controller = GetComponent<scr_DeathSproutReset_Controller>();
        StartCoroutine(Cooldown());
        transform.localScale = transform.position.x > .1f ?new Vector2(-1,1):new Vector2(1,1);
    }
    IEnumerator Cooldown()
    {
        if (maxShots > 0)
        {
            maxShots -= 1;
            yield return new WaitForSeconds(cooldown);
            Shoot();
            StartCoroutine(Cooldown());
        }
        else { s_DeathSproutReset_Controller.Die(); }

    }
    private void Shoot()
    {  GameObject newProjectile = Instantiate(projectilePrefab, shootingPos.transform.position, shootingPos.transform.rotation);  }

   
}
