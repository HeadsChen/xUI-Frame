/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  Receiver Binder
 *    Description: 
 *        GameObject binds Receiver.
 *                  
 *    Date: 2017/10/27
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;
using System.Collections.Generic;

namespace XUIF
{
    public class ReceiverBinder
    {
        //接收监听器集合
        //<string：监听事件名,ReceiveListener：监听器>
        static Dictionary<string, Receiver> _receiverDic = new Dictionary<string, Receiver>();

        /// <summary>
        /// 为对象绑定接收监听器
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="tran"></param>
        public static void BindReceiver(string eventName, Transform tran)
        {
            Receiver listener = Receiver.GetListener(tran);
            _receiverDic.Add(eventName, listener);
        }

        /// <summary>
        /// 为监听器绑定接收事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="receiveDelegate"></param>
        /// <returns></returns>
        public static Transform BindReceiveEvent(string eventName, Receiver.ReceiveDelegate receiveDelegate)
        {
            Receiver listener = _receiverDic.GetValue(eventName);
            if (listener != null)
            {
                listener.onReceive = receiveDelegate;
                MessageDispatcher.AddReceiver(eventName, listener);
                return listener.transform;
            }
            return null;
        }
    }
}
