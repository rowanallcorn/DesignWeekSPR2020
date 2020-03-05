using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_GameUI : MonoBehaviour
{
    [SerializeField] private Image p1Drop1, p1Drop2, p1Drop3, p2Drop1, p2Drop2, p2Drop3;
    [SerializeField] private Color emptyColor = Color.grey;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {     //Player 1 water indicator
        if (scr_Reference_Manager.playerOneWaterDroplets == 3)
        {
            p1Drop1.color = Color.white;
            p1Drop2.color = Color.white;
            p1Drop3.color = Color.white;
        }

        if (scr_Reference_Manager.playerOneWaterDroplets == 2)
        {
            p1Drop1.color = Color.white;
            p1Drop2.color = Color.white;
            p1Drop3.color = emptyColor;
        }

        if (scr_Reference_Manager.playerOneWaterDroplets == 1)
        {
            p1Drop1.color = Color.white;
            p1Drop2.color = emptyColor;
            p1Drop3.color = emptyColor;
        }

        if (scr_Reference_Manager.playerOneWaterDroplets == 0)
        {
            p1Drop1.color = emptyColor;
            p1Drop2.color = emptyColor;
            p1Drop3.color = emptyColor;
        }

        //Player 2 water indicator
        if (scr_Reference_Manager.playerTwoWaterDroplets == 3)
        {
            p2Drop1.color = Color.white;
            p2Drop2.color = Color.white;
            p2Drop3.color = Color.white;
        }

        if (scr_Reference_Manager.playerTwoWaterDroplets == 2)
        {
            p2Drop1.color = Color.white;
            p2Drop2.color = Color.white;
            p2Drop3.color = emptyColor;
        }

        if (scr_Reference_Manager.playerTwoWaterDroplets == 1)
        {
            p2Drop1.color = Color.white;
            p2Drop2.color = emptyColor;
            p2Drop3.color = emptyColor;
        }

        if (scr_Reference_Manager.playerTwoWaterDroplets == 0)
        {
            p2Drop1.color = emptyColor;
            p2Drop2.color = emptyColor;
            p2Drop3.color = emptyColor;
        }
    }
}
