/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  UI Manager
 *    Description: 
 *        The core of this frame.
 *        Manage all the View and ViewCtrl.
 *         
 *                  
 *    Date: 2017/10/19
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace XUIF
{
    public class UIManager : UnitySingleton<UIManager>
    {
        //#region 单例
        //private static UIManager _instance;
        //public static UIManager Instance
        //{
        //    get
        //    {
        //        if (_instance == null)
        //        {
        //            _instance = new UIManager();
        //        }
        //        return _instance;
        //    }
        //}
        //#endregion

        public Transform _UIRoot;

        Dictionary<string, string> _prefDic;
        Dictionary<string, Mediator> _mediatorDic;

        Stack<Mediator> _currentViewStack;



        private void Start()
        {
            //GameObject panel = Instantiate(LoadPanelPref());
            //panel.transform.SetParent(_UIRoot);
            //panel.transform.localPosition = Vector3.zero;
            //panel.transform.localEulerAngles = Vector3.zero;
            //panel.GetComponent<Panel>().InitPanel();
            //Mediator m = panel.AddComponent(ContextBinder.GetMediator(LoadConfig())) as Mediator;
            //m.OnRegister();
            LoadConfig();
        }

        #region 加载资源行为

        /// <summary>
        /// 创建面板对象，初始化后添加对应调度器 Mediator
        /// </summary>
        /// <param name="panel"></param>
        private void LoadPanel(string panel)
        {
            GameObject panelGo = Instantiate(LoadPanelPref(panel));
            panelGo.transform.SetParent(_UIRoot);

            panelGo.GetComponent<Panel>().InitPanel();

            System.Type m_type = ContextBinder.GetMediator(panel);
            Mediator m = panelGo.AddComponent(m_type) as Mediator;
            m.OnRegister();
            _mediatorDic.Add(panel, m);
        }
        
        /// <summary>
        /// 加载面板预设
        /// </summary>
        /// <param name="panel">面板名</param>
        /// <returns></returns>
        private GameObject LoadPanelPref(string panel)
        {
            string panelPath = _prefDic.GetValue(panel);
            if (string.IsNullOrEmpty(panelPath))
            {
                return null;
            }

            return Resources.Load<GameObject>(panelPath);
        }

        private void LoadConfig()
        {
            _prefDic = new Dictionary<string, string>
            {
                {"Test","UIPanel/TestPanel" }
            };
        }

        #endregion
    }
}

