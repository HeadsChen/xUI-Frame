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

public class TestMediator : Mediator {

    public override void OnRegister()
    {
        BindClickEvent("btn", go =>
        {
            Debug.Log(go.name + "button click");
            MessageCenter.SendMessage("img", Color.red);
            MessageCenter.SendMessage("text", "Hello xUI");
        });

        
    }
    
}
