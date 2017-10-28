
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

public interface IGetEmail{
	void GetEmail();
}

public class EmaiView : MonoBehaviour,IGetEmail {

    public Image senderHead;
    public Image AttachImg;
    public Text senderId;
    public Text content;
    public Text state;
    public Text timer;
    public GameObject getBtn;

    private void Start()
    {
		ButtonTriggerListener listen = ButtonTriggerListener.GetListener (getBtn);
		listen.onClick = go => {
			GetEmail();
		};

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

	public void GetEmail(){
		Debug.LogFormat ("Get a email:{0}", gameObject.transform.GetChild (1).GetComponent<Text> ().text);
		Destroy (gameObject);
	}

}
