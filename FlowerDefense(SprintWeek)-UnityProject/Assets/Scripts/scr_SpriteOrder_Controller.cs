using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_SpriteOrder_Controller : MonoBehaviour
{
    private SpriteRenderer sr;
    private float newSortingOrder;
    [SerializeField] private bool runOnUpdate;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        SetSortingORder();
    }
    void Update()
    {
        if (runOnUpdate)
        { SetSortingORder(); }
    }
    void SetSortingORder()
    {
        newSortingOrder = (-transform.position.y + 100) * 100f - 9000;
        sr.sortingOrder = ((int)newSortingOrder);
    }
}
