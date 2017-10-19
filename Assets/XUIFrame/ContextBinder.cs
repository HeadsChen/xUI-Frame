/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  ***
 *    Description: 
 *        ***
 *                  
 *    Date: 2017/10/17
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;
using System.Collections.Generic;

namespace XUIF
{
    class ContextBinder 
    {
        static Dictionary<string, System.Type> _dic = new Dictionary<string, System.Type> {
            //添加（面板名：中转器类型）键值对，如：
            { "TestPanel",typeof(TestMediator) }




        };




        public static System.Type GetMediator(string mStr)
        {
            return _dic.ContainsKey(mStr) ? _dic[mStr] : null;
        }
    }
}

