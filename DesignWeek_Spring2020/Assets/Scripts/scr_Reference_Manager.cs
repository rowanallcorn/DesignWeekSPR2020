using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Reference_Manager : MonoBehaviour
{
    [SerializeField] private GameObject barrierPrefabRef, turretPrefabRef;
    public static GameObject barrierPrefab, turretPrefab;
    void Awake()
    {
        barrierPrefab = barrierPrefabRef;
        turretPrefab = turretPrefabRef;
    }
}
