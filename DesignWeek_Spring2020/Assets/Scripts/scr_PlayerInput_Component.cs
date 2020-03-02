using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_PlayerInput_Component : MonoBehaviour
{
    //Get movement input
    public Vector2 GetMovementInput(KeyCode horNegativeKey, KeyCode horPositiveKey, KeyCode verNegativeKey, KeyCode verPositiveKey)
    {
        float horNeg = Input.GetKey(horNegativeKey) ? -1 : 0;
        float horPos = Input.GetKey(horPositiveKey) ? 1 : 0;
        float horInput = horNeg + horPos;//horizontal input
        float verNeg = Input.GetKey(verNegativeKey) ? -1 : 0;
        float verPos = Input.GetKey(verPositiveKey) ? 1 : 0;
        float verInput = verNeg + verPos;//vertical input
        return new Vector2(horInput,verInput);//final input
    }

}
