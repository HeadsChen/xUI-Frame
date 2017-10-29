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
            OpenPanel(ViewDefine.HEROINFO);
		});

        BindClickEvent("Email", go =>
        {
            OpenPanel(ViewDefine.EMAILBOX);
        });

        MessageDispatcher.SendMsg("player_id", "Heads");

        MessageDispatcher.SendMsg("head", LoadImg("head"));
    }

    private Sprite LoadImg(string imgName)
    {
        return Resources.Load<Sprite>("Sprite/" + imgName);
    }
    
    private string GetText(string id)
    {
        return Singleton<LanguageMgr>.Instance.GetText(id);
    }
}
