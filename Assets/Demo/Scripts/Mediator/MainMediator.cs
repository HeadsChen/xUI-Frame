/***
 *    Project:
 *		  ***
 *    Title: 
 *		  ***
 *    Description: 
 *        ***
 *                  
 *    Date: 2017
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;
using System.Collections;
using XUIF;
using System;

public class MainMediator : Mediator {

    public override void OnRegister()
    {
		BindClickEvent ("Player_Info", go => {
			OpenPanel ("HeroInfo");
		});

        BindClickEvent("Email", go =>
        {
            OpenPanel("Email");
        });

        MessageCenter.SendMessage("player_id", "Heads");

        MessageCenter.SendMessage("head", LoadImg("head"));
    }

    private Sprite LoadImg(string imgName)
    {
        return Resources.Load<Sprite>("Sprite/" + imgName);
    }
    
}
