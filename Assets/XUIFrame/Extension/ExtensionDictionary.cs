﻿/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  Extension Dictionary
 *    Description: 
 *        ***
 *                  
 *    Date: 2017/10/19
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */
 
using System.Collections.Generic;

public static class ExtensionDictionary {

    /// <summary>
	/// this Dictionary<Tkey,Tvalue> dict 表示要获取值的字典
	/// </summary>
	public static Tvalue GetValue<Tkey, Tvalue>(this Dictionary<Tkey, Tvalue> dict, Tkey key)
    {        
        Tvalue value;
        dict.TryGetValue(key, out value);
        return value;
    }

	public static bool includeKey<Tkey,Tvalue>(this Dictionary<Tkey,Tvalue> dict,Tkey key){
		if (dict != null) {
			return dict.ContainsKey (key);
		}
		return false;
	}

	public static int GetCount<Tkey,Tvalue>(this Dictionary<Tkey,Tvalue> dict){
		if (dict != null) {
			return dict.Count;
		}
		return 0;
	}
}
