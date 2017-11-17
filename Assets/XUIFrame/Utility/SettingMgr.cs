using System.IO;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class SettingMgr : Singleton<SettingMgr> {

	Dictionary<string,string> _settingDic;

	public void LoadSettingConfig(){
		IConfig config = new ConfigFromJson (PathDefine.SETTINGCONFIG);
		_settingDic = config.Config;
	}

	public string GetSetting(string key){
		return _settingDic.GetValue(key);
	}

	public void SetSetting(string key,string value){
		if (_settingDic.ContainsKey (key)) {
			if (_settingDic [key] != value) {
				_settingDic [key] = value;
			}
		}
	}

	public void Config2Json(){
		List<KeyValueNode> nodeList = new List<KeyValueNode> ();
		foreach (var pairs in _settingDic) {
			KeyValueNode node = new KeyValueNode ();
			node.Key = pairs.Key;
			node.Value = pairs.Value;
			nodeList.Add (node);
		}
		KeyValuePairs _pairs = new KeyValuePairs ();
		_pairs.ConfigInfoList = nodeList;

		string jsonStr = JsonUtility.ToJson (_pairs);
		SaveConfig (jsonStr);

		Debug.Log ("Setting Changed");
		AssetDatabase.Refresh ();

	}

	private void SaveConfig(string json){
		string path = Application.dataPath + "/Demo/Resources/" + PathDefine.SETTINGCONFIG + ".json";
		File.WriteAllText (path, json);
	}
}
