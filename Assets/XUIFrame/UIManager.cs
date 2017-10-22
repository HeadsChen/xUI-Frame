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

		public Transform _Canvas;

		Dictionary<string, string> _prefDic;
		Dictionary<string, Mediator> _mediatorDic;

        UITree<Mediator> _UITree;

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
            CreateTree();

            OpenPanel("Login");
		}

        #region 对外接口。打开、关闭面板方式各两种

        /// <summary>
        /// 打开新面板，根据是否为子面板添加为叶节点或末梢节点。
        /// 添加为末梢时，冻结前末梢面板。
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        public bool OpenPanel(string panel)
        {
            Mediator m = GetMediator(panel);
            if (m != null)
            {
                if (_UITree.NotOnlyRoot())
                {
                    Mediator[] freezeArr = _UITree.PeekEnd();
                    for (int i = 0; i < freezeArr.Length; i++)
                    {
                        freezeArr[i].Freeze();
                    }
                }
                _UITree.PushEnd(panel, m);
                m.Display();
            }

			Log ();

            return m != null;
        }

        /// <summary>
        /// 关闭末梢面板，重激活前末梢面板
        /// </summary>
		public void ClosePanel(){
            Mediator[] hideArr = _UITree.PopEnd();
            for (int i = 0; i < hideArr.Length; i++)
            {
                hideArr[i].Hide();
            }

            Mediator[] reactArr = _UITree.PeekEnd();
            for (int i = 0; i < reactArr.Length; i++)
            {
                reactArr[i].Reactivate();
            }

			Log ();
		}        

        public bool OpenSubPanel(string panel,Transform parent=null)
        {
            Mediator m = GetMediator(panel);
            if (m != null)
            {

                if (parent != null)
                {
                    m.transform.SetParent(parent, false);
                }
                _UITree.AddLeaf(panel, m);
                m.Display();
            }

			Log ();

            return m != null;
        }

        /// <summary>
        /// 关闭子面板
        /// </summary>
        /// <param name="panel"></param>
        public void CloseSubPanel(string panel)
        {
            Mediator m = _UITree.RemoveLeaf(panel);
            m.Hide();

			Log ();
        }

        #endregion

        #region UI树行为逻辑
          
        /// <summary>
        /// 创建UI树
        /// </summary>
        private void CreateTree()
        {
            Mediator m = GetMediator("Root");
            _UITree = new UITree<Mediator>("Root", m);
            if (m != null)
            {
                Debug.Log("UI Tree created.");
            }
        }

        //      /// <summary>
        //      /// 添加新面板到UI树末梢
        //      /// </summary>
        //      /// <param name="panel">面板名</param>
        //      /// <param name="m">面板调度器</param>
        //      private void PushPanel2End (string panel, Mediator m)
        //{
        //          _UITree.PushEnd(panel, m);
        //}

        //      /// <summary>
        //      /// Pops the panel from UITree end.
        //      /// </summary>
        //      /// <returns>The panel from cache.</returns>
        //      private Mediator[] PopPanelFromEnd ()
        //{
        //          return _UITree.PopEnd();
        //}

        ///// <summary>
        ///// Peeks the panel from UITree end.
        ///// </summary>
        ///// <returns>The panel from cache.</returns>
        //private Mediator[] PeekPanelFromEnd()
        //{
        //    return _UITree.PeekEnd();
        //}


        //      private void AddLeaf2End(string panel, Mediator m)
        //      {
        //          _UITree.AddLeaf(panel, m);
        //      }

        //      private Mediator RemoveLeafFromEnd(string panel)
        //      {
        //          return _UITree.RemoveLeaf(panel);
        //      }

        #endregion

        /// <summary>
        /// Gets the mediator.
        /// </summary>
        /// <returns>The mediator.</returns>
        /// <param name="panel">面板名</param>
        private Mediator GetMediator (string panel)
		{
            if (!_mediatorDic.ContainsKey(panel))
            {
                LoadPanel(panel);
            }
            return _mediatorDic.GetValue(panel);
		}

		#region 测试用
		private void Log(){
			Debug.LogFormat ("Current UI Tree contains : {0}", _UITree.Traverse (_UITree.End));

			Debug.LogFormat ("Current panel is {0}", _UITree.End.Id);
		}
		#endregion


		#region 加载资源行为

		/// <summary>
		/// 创建面板对象，初始化后添加对应调度器 Mediator
		/// </summary>
		/// <param name="panel"></param>
		private void LoadPanel (string panel)
		{
			GameObject go = LoadPanelPref (panel);
			if (go != null) {
                GameObject panelGo = Instantiate(go);
                panelGo.transform.SetParent(_Canvas, false);
                panelGo.GetComponent<Panel> ().InitPanel ();
                panelGo.name = panelGo.name.Replace("(Clone)", "");

                try
                {
                    System.Type m_type = ContextBinder.GetMediator(panel);
                    Mediator m = panelGo.AddComponent(m_type) as Mediator;
                    _mediatorDic.Add(panel, m);
                    m.OnRegister();
                }
                catch
                {
                    Debug.LogErrorFormat("Mediator not set to {0}Panel", panel);
                }
				
                return;
			}
            Debug.LogWarningFormat("The Panel {0} prefab is nonexistent.Please check the path:{1}",
                panel, _prefDic[panel]);
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

        private void LoadConfig()
		{
			_prefDic = new Dictionary<string, string> { 
				{
					"Main","UIPanel/MainPanel"
				}, {
					"Root","UIPanel/RootPanel"
				}, {
					"Login","UIPanel/LoginPanel"
				}, {
					"HeroInfo","UIPanel/HeroInfoPanel"
				}, {
					"PetInfo","UIPanel/PetInfoPanel"
				}, {
					"DetailInfo","UIPanel/DetailInfoPanel"
                },
                {
                    "Email","UIPanel/EmailPanel"
                },
                {
                    "Award","UIPanel/AwardPanel"
                },
                {
                    "Letter","UIPanel/LetterPanel"
                },
                {
                    "Notice","UIPanel/NoticePanel"
                },
            };
		}

		#endregion
	}
}

