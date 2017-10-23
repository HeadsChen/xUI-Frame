using UnityEngine;
using UnityEngine.UI;
using XUIF;


public class PetInfoPanel : Panel {

	public Text petInfo1;
	public Text petInfo2;
	public GameObject closeBtn;

	public override void InitPanel ()
	{
		RegisterButton ("pet_info_close", closeBtn);

		ReceiveMessage ("pet_info1", o => {
			petInfo1.text = o.ToString ();
		});

		ReceiveMessage ("pet_info2", o => {
			petInfo2.text = o.ToString ();
		});
	}
}
