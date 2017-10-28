/***
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
using UnityEngine.UI;
using XUIF;

public class RootView : View
{
    public Image root;

    public override void InitView()
    {
        ReceiveMessage("Mask", o =>
        {
            root.color = (Color)o;
        });
    }
}


