﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class scr_Player_Controller : MonoBehaviour
{
    //components
    private scr_PlayerInput_Component s_PlayerInput_Component;
    private Rigidbody2D rb;
    private Animator anim, targetIconAnim;
    private Collider2D myColl;
    private AudioSource audio;
    //Gameplay 
    [SerializeField] private float maxMovementSpeed, accTime, decTime;
    //Setup
    [SerializeField] private int pLayerID;
    [SerializeField] private string upKey, downKey, leftKey, rightKey, spawnTurretKey1, spawnTurretKey2, spawnTurretKey3, spawnBarrierKey1, spawnBarrierKey2, spawnBarrierKey3;
    [SerializeField] [Range(0, .8f)] private float joystickDeadZone;
    [SerializeField] private Vector2 gameSpaceMinBoundaries, gameSpaceMaxBoundaries;
    private GameObject barrierPrefab, turretPrefab;
    [SerializeField] private GameObject targetIcon;
    [SerializeField] private List<AudioClip> audioClips;
    [SerializeField] private Color grassPLacementUI, waterPlacementUI;
    //logic 
    private Vector2 movementInput;
    private Vector2 movementSpeed;
    private float facingDir;
    [SerializeField] private List<Vector2> sproutCheckOffsets;
    private Vector2 currentSproutCheckOffset;
    private int setAnim;
    private GameObject targetTile, currentLockedTile;
    private bool watering;
    public int waterDroplets;
    private bool stopInput, stunned, refilling, refill, startedWalking;
    private float targetIconAlpha;



    void Start()
    {
        s_PlayerInput_Component = GetComponent<scr_PlayerInput_Component>();
        myColl = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        barrierPrefab = scr_Reference_Manager.barrierPrefab;
        turretPrefab = scr_Reference_Manager.turretPrefab;
        anim = GetComponent<Animator>();
        targetIconAnim = targetIcon.GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }
    public void GetInputBack()
    {
        if (refilling)
        {
            waterDroplets += 1;
            refilling = false;
        }
        if (stunned)
        {
            stunned = false;
        }
        stopInput = false;
    }
    void Update()
    {
        if (pLayerID == 1) { scr_Reference_Manager.playerOneWaterDroplets = waterDroplets; }
        if (pLayerID == 2) { scr_Reference_Manager.playerTwoWaterDroplets = waterDroplets; }
        if (!stopInput)
        {
            HandlePlayerInput();//GetMovement Input
            Movement();//run movement code
            myColl.enabled = true;
        }
        else { rb.velocity = Vector2.zero; }
        FacingDirection();//run facing direction code
        SetAnimations();//Trigger animations
        SetTargetIconState();//set target icon's color and position 
        targetTile = GetTargetTile();//Get closest interactible tile
        ManageAudio(); //Trigger audio clips

    }
    private void HandlePlayerInput()
    {
        movementInput = s_PlayerInput_Component.GetMovementInput(
            (KeyCode)System.Enum.Parse(typeof(KeyCode), leftKey),
            (KeyCode)System.Enum.Parse(typeof(KeyCode), rightKey),
            (KeyCode)System.Enum.Parse(typeof(KeyCode), downKey),
            (KeyCode)System.Enum.Parse(typeof(KeyCode), upKey));
        if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), spawnTurretKey1)) || Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), spawnTurretKey2)) || Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), spawnTurretKey3)))
        { Action(turretPrefab); }
        if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), spawnBarrierKey1)) || Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), spawnBarrierKey2)) || Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), spawnBarrierKey3)))
        { Action(barrierPrefab); }
        if (movementInput.magnitude > joystickDeadZone && rb.velocity.magnitude < .2f)
        {
            startedWalking = true;
        }
    }
    private void SetTargetIconState()
    {
        if (targetTile != null)
        {
            if (currentLockedTile != targetTile)
            {
                targetIconAnim.SetTrigger("Reset");
                targetIconAlpha = 0;
                currentLockedTile = targetTile;
            }
            if (targetTile.layer == LayerMask.NameToLayer("Grass"))
            { targetIcon.transform.position = targetTile.transform.position; targetIcon.GetComponent<SpriteRenderer>().color = new Color(grassPLacementUI.r, grassPLacementUI.g, grassPLacementUI.b, targetIconAlpha); }
            else { targetIcon.transform.position = targetTile.transform.position; targetIcon.GetComponent<SpriteRenderer>().color = new Color(waterPlacementUI.r, waterPlacementUI.g, waterPlacementUI.b, targetIconAlpha); }
            targetIconAlpha = Mathf.Clamp(targetIconAlpha += 2.5f * Time.deltaTime, 0, 1);
        }
        else
        {
            targetIconAlpha = 0;
            targetIcon.GetComponent<SpriteRenderer>().color = Color.clear;
        }
    }
    private void ManageAudio()
    {

        if (audio.clip == audioClips[0] && audio.isPlaying)
        { startedWalking = false; }
        if (audio.clip == audioClips[0] && !audio.isPlaying && rb.velocity.magnitude > .2f)
        {   startedWalking = true;  }

        if (startedWalking)
        {
            PlayAudio(audioClips[0], 1, Mathf.Infinity);
            startedWalking = false;
        }
        if (watering)
        {
            watering = false;
            PlayAudio(audioClips[1], 1, 0);
            PlayAudio(audioClips[2], 1, 0);
        }
        if (stunned)
        {
            PlayAudio(audioClips[3], .4f, 0);
            stunned = false;
        }
        if (refill)
        {
            refill = false;
            PlayAudio(audioClips[4], .4f, 3);
        }
    }
    private void PlayAudio(AudioClip clip, float volume, float loopRuns)
    {
       
        bool forceStop = false;
        if (audio.clip == audioClips[0] && rb.velocity.magnitude < .2f)
        { forceStop = true; }
        //audio.Stop();
        audio.clip = clip;
        audio.PlayOneShot(clip);
        audio.volume = volume;
        if (loopRuns > 0 && !forceStop)
        { StartCoroutine(LoopAudio(clip, clip.length, loopRuns)); }
    }
    IEnumerator LoopAudio(AudioClip clip, float delay, float loopRuns)
    {
        yield return new WaitForSeconds(delay);
        if (audio.clip == clip)
        { PlayAudio(clip, audio.volume, loopRuns - 1); }
    }
    private void Movement()
    {

        //Horizontal Movement
        if (Mathf.Abs(movementInput.x) > joystickDeadZone)//acceleration
        { movementSpeed.x = Mathf.Clamp(movementSpeed.x + (maxMovementSpeed * movementInput.x / accTime * Time.deltaTime), -maxMovementSpeed, maxMovementSpeed); }
        else//deceleration 
        {
            if (Mathf.Abs(rb.velocity.x) > 0.5)
            {
                float direction = -Mathf.Sign(rb.velocity.x);
                movementSpeed.x += maxMovementSpeed * direction / decTime * Time.deltaTime;
            }
            else { movementSpeed.x = 0; }
        }
        //Vertical Movement
        if (Mathf.Abs(movementInput.y) > joystickDeadZone)//acceleration
        { movementSpeed.y = Mathf.Clamp(movementSpeed.y + (maxMovementSpeed * movementInput.y / accTime * Time.deltaTime), -maxMovementSpeed, maxMovementSpeed); }
        else//deceleration 
        {
            if (Mathf.Abs(rb.velocity.y) > 0.5)
            {
                float direction = -Mathf.Sign(rb.velocity.y);
                movementSpeed.y += maxMovementSpeed * direction / decTime * Time.deltaTime;
            }
            else { movementSpeed.y = 0; }
        }
        rb.velocity = new Vector2(movementSpeed.x, movementSpeed.y);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxMovementSpeed);//clamp velocity
        //clamp player to gamespace
        float ClampedX = Mathf.Clamp(rb.position.x, gameSpaceMinBoundaries.x, gameSpaceMaxBoundaries.x);
        float ClampedY = Mathf.Clamp(rb.position.y, gameSpaceMinBoundaries.y, gameSpaceMaxBoundaries.y);
        rb.position = new Vector2(ClampedX, ClampedY);
    }
    private void FacingDirection()
    {//Get a number from 0 to 3 to determine the facing direction ((0)right,(1)left,(2)up,(3)down)
        if (movementInput.magnitude > joystickDeadZone)
        {
            if (Mathf.Abs(movementInput.x) > Mathf.Abs(movementInput.y))
            { facingDir = Mathf.Sign(movementInput.x) > 0 ? 0 : 1; }
            else { facingDir = Mathf.Sign(movementInput.y) > 0 ? 2 : 3; }
            currentSproutCheckOffset = sproutCheckOffsets[(int)facingDir];
        }
    }
    private void SetAnimations()
    {
        anim.SetBool("NotMoving", rb.velocity.magnitude < .2f);//set to idle or not
        anim.SetBool("Watering", watering);//set to watering or not
        anim.SetBool("Refilling", refill);//set to refilling  or not
        anim.SetBool("Hit", stunned);//set to refilling  or not
        //Set direction and movement animations
        if (setAnim != facingDir&&!stopInput)
        {
            switch (facingDir)
            {
                case 0:
                    if (setAnim != facingDir) { anim.SetTrigger("WalkRight"); }
                    setAnim = (int)facingDir; break;
                case 1:
                    if (setAnim != facingDir) { anim.SetTrigger("WalkLeft"); }
                    setAnim = (int)facingDir; break;
                case 2:
                    if (setAnim != facingDir) { anim.SetTrigger("WalkUp"); }
                    setAnim = (int)facingDir; break;
                case 3:
                    if (setAnim != facingDir) { anim.SetTrigger("WalkDown"); }
                    setAnim = (int)facingDir; break;
            }
        }
    }
    private GameObject GetTargetTile()
    {
        Collider2D[] tiles = Physics2D.OverlapBoxAll((Vector2)transform.position - (Vector2)transform.up * .5f + currentSproutCheckOffset * .9f, new Vector2(.1f, .1f), 0, LayerMask.GetMask("Grass") + LayerMask.GetMask("WaterTile"));
        float smallestDist = Mathf.Infinity;
        Collider2D closestColl = null;
        if (tiles.Length > 0 && !stopInput)
        {
            if (tiles.Length > 1)
            {
                foreach (Collider2D coll in tiles)
                {
                    float myDist = Vector2.Distance(transform.position, coll.transform.position);
                    if (myDist < smallestDist)
                    { smallestDist = myDist; closestColl = coll; }
                }
            }
            else { closestColl = tiles[0]; }

            if (closestColl.gameObject.layer == LayerMask.NameToLayer("Grass"))
            {
                if (!closestColl.GetComponent<scr_Grass_Controller>().isActive && closestColl.GetComponent<scr_Grass_Controller>().isGrassy && waterDroplets > 0)
                { return closestColl.gameObject; }
                else { return null; }
            }
            else if (waterDroplets < 3) { return closestColl.gameObject; }
            else { return null; }
        }
        else { return null; }

    }
    private void Action(GameObject spawnable)
    {
        if (targetTile != null)
        {
            //water spawnable
            if (targetTile.GetComponent<scr_Grass_Controller>() != null && waterDroplets > 0)
            {
                waterDroplets -= 1;
                watering = true;
                targetTile.GetComponent<scr_Grass_Controller>().Activate(spawnable);
            }
            if (targetTile.layer == LayerMask.NameToLayer("WaterTile"))
            {
                stopInput = true;
                refilling = true;
                refill = true;
            }
        }
    }
 
    public void Stunned()//called from bullet collision
    {
        stopInput = true;
        stunned = true;
        //scr_Reference_Manager.s_Sound_Manager.PlaySound("stun");
        myColl.enabled = false;
    }
}