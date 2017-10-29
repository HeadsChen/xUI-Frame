/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *        Interface Config
 *    Description: 
 *		  ***
 *                  
 *    Date: 2017/10/28
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */

using System;
using System.Collections.Generic;

public interface IConfig
{
	/// <summary>
	/// 配置键值对 只读
	/// </summary>
	/// <value>The config.</value>
	Dictionary<string,string> Config{ get; }

}

[Serializable]
internal class KeyValuePairs
{
	public List<KeyValueNode> ConfigInfoList = null;
}

[Serializable]
internal class KeyValueNode
{
	public string Key = null;
	public string Value = null;
}