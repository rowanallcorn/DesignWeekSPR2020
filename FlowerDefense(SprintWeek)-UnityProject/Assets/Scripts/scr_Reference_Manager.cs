﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Reference_Manager : MonoBehaviour
{
    [SerializeField] private GameObject barrierPrefabRef, turretPrefabRef, projectileHolderRef, turretHolderRef, barrierHolderRef, effectsHolderRef;
    public static GameObject barrierPrefab, turretPrefab, projectileHolder, turretHolder, barrierHolder, effectsHolder;
    public static scr_Sound_Manager s_Sound_Manager;
    [SerializeField]private scr_TransitionScenes s_TransitionScenesRef;
    public static scr_TransitionScenes s_TransitionScenes;
    public static int playerOneWaterDroplets,playerTwoWaterDroplets;
    void Awake()
    {
        s_TransitionScenes = s_TransitionScenesRef;
        barrierPrefab = barrierPrefabRef;
        turretPrefab = turretPrefabRef;
        projectileHolder = projectileHolderRef;
        turretHolder = turretHolderRef;
        barrierHolder = barrierHolderRef;
        effectsHolder = effectsHolderRef;

    }

}
