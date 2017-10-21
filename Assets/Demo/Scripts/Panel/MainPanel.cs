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
using UnityEngine.UI;
using XUIF;

public class MainPanel : Panel {


	public GameObject btn;


    public override void InitPanel()
    {
        RegisterButton("hero_info", btn);

        
    }
}
