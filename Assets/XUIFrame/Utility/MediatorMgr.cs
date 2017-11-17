/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  UI Resouces Pool
 *    Description: 
 *        Cache UI gameobject.
 *                  
 *    Date: 2017/10/23
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;
using System.Collections.Generic;

namespace XUIF
{
	public class MediatorMgr:Singleton<MediatorMgr>
	{
		//维护UI调度器
		//面板名:对应调度器
		Dictionary<string, Mediator> _mDic;

		private Transform _canvas;
		private Transform Canvas{
			get{
				if (_canvas == null) {
					_canvas = GameObject.Find ("Canvas").transform;
				}
				return _canvas;
			}
		}



		private MediatorMgr ()
		{
			if (Canvas.childCount != 0) {
				foreach (Transform item in Canvas) {
					GameObject.Destroy (item.gameObject);
				}
			}
			_mDic = new Dictionary<string, Mediator> ();
		}

		#region 对外接口 获取/移除（将删除游戏物体） 调度器

		/// <summary>
		/// 获取调度器
		/// </summary>
		/// <param name="name">视图名</param>
		/// <returns></returns>
		public Mediator GetMediator (string name)
		{
			if (!_mDic.ContainsKey (name)) {
				SetMediator (name);
			}
			return _mDic.GetValue (name);
		}

		/// <summary>
		/// 移除调度器 并删除游戏物体
		/// </summary>
		/// <param name="name"></param>
		public void RemoveMediator (string name)
		{
			if (_mDic.ContainsKey (name)) {
				GameObject.Destroy (_mDic [name].gameObject);
				_mDic.Remove (name);
			}
		}


		public void ClearMediator(){
			foreach (var m in _mDic.Values) {
				GameObject.Destroy (m.gameObject);
			}
			_mDic.Clear ();
		}

		#endregion

		#region 私有方法

		/// <summary>
		/// 创建视图实例对象，初始化后添加对应调度器 Mediator
		/// </summary>
		/// <param name="name">视图名</param>
		private void SetMediator (string name)
		{
			GameObject go = LoadPanel (name);
			if (go != null) {
				GameObject panelGo = GameObject.Instantiate (go);
				panelGo.transform.SetParent (Canvas, false);
				panelGo.name = name;

				Mediator m = null;
				try {
					View panel = panelGo.GetComponent<View> ();
					panel.InitView ();
					m = MediationBinder.Bind (panel, name);
					if (m != null) {
						m.OnRegister ();
						_mDic.AddKeyValue (name, m);
					}
				} catch (System.Exception e) {
//					GameObject.Destroy (panelGo);
					Debug.LogErrorFormat ("{0} Mediator can not set to {0} Panel.Cause:{1}", name, e.Message);
				}
				return;
			}

			Debug.LogWarningFormat ("The Panel {0} prefab is nonexistent.Please check the UIPanel path",
				name);
		}

		/// <summary>
		/// 加载视图预设
		/// </summary>
		/// <param name="name">视图名</param>
		/// <returns></returns>
		private GameObject LoadPanel (string name)
		{
			string path = PathDefine.PANEL + name + "Panel";

			return ResLoader.Instance.LoadResFromResFile<GameObject> (path, false);
		}

		#endregion
	}
}

