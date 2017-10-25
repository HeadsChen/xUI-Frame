/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  Context Model
 *    Description: 
 *        Data Base.
 *                  
 *    Date: 2017/10/25
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */
 
using UnityEngine;

namespace XUIF
{
    /// <summary>
    /// 数据类
    /// </summary>
    public class ContextModel
    {
        //数据原型
        private object _value;
        //数据属性
        public object Value
        {
            get
            {
                return _value != null ? _value : "Null";
            }

            set
            {
                if (_value != value)
                {
                    Debug.LogFormat("Value {0} has changed to {1}", _value != null ? _value : "Null", value);

                    _value = value;
                    if (OnValueChanged != null)
                        OnValueChanged(_value);
                }
            }
        }

        public delegate void MessageDelegate(object value);

        private MessageDelegate OnValueChanged;
        
        public void AddListner(MessageDelegate onChanged)
        {
            OnValueChanged += onChanged;
            if (_value != null)
                OnValueChanged(_value);
        }
    }
}
