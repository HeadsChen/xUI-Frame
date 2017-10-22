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
    public class MessageCenter
    {
        //消息委托
        public delegate void MessageDelegate(object o);
        //图片精灵委托 用于发送图片参数
        public delegate void ImgDelegate(Sprite sprite);

        //消息委托集
        //<string:消息键，MessageDelegate:更新数据委托>
        static Dictionary<string, MessageDelegate> _messagesDic = new Dictionary<string, MessageDelegate>();
        //<string:消息键，ImgDelegate:更新数据委托>
        static Dictionary<string, ImgDelegate> _imgDic = new Dictionary<string, ImgDelegate>();


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

        public static void AddImgListener(string imgType, ImgDelegate delHandle)
        {
            if (!_imgDic.ContainsKey(imgType))
            {
                _imgDic.Add(imgType, null);
            }
            _imgDic[imgType] += delHandle;
        }

        /// <summary>
        /// 移除指定消息监听
        /// </summary>
        /// <param name="msgType">消息类型</param>
        public static bool RemoveMsgListener(string msgType)
        {
            return _messagesDic.Remove(msgType);
        }

        public static bool RemoveImgListener(string imgType)
        {
            return _imgDic.Remove(imgType);
        }

        /// <summary>
        /// 清空消息监听
        /// </summary>
        //public static void ClearMsgListener()
        //{
        //    if (_messagesDic != null)
        //    {
        //        _messagesDic.Clear();
        //    }
        //}

        //public static void ClearImgListener()
        //{
        //    if (_imgDic != null)
        //    {
        //        _imgDic.Clear();
        //    }
        //}

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msgType">消息类型</param>
        /// <param name="o">更新数据</param>
        public static void SendMessage(string msgType, object o)
        {
            MessageDelegate del = _messagesDic.GetValue(msgType);
            if (del != null)
            {
                del(o);
            }
        }

        public static void SendImg(string imgType, Sprite sprite)
        {
            ImgDelegate del = _imgDic.GetValue(imgType);
            if (del != null)
            {
                del(sprite);
            }
        }

    }

}

