using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_DeathSproutReset_Controller : MonoBehaviour
{
    public GameObject originSprout;//assigned by sprout when spawned
    [SerializeField] private GameObject deathParticleEffect;
    private bool isDead;
    public void Die()
    {
        if (!isDead)
        {
            isDead = true;
            GameObject newParticleEffect = Instantiate(deathParticleEffect, transform.position, Quaternion.identity);
            Destroy(newParticleEffect, 2f);
            originSprout.GetComponent<scr_Grass_Controller>().isActive = false;
            Destroy(gameObject, .2f);
        }

    }
}
