using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class scr_Player_Controller : MonoBehaviour
{
    //components
    private scr_PlayerInput_Component s_PlayerInput_Component;
    private Rigidbody2D rb;
    private Animator anim;
    //Gameplay 
    [SerializeField] private float maxMovementSpeed, accTime, decTime;
    //Setup
    [SerializeField] private string upKey, downKey, leftKey, rightKey, spawnTurretKey, spawnBarrierKey;
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
    private GameObject targetTile;
    private bool watering;
    public int waterDroplets;
    private bool stopInput;


    void Start()
    {
        s_PlayerInput_Component = GetComponent<scr_PlayerInput_Component>();
        rb = GetComponent<Rigidbody2D>();
        barrierPrefab = scr_Reference_Manager.barrierPrefab;
        turretPrefab = scr_Reference_Manager.turretPrefab;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        HandlePlayerInput();//GetMovement Input
        Movement();//run movement code
        FacingDirection();//run facing direction code
        SetAnimations();//Trigger animations
        targetTile = GetTargetTile();//Get closest interactible tile
        SetTargetIconState();//set target icon's color and position 
        ManageAudio(); //Trigger audio clips
    }
    private void HandlePlayerInput()
    {
        if (!stopInput)
        {
            movementInput = s_PlayerInput_Component.GetMovementInput(
                (KeyCode)System.Enum.Parse(typeof(KeyCode), leftKey),
                (KeyCode)System.Enum.Parse(typeof(KeyCode), rightKey),
                (KeyCode)System.Enum.Parse(typeof(KeyCode), downKey),
                (KeyCode)System.Enum.Parse(typeof(KeyCode), upKey));
            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), spawnTurretKey)))
            { Action(turretPrefab); }
            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), spawnBarrierKey)))
            { Action(barrierPrefab); }
        }
    }
    private void SetTargetIconState()
    {
        if (targetTile != null)
        {
            if (targetTile.layer == LayerMask.NameToLayer("Grass"))
            { targetIcon.transform.position = targetTile.transform.position; targetIcon.GetComponent<SpriteRenderer>().color = grassPLacementUI; }
            else { targetIcon.transform.position = targetTile.transform.position; targetIcon.GetComponent<SpriteRenderer>().color = waterPlacementUI; }
        }
        else { targetIcon.GetComponent<SpriteRenderer>().color = Color.clear; }
    }
    private void ManageAudio()
    {
        if (rb.velocity.magnitude > .2f)
        { scr_Sound_Manager.PlayAudioClip(audioClips[0], 0, true, 1); }
        if (watering)
        {
            watering = false;
            scr_Sound_Manager.PlayAudioClip(audioClips[1], 0, false, .03f);
            scr_Sound_Manager.PlayAudioClip(audioClips[2], 0, false, .5f);
        }
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
    {//Get a number from 0 to 3 to determine the facing direction ((1)right,(2)left,(3)up,(4)down)
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
        anim.SetBool("NotMoving", rb.velocity.magnitude < .2f);
        if (setAnim != facingDir)
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
        Collider2D[] tiles = Physics2D.OverlapBoxAll((Vector2)transform.position + currentSproutCheckOffset * .3f, new Vector2(.1f, .1f), 0, LayerMask.GetMask("Grass") + LayerMask.GetMask("WaterTile"));
        float smallestDist = Mathf.Infinity;
        Collider2D closestColl = null;
        if (tiles.Length > 0)
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
                if (!closestColl.GetComponent<scr_Grass_Controller>().isActive && closestColl.GetComponent<scr_Grass_Controller>().isGrassy)
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
            }
        }
    }
    public void StopGettingWater()
    {
        stopInput = false;
    }
}