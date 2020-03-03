using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Turret_Controller : MonoBehaviour
{
    scr_DeathSproutReset_Controller s_DeathSproutReset_Controller;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float cooldown;
    [SerializeField] private int maxShots;

    private void Start()
    {
        s_DeathSproutReset_Controller = GetComponent<scr_DeathSproutReset_Controller>();
        StartCoroutine(Cooldown());
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
    {  GameObject newProjectile = Instantiate(projectilePrefab, transform.position + transform.up * .6f, transform.rotation); }
   
}
