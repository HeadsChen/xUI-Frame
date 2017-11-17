using UnityEngine;
using System.Collections;
using XUIF;

public class SettingView : View {

	public GameObject CNBtn;
	public GameObject ENBtn;

	public override void InitView ()
	{
		BindButton ("Lang_CN", CNBtn);
		BindButton ("Lang_EN", ENBtn);
	}

}
