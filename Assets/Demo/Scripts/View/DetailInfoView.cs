/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  Detail Information Panel
 *    Description: 
 *        Demo Detail Information
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

public class DetailInfoView : View {

	public Text detail1;
	public Text detail2;
	public Text detail3;

	public override void InitView ()
	{
		ReceiveMessage ("detail1", o => {
			detail1.ToString ();
		});

		ReceiveMessage ("detail2", o => {
			detail2.ToString ();
		});

		ReceiveMessage ("detail3", o => {
			detail3.ToString ();
		});
	}
}
