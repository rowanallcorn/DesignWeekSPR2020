using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Player_Controller : MonoBehaviour
{
    private scr_PlayerInput_Component s_PlayerInput_Component;
    [SerializeField] private Vector2 movementInput;
    [SerializeField] private string upKey, downKey, leftKey, rightKey;
    void Start()
    {
        s_PlayerInput_Component = GetComponent<scr_PlayerInput_Component>();
    }

    void Update()
    {
        //GetMovement Input
        movementInput = s_PlayerInput_Component.GetMovementInput(
            (KeyCode)System.Enum.Parse(typeof(KeyCode), leftKey),
            (KeyCode)System.Enum.Parse(typeof(KeyCode), rightKey),
            (KeyCode)System.Enum.Parse(typeof(KeyCode), downKey),
            (KeyCode)System.Enum.Parse(typeof(KeyCode), upKey));
    }
}
