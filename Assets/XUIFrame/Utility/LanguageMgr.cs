/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  Language Manager
 *    Description: 
 *        Language Localization
 *                  
 *    Date: 2017/10/29
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;
using System.Collections.Generic;

namespace XUIF
{
    public class LanguageMgr
    {
        class LangPath
        {
            //配置文件路径
            string _path;

            public string Path { get { return _path; } }

            public LangPath(string path)
            {
                _path = path;
            }

            public static readonly LangPath CHINESE = new LangPath("Config/Localization/CHINESE");
            public static readonly LangPath ENGLISH = new LangPath("Config/Localization/ENGLISH");
        }

        private LanguageMgr()
        {
            _langDic = new Dictionary<string, string>();
            LoadLangConfig();
        }

        //语言键值对集合
        Dictionary<string, string> _langDic;

        /// <summary>
        /// 加载配置文件并解析为键值对集合
        /// </summary>
        void LoadLangConfig()
        {
            IConfig config = new ConfigFromJson(LangPath.CHINESE.Path);
            _langDic = config.Config;
        }
         
        public string GetText(string id)
        {
            return _langDic.GetValue(id);
        }
    }
}

