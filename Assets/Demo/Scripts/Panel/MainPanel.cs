/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  Main Panel
 *    Description: 
 *        Demo
 *                  
 *    Date: 2017/10/22
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using XUIF;
using System.Collections.Generic;

public class MainPanel : Panel {

    public Image head;
    public Text gameId;
    public GameObject email;
    public GameObject guide;
	public GameObject menu;
    public GameObject shop;
    public GameObject friend;
    public GameObject setting;
    

    public override void InitPanel()
    {
        RegisterButton("Player_Info", head.gameObject);
        RegisterButton("Email", email);
        RegisterButton("Guide", guide);
        RegisterButton("Menu", menu);
        RegisterButton("Shop",shop);
        RegisterButton("Friend", friend);
        RegisterButton("Setting", setting);

        ReceiveMessage("player_id", o => gameId.text = o.ToString());

        //TODO
        //更换头像
        ReceiveMessage("head", o => {
            head.sprite = (Sprite)o;
        });
    }
}
