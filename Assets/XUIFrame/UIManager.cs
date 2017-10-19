/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  UI Manager
 *    Description: 
 *        The core of this frame.
 *        Manage all the View and ViewCtrl.
 *         
 *                  
 *    Date: 2017/10/19
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace XUIF
{
	public class UIManager : UnitySingleton<UIManager>
	{
		//#region 单例
		//private static UIManager _instance;
		//public static UIManager Instance
		//{
		//    get
		//    {
		//        if (_instance == null)
		//        {
		//            _instance = new UIManager();
		//        }
		//        return _instance;
		//    }
		//}
		//#endregion

		public Transform _UIRoot;

		Dictionary<string, string> _prefDic;
		Dictionary<string, Mediator> _mediatorDic;

		Stack<Mediator> _currentViewStack;



		private void Start ()
		{
			//GameObject panel = Instantiate(LoadPanelPref());
			//panel.transform.SetParent(_UIRoot);
			//panel.transform.localPosition = Vector3.zero;
			//panel.transform.localEulerAngles = Vector3.zero;
			//panel.GetComponent<Panel>().InitPanel();
			//Mediator m = panel.AddComponent(ContextBinder.GetMediator(LoadConfig())) as Mediator;
			//m.OnRegister();
			_mediatorDic = new Dictionary<string, Mediator> ();
			LoadConfig ();
		}

		/// <summary>
		/// Opens the new panel.
		/// </summary>
		/// <returns><c>true</c>, if new panel was opened, <c>false</c> otherwise.</returns>
		/// <param name="panel">Panel Name</param>
		public bool OpenNewPanel(string panel){
			Mediator m = GetMediator (panel);
			if (m != null) {
				m.Display ();
				PushPanel2Cache (m);
			}
			return m != null;
		}

		public void ClosePanel(){
			
		}

		#region 面板调度器入栈出栈

		/// <summary>
		/// Pushs the panel to cache.
		/// </summary>
		/// <param name="m">入栈面板的调度器</param>
		private void PushPanel2Cache (Mediator m)
		{
			if (_currentViewStack != null) {
				_currentViewStack.Push (m);
			}
		}

		/// <summary>
		/// Pops the panel from cache.
		/// </summary>
		/// <returns>The panel from cache.</returns>
		private Mediator PopPanelFromCache ()
		{
			return _currentViewStack.IsNotNullOrEmpty () ? _currentViewStack.Pop () : null;
		}

		/// <summary>
		/// Peeks the panel from cache.
		/// </summary>
		/// <returns>The panel from cache.</returns>
		private Mediator PeekPanelFromCache ()
		{
			return _currentViewStack.IsNotNullOrEmpty () ? _currentViewStack.Peek () : null;
		}

		#endregion

		/// <summary>
		/// Gets the mediator.
		/// </summary>
		/// <returns>The mediator.</returns>
		/// <param name="panel">面板名</param>
		public Mediator GetMediator (string panel)
		{
			return _mediatorDic.GetValue (panel);
		}

		#region 加载资源行为

		/// <summary>
		/// 创建面板对象，初始化后添加对应调度器 Mediator
		/// </summary>
		/// <param name="panel"></param>
		private void LoadPanel (string panel)
		{
			GameObject go = LoadPanelPref (panel);
			if (go != null) {
				GameObject panelGo = Instantiate<GameObject> (go);
				panelGo.transform.SetParent (_UIRoot);

				panelGo.GetComponent<Panel> ().InitPanel ();

				System.Type m_type = ContextBinder.GetMediator (panel);
				Mediator m = panelGo.AddComponent (m_type) as Mediator;
				m.OnRegister ();
				_mediatorDic.Add (panel, m);
			}
		}

		/// <summary>
		/// 加载面板预设
		/// </summary>
		/// <param name="panel">面板名</param>
		/// <returns></returns>
		private GameObject LoadPanelPref (string panel)
		{
			string panelPath = _prefDic.GetValue (panel);
			if (string.IsNullOrEmpty (panelPath)) {
				return null;
			}

			return Resources.Load<GameObject> (panelPath);
		}

		private void LoadConfig ()
		{
			_prefDic = new Dictionary<string, string> {
				{ "Test","UIPanel/TestPanel" }
			};
		}

		#endregion
	}
}

