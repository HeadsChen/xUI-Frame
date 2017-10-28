/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  Email Mediator
 *    Description: 
 *        Demo Email Mediator
 *                  
 *    Date: 2017/10/28
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;
using System.Collections;
using XUIF;
using UnityEngine.UI;

public class EmailMediator : Mediator {

    public override void OnRegister()
    {
        BindClickEvent("Gat_Email", go =>
        {
            Debug.LogFormat("Get a email:{0}", go.transform.GetChild(1).GetComponent<Text>().text);
            Destroy(go.transform.parent);
        });
    }

}
