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

public class MainView : View {

    public Image head;
    public GameObject email;
    public GameObject guide;
	public GameObject menu;
    public GameObject shop;
    public GameObject friend;
    public GameObject setting;

    public Text playerId;

    public override void InitView()
    {
        BindButton("Player_Info", head.gameObject);
        BindButton("Email", email);
        BindButton("Guide", guide);
        BindButton("Menu", menu);
        BindButton("Shop",shop);
        BindButton("Friend", friend);
        BindButton("Setting", setting);

        ReceiveMessage("player_id", o => playerId.text = o.ToString());
        
        //更换头像
        ReceiveMessage("head", o => {
            head.sprite = (Sprite)o;
        });        
    }
}
