/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  Resouces Loader
 *    Description: 
 *        Load and get resouces
 *                  
 *    Date: 2017/10/23
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;
using System.Collections.Generic;

public class ResLoader :Singleton<ResLoader> {

    private Dictionary<string,Object> _resCache;

    private ResLoader()
    {
        _resCache = new Dictionary<string, Object>();
    }

    
    public T LoadResFromResFile<T>(string path,bool cache)where T : Object
    {
        if (_resCache.ContainsKey(path))
        {
            return _resCache[path] as T;
        }

        T TRes = Resources.Load<T>(path);
        if (TRes == null)
        {
            Debug.LogError("目标资源无法加载，检查：Resouces/" + path);
        }else if (cache)
        {
            _resCache.Add(path, TRes);
        }
        return TRes;
    }

	//其他加载方式
	//异步、AB、www加载
}
