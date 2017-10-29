/***
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
    public class MessageDispatcher
    {
        //数据模型集合
        //数据类别键：上下文数据模型
        static Dictionary<string, ContextModel> _modelDic = new Dictionary<string, ContextModel>();

        //接收器集合
        //接收器名：接收器
        static Dictionary<string, IReceive> _receiveDic = new Dictionary<string, IReceive>();

        /// <summary>
        /// 初始化数据模型键值对
        /// </summary>
        /// <param name="msgType">数据类别键</param>
        /// <param name="o">数据初始值</param>
        public static void RegisterModel(string msgType,object o)
        {
            if (_modelDic.ContainsKey(msgType))
            {
                _modelDic[msgType].Value = o;
                return;
            }
            ContextModel model = new ContextModel();
            model.Value = o;
            _modelDic.Add(msgType, model);
        }

        /// <summary>
        /// 添加监听器
        /// </summary>
        /// <param name="msgType">数据类别键</param>
        /// <param name="delHandle">消息委托</param>
        public static void AddListner(string msgType, ContextModel.MessageDelegate delHandle)
        {
            if (_modelDic.ContainsKey(msgType))
            {
                _modelDic[msgType].AddListner(delHandle);
                return;
            }
            ContextModel model = new ContextModel();
            model.AddListner(delHandle);
            _modelDic.Add(msgType, model);
        }

        /// <summary>
        /// 移除监听器
        /// </summary>
        /// <param name="msgType"></param>
        public static void RemoveListner(string msgType)
        {
            _modelDic.Remove(msgType);
        }

        /// <summary>
        /// 发送消息 用于更新数据
        /// </summary>
        /// <param name="msgType">数据类别键</param>
        /// <param name="o">新数据</param>
        public static void SendMsg(string msgType,object o)
        {
            if (_modelDic.ContainsKey(msgType))
            {
                _modelDic[msgType].Value = o;
            }
        }


        public static void AddReceiver(string msgType,IReceive rec)
        {
            _receiveDic[msgType] = rec;
        }

        public static void RemoveReceive(string msgType)
        {
            _receiveDic.Remove(msgType);
        }

        public static void SendMsg(string msgType)
        {
            if (_receiveDic.ContainsKey(msgType))
            {
                _receiveDic[msgType].OnReceive();
            }
        }
    }


}

