using UnityEngine;
using XUIF;

public class PetInfoMediator : Mediator {

	public override void OnRegister ()
	{
		BindClickEvent ("pet_info_close", go => {
			UIManager.Instance.CloseSubPanel ("PetInfo");
		});
	}
}
