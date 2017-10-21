using UnityEngine;
using XUIF;

public class DetailInfoMediator : Mediator {

	public override void OnRegister ()
	{
		BindClickEvent ("detail_info_close", go => {
			UIManager.Instance.CloseSubPanel ("DetailInfo");
		});
	}
}
