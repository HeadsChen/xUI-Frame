/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  Login Panel
 *    Description: 
 *        Demo Login
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

public class LoginView : View {

    public Text loginTitle;
    public InputField acountInput;
    public GameObject loginBtn;

    public override void InitView()
    {
        BindButton("login", loginBtn);

        ReceiveMessage("login_title", o =>
        {
            loginTitle.text = o.ToString();
        });

        acountInput.onEndEdit.AddListener(str => {
            Debug.Log("input " + str);
        });
    }
    
}
