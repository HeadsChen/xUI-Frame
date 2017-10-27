/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  Main Mediator
 *    Description: 
 *        Demo Main
 *                  
 *    Date: 2017/10/22
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

        MessageDispatcher.SendMsg("player_id", "Heads");

        MessageDispatcher.SendMsg("head", LoadImg("head"));
    }

    private Sprite LoadImg(string imgName)
    {
        return Resources.Load<Sprite>("Sprite/" + imgName);
    }
    
}
