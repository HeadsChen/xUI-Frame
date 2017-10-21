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

public static class ExtensionStack
{

    /// <summary>
    /// Determines if is not null or empty the specified stack.
    /// </summary>
    /// <returns><c>true</c> if is not null or empty the specified stack; otherwise, <c>false</c>.</returns>
    /// <param name="stack">Stack.</param>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public static bool IsNotNullOrEmpty<T>(this Stack<T> stack)
    {
        return stack != null || stack.Count >= 1;
    }

    /// <summary>
    /// 取得栈顶元素，若空栈则返回null
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stack"></param>
    /// <returns></returns>
    public static T PeekItem<T>(this Stack<T> stack) where T : class
    {
        return stack.IsNotNullOrEmpty() ? stack.Peek() : null;
    }

    /// <summary>
    /// 栈顶元素出栈，若空栈则返回null
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stack"></param>
    /// <returns></returns>
    public static T PopItem<T>(this Stack<T> stack)where T : class
    {
        return stack.IsNotNullOrEmpty() ? stack.Pop() : null;
    }
}