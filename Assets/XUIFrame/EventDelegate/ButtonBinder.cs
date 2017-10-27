/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  Event Binder
 *    Description: 
 *        GameObject binds event.
 *                  
 *    Date: 2017/10/19
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;
using System.Collections.Generic;

namespace XUIF
{
    public class ButtonBinder
    {
        //事件监听器集合
        //<string：监听事件名,EventTriggerListener：监听器>
        static Dictionary<string, ButtonTriggerListener> _eventListenerDic = new Dictionary<string, ButtonTriggerListener>();
        
        /// <summary>
        /// 为对象绑定按钮监听器
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="go">绑定监听器的对象</param>
        public static void BindButton(string eventName, GameObject go)
        {
            ButtonTriggerListener listener = ButtonTriggerListener.GetListener(go);
            _eventListenerDic.Add(eventName, listener);
        }

        /// <summary>
        /// 为监听器绑定点击事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="clickDelgate">点击事件委托</param>
        /// <returns></returns>
        public static bool BindClickEvent(string eventName, ButtonTriggerListener.EventDelegate clickDelgate)
        {
            ButtonTriggerListener listener = _eventListenerDic.GetValue(eventName);
            if (listener != null)
            {
                listener.onClick += clickDelgate;
                return true;
            }
            return false;
        }


        

        //根据实际需要添加绑定事件
    }
}

