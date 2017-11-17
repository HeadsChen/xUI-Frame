/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  Extension
 *    Description: 
 *        Extension function
 *                  
 *    Date: 2017/10/19
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */
 
using UnityEngine;
using System.Collections.Generic;

public static class Extension {

	/// <summary>
	/// 存在键时替换值，否则添加键值。
	/// </summary>
	public static void AddKeyValue<Tkey,Tvalue>(this Dictionary<Tkey,Tvalue> dict,Tkey key,Tvalue value){
		if (dict != null) {
			if (dict.ContainsKey (key)) {
				dict [key] = value;
				return;
			}
			dict.Add (key, value);
		}
	}

    /// <summary>
    /// 取值。存在返回对应值，不存在或字典未定义时返回默认值。
    /// </summary>
	public static Tvalue GetValue<Tkey, Tvalue>(this Dictionary<Tkey, Tvalue> dict, Tkey key)
    {
        Tvalue value = default(Tvalue);
        if (dict != null)
        {
            dict.TryGetValue(key, out value);
        }
        return value;
    }

    /// <summary>
    /// 是否包含键。未定义时不报错。
    /// </summary>
	public static bool includeKey<Tkey,Tvalue>(this Dictionary<Tkey,Tvalue> dict,Tkey key){
		if (dict != null) {
			return dict.ContainsKey (key);
		}
		return false;
	}

    /// <summary>
    /// 地点键值对数。未定义时不报错
    /// </summary>
	public static int GetCount<Tkey,Tvalue>(this Dictionary<Tkey,Tvalue> dict){
		if (dict != null) {
			return dict.Count;
		}
		return 0;
	}

    /// <summary>
    /// 清除子物体对象
    /// </summary>
	public static void ClearChildren(this Transform trans)
	{
        if (trans.childCount == 0)
            return;
		foreach (Transform child in trans)
		{
			GameObject.Destroy(child.gameObject);
		}
	}

    /// <summary>
    /// 将预设体添加为子物体对象
    /// </summary>
	public static Transform AddChildFromPrefab(this Transform trans, Transform prefab, string name = null)
	{
		Transform childTrans = GameObject.Instantiate(prefab) as Transform;
		childTrans.SetParent(trans, false);
		if (name != null)
		{
			childTrans.gameObject.name = name;
		}
		return childTrans;
	}
}