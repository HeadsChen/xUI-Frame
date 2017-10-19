/***
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

}
