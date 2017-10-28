/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  Pet Information Panel
 *    Description: 
 *        Demo Pet Information
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


public class PetInfoView : View {

	public Text petInfo1;
	public Text petInfo2;
	//public GameObject closeBtn;

	public override void InitView ()
	{
		//RegisterButton ("pet_info_close", closeBtn);

		ReceiveMessage ("pet_info1", o => {
			petInfo1.text = o.ToString ();
		});

		ReceiveMessage ("pet_info2", o => {
			petInfo2.text = o.ToString ();
		});
	}
}
