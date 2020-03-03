﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class scr_Player_Controller : MonoBehaviour
{
    //components
    private scr_PlayerInput_Component s_PlayerInput_Component;
    private Rigidbody2D rb;
    //Gameplay 
    [SerializeField] private float maxMovementSpeed, accTime, decTime;
    //Setup
    [SerializeField] private string upKey, downKey, leftKey, rightKey, spawnTurretKey, spawnBarrierKey;
    [SerializeField] [Range(0, .8f)] private float joystickDeadZone;
    [SerializeField] private Vector2 gameSpaceMinBoundaries, gameSpaceMaxBoundaries;
    private GameObject barrierPrefab, turretPrefab;
    //logic 
    private Vector2 movementInput;
    private Vector2 movementSpeed;
    private float facingDir;
    [SerializeField] private List<Vector2> sproutCheckOffsets;
    private Vector2 currentSproutCheckOffset;


    void Start()
    {
        s_PlayerInput_Component = GetComponent<scr_PlayerInput_Component>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //GetMovement Input
        movementInput = s_PlayerInput_Component.GetMovementInput(
            (KeyCode)System.Enum.Parse(typeof(KeyCode), leftKey),
            (KeyCode)System.Enum.Parse(typeof(KeyCode), rightKey),
            (KeyCode)System.Enum.Parse(typeof(KeyCode), downKey),
            (KeyCode)System.Enum.Parse(typeof(KeyCode), upKey));
        Movement();//run movement code
        //Constrain pllayer to gamespace
        float ClampedX = Mathf.Clamp(rb.position.x, gameSpaceMinBoundaries.x, gameSpaceMaxBoundaries.x);
        float ClampedY = Mathf.Clamp(rb.position.y, gameSpaceMinBoundaries.y, gameSpaceMaxBoundaries.y);
        rb.position = new Vector2(ClampedX, ClampedY);

        FacingDirection();//run facing dirrection code
        //Spawning turrets and barriers
        if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), spawnTurretKey)))
        { Spawn(turretPrefab); }
        if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), spawnBarrierKey)))
        { Spawn(barrierPrefab); }
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
    }
    private void FacingDirection()
    {//Get a number from 0 to 3 to determine the facing direction ((1)right,(2)left,(3)up,(4)down)
        if (movementInput.magnitude > joystickDeadZone)
        {
            if (Mathf.Abs(movementInput.x) > Mathf.Abs(movementInput.y))
            { facingDir = Mathf.Sign(movementInput.x) > 0 ? 0 : 1; }
            else { facingDir = Mathf.Sign(movementInput.y) > 0 ? 2 : 3; }
            currentSproutCheckOffset = sproutCheckOffsets[(int)facingDir];
        }
        //TODO 
        //Implement sprite change
    }
    private void Spawn(GameObject spawnable)
    {
        //check if there is a sprout
        Collider2D[] sprouts = Physics2D.OverlapBoxAll((Vector2)transform.position + currentSproutCheckOffset * 1f, new Vector2(.6f, .6f), 0, LayerMask.GetMask("Sprout"));
        if (sprouts.Length > 0)
        {
            if (sprouts[0].GetComponent<scr_Grass_Controller>() != null)
            { sprouts[0].GetComponent<scr_Grass_Controller>().Activate(spawnable); }
        }
    }
}
