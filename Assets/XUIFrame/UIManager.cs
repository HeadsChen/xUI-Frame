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

namespace XUIF
{
	public class UIManager : Singleton<UIManager>
	{      
        //维护UI树
        TreeStack<Mediator> _UITree;

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
                if (_UITree.NotOnlyRoot)
                {
                    Mediator[] freezeArr = _UITree.Peek();
                    for (int i = 0; i < freezeArr.Length; i++)
                    {
                        freezeArr[i].Freeze();
                    }
                }
                _UITree.Push(name, m);
                m.Display();
            }
            return m != null;
        }

        /// <summary>
        /// 退回至指定视图 无指定则返回上一级
        /// </summary>
        /// <param name="name">指定视图名</param>
		public void Return2Panel(string name = null)
        {
            Mediator[] hideArr = _UITree.Pull(name);
            for (int i = 0; i < hideArr.Length; i++)
            {
                hideArr[i].Hide();
            }

            Mediator[] reactArr = _UITree.Peek();
            for (int i = 0; i < reactArr.Length; i++)
            {
                reactArr[i].Reactivate();
            }
        } 

        /// <summary>
        /// 打开并列子视图
        /// </summary>
        /// <param name="name">视图名</param>
        /// <param name="parent">父视图</param>
        /// <returns></returns>
        public Mediator OpenSubPanel(string name)
        {
            Mediator m = GetMediator(name);
            if (m != null)
            {
                _UITree.AddLeaf(name, m);
                m.Display();
            }
            return m;
        }

        /// <summary>
        /// 关闭子视图
        /// </summary>
        /// <param name="panel"></param>
        public void CloseSubPanel(string panel)
        {
            Mediator m = _UITree.RemoveLeaf(panel);
            m.Hide();
        }

        #endregion

        #region UI树行为逻辑
          
        /// <summary>
        /// 创建UI树
        /// </summary>
        private void CreateTree()
        {
            Mediator m = GetMediator("Root");
            if (m != null)
            {
                _UITree = new TreeStack<Mediator>("Root", m);
                return;
            }
            Debug.LogErrorFormat("UI TreeStack failed to create.Cause:could not find the mediator {0}", "Root");
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
		public string Log(){

			return string.Format ("Current UI Tree contains:\n{0}\nCurrent panel is {1} ", _UITree.Traverse (_UITree.End), _UITree.End.Id);
		}
		#endregion


		
	}
}

