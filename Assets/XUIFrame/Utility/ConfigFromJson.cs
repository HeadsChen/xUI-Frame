/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *        Config From Json
 *    Description: 
 *		  Load Config From Json
 *                  
 *    Date: 2017/10/28
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;
using System.Collections.Generic;
using System;

public class ConfigFromJson : IConfig
{

	//从Json读取的配置路径
	static Dictionary<string,string> _config;

	public Dictionary<string,string> Config{ get { return _config; } }

	/// <summary>
	/// Initializes a new instance of the <see cref="ConfigFromJson"/> class.
	/// </summary>
	/// <param name="path">配置文件路径</param>
	public ConfigFromJson (string path)
	{
		_config = new Dictionary<string, string> ();
		LoadConfigByJson (path);
	}

	/// <summary>
	/// Loads the config by json.
	/// </summary>
	/// <param name="jsonPath">Json path.</param>
	private void LoadConfigByJson (string jsonPath)
	{
		TextAsset configText = null;
		KeyValuePairs _pairs = null;

		if (string.IsNullOrEmpty (jsonPath))
			return;

		try {
			//加载Json文件并解析
			configText = ResLoader.Instance.LoadResFromResFile<TextAsset> (jsonPath, false);
			_pairs = JsonUtility.FromJson<KeyValuePairs> (configText.text);
		} catch {
			throw new JsonAnalysisException ("Json analysis exception! Please check the Json file: Resouces/" + jsonPath);
//			Debug.LogErrorFormat ("Failed to analysis.Please check the Json file: Resouces/{0}", jsonPath);
		}

		//将解析后得到的配置存入集合中
		for (int i = 0; i < _pairs.ConfigInfoList.Count; i++) {
			_config.Add (_pairs.ConfigInfoList [i].Key, _pairs.ConfigInfoList [i].Value);
		}
	}
}


public class JsonAnalysisException:Exception
{

	public JsonAnalysisException () : base ()
	{
	}

	public JsonAnalysisException (string message) : base (message)
	{
	}
}
