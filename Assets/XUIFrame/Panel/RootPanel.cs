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
using UnityEngine.UI;
using XUIF;

public class RootPanel : Panel
{
    public Image root;

    public override void InitPanel()
    {
        AddMessage("Mask", o =>
        {
            root.color = (Color)o;
        });
    }
}


