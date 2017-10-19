/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  Event Listener
 *    Description: 
 *        GameObject binds event here
 *                  
 *    Date: 2017/10/14
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class EventTriggerListener : EventTrigger {

    //游戏物体作为监听对象
    public delegate void EventDelegate(GameObject go);

    //根据项目需要添加委托事件
    public EventDelegate onClick;
    public EventDelegate onDown;
    public EventDelegate onUp;

    /// <summary>
    /// 取得“事件监听器”
    /// </summary>
    /// <param name="go">监听对象</param>
    /// <returns></returns>
    public static EventTriggerListener GetListener(GameObject go)
    {
        EventTriggerListener listen = go.GetComponent<EventTriggerListener>();
        if (listen == null)
            listen = go.AddComponent<EventTriggerListener>();
        return listen;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null)
        {
            onClick(gameObject);
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (onDown != null)
        {
            onDown(gameObject);
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (onUp != null)
        {
            onUp(gameObject);
        }
    }

}
