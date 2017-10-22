﻿/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  Email Panel
 *    Description: 
 *        Demo email panel
 *                  
 *    Date: 2017/10/22
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;
using UnityEngine.UI;
using XUIF;

public class EmailPanel : Panel {
    
    public GameObject noticeLabel;
    public GameObject letterLabel;
    public GameObject awardLabel;
    public GameObject getAll;
    public GameObject close;

    public Text bottomNotice;

    public override void InitPanel()
    {
        RegisterButton("Notice", noticeLabel);
        RegisterButton("Letter", letterLabel);
        RegisterButton("Award", awardLabel);
        RegisterButton("Get_All", getAll);
        RegisterButton("Close_Email", close);

        AddMessage("bottom_notice", o =>
        {
            bottomNotice.text = o.ToString();
        });
    }
}
