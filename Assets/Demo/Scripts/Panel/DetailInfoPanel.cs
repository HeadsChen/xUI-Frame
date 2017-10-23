using UnityEngine;
using UnityEngine.UI;
using XUIF;

public class DetailInfoPanel : Panel {

	public Text detail1;
	public Text detail2;
	public Text detail3;
	public GameObject closeBtn;

	public override void InitPanel ()
	{
		RegisterButton ("detail_info_close", closeBtn);

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
