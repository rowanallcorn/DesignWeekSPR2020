using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Grass_Controller : MonoBehaviour
{
    public bool isActive;
    public void Activate(GameObject spawnableObj)//called when sprout is watered
    {
        if (!isActive)
        { StartCoroutine(wateredActions(spawnableObj)); }
    }
    IEnumerator wateredActions(GameObject spawnableObj)
    {
        isActive = true;
        //print(gameObject.name + " was wattered");
        yield return new WaitForSeconds(.2f);//wait a bit
        GameObject newSpawnable = Instantiate(spawnableObj, transform.position, transform.rotation);//instatiate wall
        newSpawnable.GetComponent<scr_DeathSproutReset_Controller>().originSprout = gameObject;
        //Destroy(gameObject);//die
    }
}
