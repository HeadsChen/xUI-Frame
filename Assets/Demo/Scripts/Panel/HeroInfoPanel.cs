using UnityEngine;
using UnityEngine.UI;
using XUIF;

public class HeroInfoPanel : Panel {

	public Image head;
	public Text playerId;
	public GameObject petInfoBtn;
	public GameObject detailInfoBtn;
	public GameObject backBtn;

	public override void InitPanel ()
	{
		RegisterButton ("pet_info", petInfoBtn);
		RegisterButton ("detail_info", detailInfoBtn);
		RegisterButton ("hero_info_close", backBtn);

		ReceiveMessage ("head", o => {
            //head.sprite = Resources.Load<Sprite>(o.ToString());
		});

		ReceiveMessage ("player_id", o => {
			playerId.text = o.ToString ();
		});
	}
}
