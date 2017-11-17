using UnityEngine;
using System.Collections;
using XUIF;
using UnityEngine.SceneManagement;

public class SettingMediator : Mediator {

	public override void OnRegister ()
	{
		BindClickEvent ("Lang_CN", go => {
			ChangeLang("CHINESE");
		});

		BindClickEvent ("Lang_EN", go => {
			ChangeLang("ENGLISH");
		});
	}

	void ChangeLang(string lang){
		SettingMgr.Instance.SetSetting ("Language", PathDefine.LANGCONFIG + lang);
		SettingMgr.Instance.Config2Json ();
		UIManager.Instance.ClearTreeStack();
		SceneManager.LoadScene (0);
	}
}
