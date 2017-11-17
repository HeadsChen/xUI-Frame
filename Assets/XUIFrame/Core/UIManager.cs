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
        TreeStack<Mediator> _UITreeStack;

		private UIManager()
		{
            CreateTreeStack();

            OpenPanel(ViewDefine.LOGIN);
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
                if (_UITreeStack.NotOnlyRoot)
                {
                    Mediator[] freezeArr = _UITreeStack.Peek();
                    for (int i = 0; i < freezeArr.Length; i++)
                    {
                        freezeArr[i].Freeze();
                    }
                }
                _UITreeStack.Push(name, m);
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
            Mediator[] hideArr = _UITreeStack.Pull(name);
            for (int i = 0; i < hideArr.Length; i++)
            {
                hideArr[i].Hide();
            }

            Mediator[] reactArr = _UITreeStack.Peek();
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
                _UITreeStack.AddLeaf(name, m);
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
            Mediator m = _UITreeStack.RemoveLeaf(panel);
            m.Hide();
        }

		public void ClearTreeStack(){
			MediatorMgr.Instance.ClearMediator();
			_UITreeStack.Clear ();
		}

        #endregion

        #region UI树行为逻辑
          
        /// <summary>
        /// 创建UI树
        /// </summary>
        private void CreateTreeStack()
        {
            Mediator m = GetMediator(ViewDefine.ROOT);
            if (m != null)
            {
                _UITreeStack = new TreeStack<Mediator>("Root", m);
                return;
            }
			Debug.LogErrorFormat("UI TreeStack failed to create.Cause:could not find the {0} mediator.", "Root");
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

		#if UNITY_EDITOR
		#region 测试用
		public string Log(){
			if(_UITreeStack!=null)
				return string.Format ("Current UI Tree contains:\n{0}\nCurrent panel is {1} ", _UITreeStack.Traverse (_UITreeStack.End), _UITreeStack.End.Id);
			return null;
		}
		#endregion
		#endif

		
	}
}

