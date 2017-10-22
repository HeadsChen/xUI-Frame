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
			{ "Main",typeof(MainMediator) },
			{ "Root",typeof(RootMediator) },
			{ "Login",typeof(LoginMediator) },
			{ "HeroInfo",typeof(HeroInfoMediator) },
			{ "PetInfo",typeof(PetInfoMediator) },
			{ "DetailInfo",typeof(DetailInfoMediator) },
            {"Email",typeof(EmailMediator) },
            {"Notice",typeof(NoticeMediator) },
            {"Letter",typeof(LetterMediator) },
            {"Award",typeof(AwardMediator) },
        };




        public static System.Type GetMediator(string mStr)
        {
            return _dic.ContainsKey(mStr) ? _dic[mStr] : null;
        }
    }
}

