using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_DeathSproutReset_Controller : MonoBehaviour
{
    public GameObject originSprout;//assigned by sprout when spawned
    public void Die()
    {
        originSprout.GetComponent<scr_SproutController>().isActive = false;
        Destroy(gameObject, .2f);
    }
}
