using UnityEngine;
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

		BindClickEvent ("hero_info_close", go => {
			ClosePanel ();
		});

		BindClickEvent ("Return_Login", go => {
			UIManager.Instance.PullToPanel("Login");
		
		});

		//MessageCenter.SendMessage ("head","Sprite" );

		MessageCenter.SendMessage ("player_id", "Heads");
	}


}
