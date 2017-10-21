/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  Base Mediator
 *    Description: 
 *        Panel Mediator and all the UI mediator inherit it.
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
    public class Mediator : MonoBehaviour
    {

        #region 面板状态（生命周期）逻辑对外接口

        /// <summary>
        /// 视图面板初始化完成后启动。注册事件委托。
        /// </summary>
        public virtual void OnRegister() { }

        /// <summary>
        /// 显示面板
        /// </summary>
        public virtual void Display() { }
        
        /// <summary>
        /// 隐藏面板
        /// </summary>
        public virtual void Hide() { }

        /// <summary>
        /// 冻结面板
        /// </summary>
        public virtual void Freeze() { }

        /// <summary>
        /// 恢复面板
        /// </summary>
        public virtual void Reactivate() { }

        #endregion

        #region 面板行为逻辑（绑定事件委托）

        /// <summary>
        /// 绑定点击事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="clickHandle">点击事件委托</param>
        protected void BindClickEvent(string eventName,EventTriggerListener.EventDelegate clickHandle)
        {
            bool bound = EventDispatcher.BindClickEvent(eventName, clickHandle);
			if (!bound) {
				Debug.LogFormat ("Event {0} failed to bind.", eventName);
			}
        }
        
        /// <summary>
        /// 通知管理者打开其他面板
        /// </summary>
        /// <param name="panel">新面板名</param>
        protected void OpenPanel(string panel)
        {
            UIManager.Instance.OpenPanel(panel);
        }

        /// <summary>
        /// 通知管理者回收并关闭该面板
        /// </summary>
        protected void ClosePanel()
        {

        }

        #endregion

    }
}

