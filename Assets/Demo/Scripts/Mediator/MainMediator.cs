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
		BindClickEvent ("hero_info", go => {
			OpenPanel ("HeroInfo");
		});

        
    }
    
}
