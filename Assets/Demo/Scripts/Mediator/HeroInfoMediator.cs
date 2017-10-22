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

		//MessageCenter.SendMessage ("head", );

		MessageCenter.SendMessage ("player_id", "Heads");
	}


}
