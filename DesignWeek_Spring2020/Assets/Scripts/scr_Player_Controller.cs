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
    [SerializeField] [Range(0, .8f)] private float joystickDeadZone;
    //logic 
    private Vector2 movementInput;
    private Vector2 movementSpeed;
    private float facingDir;


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
        FacingDirection();//run facing dirrection code
    }

    private void Movement()
    {
        //Horizontal Movement
        if (Mathf.Abs(movementInput.x) > joystickDeadZone)//acceleration
        {  movementSpeed.x = Mathf.Clamp(movementSpeed.x + (maxMovementSpeed * movementInput.x / accTime * Time.deltaTime), -maxMovementSpeed, maxMovementSpeed); }
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
    {//Get a number from 1 to 4 to determine the facing direction ((1)right,(2)left,(3)up,(4)down)
        if (movementInput.magnitude > joystickDeadZone)
        {
            if (Mathf.Abs(movementInput.x) > Mathf.Abs(movementInput.y))
            { facingDir = Mathf.Sign(movementInput.x) > 0 ? 1 : 2; }
            else { facingDir = Mathf.Sign(movementInput.y) > 0 ? 3 : 4; }
        }
        //TODO 
        //Implement sprite change
    }
}
