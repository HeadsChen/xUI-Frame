﻿/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  Root Mediator
 *    Description: 
 *        Demo root
 *                  
 *    Date: 2017/10/22
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;
using XUIF;

public class RootMediator : Mediator {

    public override void OnRegister()
    {
        MessageDispatcher.SendMsg("Mask", new Color(0, 0, 0, 0));
        gameObject.SetActive(false);
    }
}
