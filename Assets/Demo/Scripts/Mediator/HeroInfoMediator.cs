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

		MessageCenter.SendMessage ("hero_head", new Color (Random.Range (0, 1f), Random.Range (0, 1f), Random.Range (0, 1f), Random.Range (0, 1f)));

		MessageCenter.SendMessage ("player_id", "Heads");
	}


}
