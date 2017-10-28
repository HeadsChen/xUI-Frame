/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  Email View
 *    Description: 
 *        Demo email panel
 *                  
 *    Date: 2017/10/28
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;
using UnityEngine.UI;
using XUIF;

public class EmaiView : MonoBehaviour {

    public Image senderHead;
    public Image AttachImg;
    public Text senderId;
    public Text content;
    public Text state;
    public Text timer;
    public GameObject getBtn;

    private void Awake()
    {
        ButtonBinder.BindButton("Get_Email", getBtn);
        ButtonBinder.BindClickEvent("Gat_Email", go =>
        {
            Debug.LogFormat("Get a email:{0}", go.transform.GetChild(1).GetComponent<Text>().text);
            Destroy(go.transform.parent);
        });

        MessageDispatcher.AddListner(transform.name, o =>
        {
            EmailStruct email = (EmailStruct)o;
            senderHead.sprite = email.senderHead;
            AttachImg.sprite = email.attachImg;
            senderId.text = email.senderId;
            content.text = email.content;
            state.text = email.state;
            timer.text = email.Timer;
        });
        
    }
}
