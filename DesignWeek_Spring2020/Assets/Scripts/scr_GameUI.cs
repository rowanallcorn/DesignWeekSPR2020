using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_GameUI : MonoBehaviour
{
    [SerializeField] private Image p1Drop1,p1Drop2,p1Drop3, p2Drop1, p2Drop2, p2Drop3;
    [SerializeField] private Color emptyColor = Color.grey; 
 
    // Start is called before the first frame update
    void Start()
    {
        //Player 1 water indicator
        if (scr_Reference_Manager.playerOneWaterDroplets == 3)
        { 
        
        }

        if (scr_Reference_Manager.playerOneWaterDroplets == 2)
        {

        }

        if (scr_Reference_Manager.playerOneWaterDroplets == 1)
        {

        }

        if (scr_Reference_Manager.playerOneWaterDroplets == 0)
        {

        }

        //Player 2 water indicator
        if (scr_Reference_Manager.playerTwoWaterDroplets == 3)
        {

        }

        if (scr_Reference_Manager.playerTwoWaterDroplets == 2)
        {

        }

        if (scr_Reference_Manager.playerTwoWaterDroplets == 1)
        {

        }

        if (scr_Reference_Manager.playerTwoWaterDroplets == 0)
        {
             
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
