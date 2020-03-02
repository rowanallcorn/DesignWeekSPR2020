using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Player_Controller : MonoBehaviour
{
    //components
    private scr_PlayerInput_Component s_PlayerInput_Component;
    private Rigidbody2D rb;
    //Gameplay 
    [SerializeField] private float maxMovementSpeed, accTime, decTime;
    //Setup
    [SerializeField] private string upKey, downKey, leftKey, rightKey;
    //logic 
    private Vector2 movementInput;
    private Vector2 movementSpeed;
   
    
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
    }
    private void Movement()
    {
        if (movementInput.magnitude > .3f)
        {//acceleration
            movementSpeed.x = Mathf.Clamp(movementSpeed.x + (maxMovementSpeed * movementInput.x / accTime * Time.deltaTime), -maxMovementSpeed, maxMovementSpeed);//horizontal
            movementSpeed.y = Mathf.Clamp(movementSpeed.y + (maxMovementSpeed * movementInput.y / accTime * Time.deltaTime), -maxMovementSpeed, maxMovementSpeed);//vertical
            rb.velocity = new Vector2(movementSpeed.x, movementSpeed.y);
        }
        else
        {//decceleration 
            if (Mathf.Abs(rb.velocity.x) > 0.5)
            {//horizontal
                float direction = -Mathf.Sign(rb.velocity.x);
                movementSpeed.x += maxMovementSpeed * direction / decTime * Time.deltaTime;
            }
            else { movementSpeed.x = 0; }
            if (Mathf.Abs(rb.velocity.y) > 0.5)
            {//vertical
                float direction = -Mathf.Sign(rb.velocity.y);
                movementSpeed.y += maxMovementSpeed * direction / decTime * Time.deltaTime;
            }
            else { movementSpeed.y = 0; }
            rb.velocity = new Vector2(movementSpeed.x, movementSpeed.y);
        }
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxMovementSpeed);//clamp velocity
    }
}
