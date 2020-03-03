using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_SproutController : MonoBehaviour
{
    [SerializeField] GameObject sproutObstaclePrefab;
    bool active;
    public void Activate()//called when sprout is watered
    {
        if (!active)
        { StartCoroutine(wateredActions()); }
    }
    IEnumerator wateredActions()
    {
        active = true;
        print(gameObject.name + " was wattered");
        yield return new WaitForSeconds(.2f);//wait a bit
        Instantiate(sproutObstaclePrefab, transform.position, transform.rotation);//instatiate wall
        //Destroy(gameObject);//die
    }

}
