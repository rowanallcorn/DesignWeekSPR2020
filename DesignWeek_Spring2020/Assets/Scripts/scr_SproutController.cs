using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_SproutController : MonoBehaviour
{
    [SerializeField] GameObject sproutSpawnablePrefab;
    public bool isActive;
    public void Activate()//called when sprout is watered
    {
        if (!isActive)
        { StartCoroutine(wateredActions()); }
    }
    IEnumerator wateredActions()
    {
        isActive = true;
        //print(gameObject.name + " was wattered");
        yield return new WaitForSeconds(.2f);//wait a bit
        GameObject newSpawnable = Instantiate(sproutSpawnablePrefab, transform.position, transform.rotation);//instatiate wall
        newSpawnable.GetComponent<scr_DeathSproutReset_Controller>().originSprout = gameObject;
        //Destroy(gameObject);//die
    }

}
