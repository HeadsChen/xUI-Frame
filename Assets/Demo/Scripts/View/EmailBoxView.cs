/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  Email Panel
 *    Description: 
 *        Demo email box panel
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

public class EmailBoxView : View {
    
    public GameObject noticeLabel;
    public GameObject letterLabel;
    public GameObject awardLabel;
    public GameObject getAll;
    public GameObject AddEmail;

    public Transform noticeBox;
    public Transform letterBox;
    public Transform awardBox;

    public Text bottomNotice;

    public override void InitView()
    {
        BindButton("Notice", noticeLabel);
        BindButton("Letter", letterLabel);
        BindButton("Award", awardLabel);
        BindButton("Get_All", getAll);
        BindButton("Add_Email", AddEmail);

        ReceiverBinder.BindReceiver("Notice", noticeBox);
        ReceiverBinder.BindReceiver("Letter", letterBox);
        ReceiverBinder.BindReceiver("Award", awardBox);
        
        ReceiveMessage("bottom_notice", o =>
        {
            bottomNotice.text = o.ToString();
        });        
    }
}
