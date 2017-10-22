/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  Base Panel
 *    Description: 
 *        All the UI Panel inherit it.
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
    public class Panel : MonoBehaviour
    {

        /// <summary>
        /// 初始化视图面板，将隶属该视图的UI控件注册到事件派发器或消息中心
        /// </summary>
        public virtual void InitPanel()
        {

        }

        /// <summary>
        /// 注册按钮
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="go">注册对象</param>
        protected void RegisterButton(string eventName,GameObject go)
        {
            EventDispatcher.BindButton(eventName, go);
        }

        /// <summary>
        /// 添加消息监听
        /// </summary>
        /// <param name="msgType">消息名</param>
        /// <param name="msgDelegate">消息委托</param>
        protected void AddMessage(string msgType,MessageCenter.MessageDelegate msgDelegate)
        {

            MessageCenter.AddMsgListener(msgType, msgDelegate);
        }
        
    }
}

