/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  Email Mediator
 *    Description: 
 *        Demo email box mediator
 *                  
 *    Date: 2017/10/22
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */

using System.Collections.Generic;
using UnityEngine;
using XUIF;

public class EmailBoxMediator : Mediator
{

	public override void OnRegister ()
	{
		BindClickEvent ("Notice", go => {
			SelectLabel (go);
		});

		BindClickEvent ("Letter", go => {
			SelectLabel (go);
		});

		BindClickEvent ("Award", go => {
			SelectLabel (go);
		});

		BindClickEvent ("Get_All", go => {

			int emailCount = _currentBox.childCount;
			for (int i = 0; i < emailCount; i++) {
				IGetEmail getEmail = _currentBox.GetChild(i).GetComponent<IGetEmail>();
				getEmail.GetEmail();
			}

			MessageDispatcher.SendMsg ("bottom_notice", "All Email have been read.");
		});

		BindClickEvent ("Add_Email", go => {
			MessageDispatcher.SendMsg (_currentBox.name);
		});
        
		InitEmail ();
	}

	public override void Display ()
	{
		base.Display ();

		OrderLabel ();
	}

	private void SelectLabel (GameObject go)
	{
		go.transform.SetAsLastSibling ();
		_currentBox = go.transform.GetChild (go.transform.childCount - 1).GetChild (0);
	}

	List<Transform> _emailBoxList;
	Transform _currentBox;

	/// <summary>
	/// 初始化信箱
	/// </summary>
	private void InitEmail ()
	{
		_emailBoxList = new List<Transform> ();
		BindEmailReceiver ("Notice");
		BindEmailReceiver ("Letter");
		BindEmailReceiver ("Award");
		OrderLabel ();
	}

	private void BindEmailReceiver (string eventName)
	{
		Transform box = ReceiverBinder.BindReceiveEvent (eventName, tran => {
			Debug.LogFormat ("{0} box receive a Email.", tran.name);
			GameObject email = Instantiate (ResLoader.Instance.LoadResFromResFile<GameObject> ("Email", true));
			email.transform.SetParent (tran, false);

			int emailId = GameDefine.emailId++;
			email.name = "emailId_" + emailId;

			//应放在服务端发送数据到客户端
			EmailStruct emailData = new EmailStruct ();
			emailData.emailId = emailId;
			emailData.senderHead = ResLoader.Instance.LoadResFromResFile<Sprite> ("Sprite/head1", true);
			emailData.attachImg = ResLoader.Instance.LoadResFromResFile<Sprite> ("Sprite/item" + Random.Range (0, 4), true);
			emailData.senderId = "System" + Random.Range (1, 10);
			emailData.content = "系统奖励：" + Random.Range (10, 20);
			emailData.state = "剩余时间";
			emailData.Timer = Random.Range (20, 30).ToString ();

			MessageDispatcher.InitKeyValue (email.name, emailData);
		});
		if (box != null) {
			_emailBoxList.Add (box);
		}
	}

	/// <summary>
	/// 标签排序 显示第一个有邮件的标签
	///没有则显示第一个
	/// </summary>
	private void OrderLabel ()
	{
		for (int i = 0; i < _emailBoxList.Count; i++) {
			if (_emailBoxList [i].childCount > 0) {
				_currentBox = _emailBoxList [i];
				_currentBox.parent.parent.SetAsLastSibling ();
				return;
			}
		}
		_currentBox = _emailBoxList [0];
		_currentBox.parent.parent.SetAsLastSibling ();
	}
}
