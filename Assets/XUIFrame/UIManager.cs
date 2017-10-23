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
	public class UIManager : Singleton<UIManager>
	{      
        //维护UI树
        UITree<Mediator> _UITree;

		private UIManager()
		{
            CreateTree();

            OpenPanel("Login");
		}

        #region 对外接口。打开、关闭面板方式各两种

        /// <summary>
        /// 打开新视图
        /// 添加为末梢时，冻结前末梢视图。
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool OpenPanel(string name)
        {
            Mediator m = GetMediator(name);
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
                _UITree.PushEnd(name, m);
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

        /// <summary>
        /// 打开子视图
        /// </summary>
        /// <param name="name">视图名</param>
        /// <param name="parent">父视图</param>
        /// <returns></returns>
        public bool OpenSubPanel(string name,Transform parent=null)
        {
            Mediator m = GetMediator(name);
            if (m != null)
            {
				
                if (parent != null)
                {
                    m.transform.SetParent(parent, false);
                }
                _UITree.AddLeaf(name, m);
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


		public void PullToPanel(string name){
			Mediator[] hideArr = _UITree.Pull (name);
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
                
        #endregion

        /// <summary>
        /// Gets the mediator.
        /// </summary>
        /// <returns>The mediator.</returns>
        /// <param name="panel">面板名</param>
        private Mediator GetMediator (string name)
		{
            return Singleton<MediatorMgr>.Instance.GetMediator(name);
		}

		#region 测试用
		private void Log(){
			Debug.LogFormat ("Current UI Tree contains : {0}", _UITree.Traverse (_UITree.End));

			Debug.LogFormat ("Current panel is {0}", _UITree.End.Id);
		}
		#endregion


		
	}
}

