/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  Base View
 *    Description: 
 *        All the UI View inherit it.
 *                  
 *    Date: 2017/10/19
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */

using System;
using UnityEngine;

namespace XUIF
{
    public class View : MonoBehaviour
    {
       
        /// <summary>
        /// 初始化视图面板，将隶属该视图的UI控件注册到事件派发器或消息中心
        /// </summary>
        public virtual void InitView()
        {

        }

        /// <summary>
        /// 注册按钮
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="go">注册对象</param>
        protected void BindButton(string eventName, GameObject go)
        {
            ButtonBinder.BindButton(eventName, go);
        }

        /// <summary>
        /// 添加消息监听
        /// </summary>
        /// <param name="msgType">消息名</param>
        /// <param name="msgDelegate">消息委托</param>
        protected void ReceiveMessage(string msgType, ContextModel.MessageDelegate msgDelegate)
        {
            MessageDispatcher.AddListner(msgType, msgDelegate);
        }
    }
}

