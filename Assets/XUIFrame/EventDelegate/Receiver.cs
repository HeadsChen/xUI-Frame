/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  Receive Listener
 *    Description: 
 *        Receive Event.
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
    public interface IReceive
    {
        void OnReceive();
    }

    public class Receiver : MonoBehaviour,IReceive
    {
        //游戏物体作为接收对象
        public delegate void ReceiveDelegate(Transform tran);

        //接收事件
        //如 接收到邮件或信息时执行的处理
        public ReceiveDelegate onReceive;

        /// <summary>
        /// 取得事件监听器
        /// </summary>
        /// <param name="tran"></param>
        /// <returns></returns>
        public static Receiver GetListener(Transform tran)
        {
            Receiver listen = tran.GetComponent<Receiver>();
            if (listen == null)
            {
                listen = tran.gameObject.AddComponent<Receiver>();
            }

            return listen;
        }


        public void OnReceive()
        {
            if (onReceive != null)
                onReceive(transform);
        }
        
    }
}

