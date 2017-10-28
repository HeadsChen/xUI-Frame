/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  Hero Information Panel
 *    Description: 
 *        Demo Hero Information
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

public class HeroInfoView : View {

	public Image head;
	public Text playerId;
	public GameObject petInfoBtn;
	public GameObject detailInfoBtn;
	public GameObject returnLogin;

	public override void InitView ()
	{
		BindButton ("pet_info", petInfoBtn);
		BindButton ("detail_info", detailInfoBtn);
		BindButton ("Return_Login", returnLogin);

		ReceiveMessage ("head", o => {
            //head.sprite = Resources.Load<Sprite>(o.ToString());
		});

		ReceiveMessage ("player_id", o => {
			playerId.text = o.ToString ();
		});
	}
}
