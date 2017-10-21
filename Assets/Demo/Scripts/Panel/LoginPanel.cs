/***
 *    Project:
 *		  ***
 *    Title: 
 *		  ***
 *    Description: 
 *        ***
 *                  
 *    Date: 2017/10/21
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;
using UnityEngine.UI;
using XUIF;

public class LoginPanel : Panel {

    public Text loginTitle;
    public InputField acountInput;
    public GameObject loginBtn;

    public override void InitPanel()
    {
        RegisterButton("login", loginBtn);

        AddMessage("login_title", o =>
        {
            loginTitle.text = o.ToString();
        });

        acountInput.onEndEdit.AddListener(str => {
            Debug.Log("input " + str);
        });
    }
    
}
