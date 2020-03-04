using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Grass_Controller : MonoBehaviour
{
    public bool isActive;
    public bool isGrassy=true;
    private bool isDead;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Activate(GameObject spawnableObj)//called when sprout is watered
    {
        if (!isActive&&isGrassy)
        { StartCoroutine(wateredActions(spawnableObj)); }
    }
    IEnumerator wateredActions(GameObject spawnableObj)
    {
        isGrassy = false;
        isActive = true;
        isDead = true;
        //print(gameObject.name + " was wattered");
        yield return new WaitForSeconds(.2f);//wait a bit
        GameObject newSpawnable = Instantiate(spawnableObj, transform.position, transform.rotation);//instatiate wall
        newSpawnable.GetComponent<scr_DeathSproutReset_Controller>().originSprout = gameObject;
        //Destroy(gameObject);//die
    }
    private void Update()
    {
        if (!isGrassy && !isActive&& isDead)
        {
            isDead = false;
            anim.SetTrigger("GrowBack");
        }
    }
    public void RegenerateGrass()
    {
        isGrassy = true;
    }
}
