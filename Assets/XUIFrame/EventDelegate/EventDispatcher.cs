/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  Event Dispatcher
 *    Description: 
 *        Button binds and dispatchs event 
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
    public class EventDispatcher
    {
        //监听器集合
        //<string：监听事件名,EventTriggerListener：监听器>
        static Dictionary<string, EventTriggerListener> _listenerDic = new Dictionary<string, EventTriggerListener>();

        /// <summary>
        /// 为对象绑定按钮监听器
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="go">绑定监听器的对象</param>
        public static void BindButton(string eventName, GameObject go)
        {
            EventTriggerListener listener = EventTriggerListener.GetListener(go);
            _listenerDic.Add(eventName, listener);
        }

        /// <summary>
        /// 为监听器绑定点击事件
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="clickDelgate">点击事件委托</param>
        /// <returns></returns>
        public static bool BindClickEvent(string eventName, EventTriggerListener.EventDelegate clickDelgate)
        {
            EventTriggerListener listener = _listenerDic.GetValue(eventName);
            if (listener != null)
            {
                listener.onClick += clickDelgate;
                return true;
            }
            return false;
        }

        //public static bool RemoveClickEvent(string eventName)
        //{

        //}

        //根据实际需要添加绑定事件
    }
}

