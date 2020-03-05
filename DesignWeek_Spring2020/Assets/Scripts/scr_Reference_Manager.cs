using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Reference_Manager : MonoBehaviour
{
    [SerializeField] private GameObject barrierPrefabRef, turretPrefabRef, projectileHolderRef, turretHolderRef, barrierHolderRef, audioSourcesHolderRef, effectsHolderRef;
    public static GameObject barrierPrefab, turretPrefab, projectileHolder, turretHolder, barrierHolder, effectsHolder;
    public static AudioSource[] audioSources;
    public static scr_Sound_Manager s_Sound_Manager;
    public static int playerOneWaterDroplets,playerTwoWaterDroplets;
    void Awake()
    {
        barrierPrefab = barrierPrefabRef;
        turretPrefab = turretPrefabRef;
        projectileHolder = projectileHolderRef;
        turretHolder = turretHolderRef;
        barrierHolder = barrierHolderRef;
        effectsHolder = effectsHolderRef;
        audioSources = audioSourcesHolderRef.GetComponentsInChildren<AudioSource>();
        s_Sound_Manager = GetComponent<scr_Sound_Manager>();

    }

}
