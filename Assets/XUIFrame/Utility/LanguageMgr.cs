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
	public class LanguageMgr:Singleton<LanguageMgr>
	{
		private LanguageMgr ()
		{
			_langDic = new Dictionary<string, string> ();
			LoadLangConfig ();
		}

		//语言键值对集合
		Dictionary<string, string> _langDic;

		/// <summary>
		/// 加载配置文件并解析为键值对集合
		/// </summary>
		public void LoadLangConfig ()
		{
			string path = SettingMgr.Instance.GetSetting ("Language");
			IConfig config = new ConfigFromJson (path);
			_langDic = config.Config;
		}

		public string GetText (string id)
		{
			return _langDic.GetValue (id);
		}
	}
}

