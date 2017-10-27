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
    /// <summary>
    /// UI环境绑定
    /// 注入调度器
    /// </summary>
    class MediationBinder 
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

        public static Mediator Bind(View panel,string name)
        {
            if (!_dic.ContainsKey(name))
            {
                Debug.LogErrorFormat("{0} panel faid to bind mediator.Cause Mediator Dictionary does not contains {1}", name, name);
                return null;
            }

            return panel.gameObject.AddComponent(_dic[name]) as Mediator;
        }
        
    }
}

