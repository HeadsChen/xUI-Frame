﻿/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *        Message Center
 *    Description: 
 *		  View context message transfer center.
 *                  
 *    Date: 2017/10/14
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;
using System.Collections.Generic;

namespace XUIF
{
    public class MessageCenter
    {
        //消息委托
        public delegate void MessageDelegate(object o);

        //消息委托集
        //<string:消息键，MessageDelegate:更新数据委托>
        static Dictionary<string, MessageDelegate> _messagesDic = new Dictionary<string, MessageDelegate>();



        /// <summary>
        /// 添加消息监听
        /// </summary>
        /// <param name="msgType">消息类型</param>
        /// <param name="delHandle">消息委托</param>
        public static void AddMsgListener(string msgType, MessageDelegate delHandle)
        {
            if (!_messagesDic.ContainsKey(msgType))
            {
                _messagesDic.Add(msgType, null);
            }
            _messagesDic[msgType] += delHandle;
        }
        
        /// <summary>
        /// 移除指定消息委托
        /// </summary>
        /// <param name="msgType">消息类型</param>
        /// <param name="delHandle">消息委托</param>
        //public static void RemoveMsgListener(string msgType,MessageDelegate delHandle)
        //{
        //    if (_messagesDic.ContainsKey(msgType))
        //    {
        //        _messagesDic[msgType] -= delHandle;
        //    }
        //}

        /// <summary>
        /// 移除指定消息监听
        /// </summary>
        /// <param name="msgType">消息类型</param>
        public static bool RemoveMsgListener(string msgType)
        {
            return _messagesDic.Remove(msgType);
        }

        /// <summary>
        /// 清空消息监听
        /// </summary>
        public static void ClearMsgListener()
        {
            if (_messagesDic != null)
            {
                _messagesDic.Clear();
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msgType">消息类型</param>
        /// <param name="o">更新数据</param>
        public static void SendMessage(string msgType, object o)
        {
            MessageDelegate del;
            if (_messagesDic.TryGetValue(msgType, out del))
            {
                if (del != null)
                {
                    del(o);
                }
            }
        }

    }
}

