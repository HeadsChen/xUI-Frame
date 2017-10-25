/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  Hero Information Mediator
 *    Description: 
 *        Demo Hero Information
 *                  
 *    Date: 2017/10/22
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */


using XUIF;


public class HeroInfoMediator : Mediator {

	public override void OnRegister ()
	{
		BindClickEvent ("detail_info", go => {
			UIManager.Instance.OpenSubPanel("DetailInfo");
		});

		BindClickEvent ("pet_info", go => {
			UIManager.Instance.OpenSubPanel("PetInfo");
		});

		BindClickEvent ("Return_Login", go => {
			UIManager.Instance.Return2Panel("Login");
		
		});

		//MessageCenter.SendMessage ("head","Sprite" );

		MessageCenter.SendMsg ("player_id", "Heads");
	}


}
