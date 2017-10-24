/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  UI Resouces Pool
 *    Description: 
 *        Cache UI gameobject.
 *                  
 *    Date: 2017/10/23
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;
using System.Collections.Generic;

namespace XUIF
{
    public class MediatorMgr
    {
        //维护UI调度器
        //面板名:对应调度器
        Dictionary<string, Mediator> _mDic;

        //保存UI预设路径
        //用于获取时加载
        //面板名:相对 Resouces 路径
        Dictionary<string, string> _pathDic;

        private Transform _canvas;

        private MediatorMgr()
        {
            _canvas = GameObject.FindGameObjectWithTag("UICanvas").transform;
            if (_canvas.childCount != 0)
            {
                foreach (Transform item in _canvas)
                {
                    GameObject.Destroy(item.gameObject);
                }
            }
            _mDic = new Dictionary<string, Mediator>();
            InitPathDic();
        }

        #region 对外接口 获取/移除（将删除游戏物体） 调度器

        /// <summary>
        /// 获取调度器
        /// </summary>
        /// <param name="name">视图名</param>
        /// <returns></returns>
        public Mediator GetMediator(string name)
        {
            if (!_mDic.ContainsKey(name))
            {
                SetMediator(name);
            }
            return _mDic.GetValue(name);
        }

        /// <summary>
        /// 移除调度器 并删除游戏物体
        /// </summary>
        /// <param name="name"></param>
        public void RemoveMediator(string name)
        {
            if (_mDic.ContainsKey(name))
            {
                GameObject.Destroy(_mDic[name].gameObject);
                _mDic.Remove(name);
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 创建视图实例对象，初始化后添加对应调度器 Mediator
        /// </summary>
        /// <param name="name">视图名</param>
        private void SetMediator(string name)
        {
            GameObject go = LoadPanel(name);
            if (go != null)
            {
                GameObject panelGo = GameObject.Instantiate(go);
                panelGo.transform.SetParent(_canvas, false);
                panelGo.name = name;

                Mediator m = null;
                try
                {
                    Panel panel = panelGo.GetComponent<Panel>();
                    panel.InitPanel();
                    m = ContextBinder.Bind(panel, name);
                    m.OnRegister();
                    _mDic.Add(name, m);
                }
                catch(System.Exception e)
                {
                    GameObject.Destroy(panelGo);
                    Debug.LogErrorFormat("Mediator not set to {0} Panel.Cause:{1}", name, e.Message);
                }
                return;
            }

            Debug.LogWarningFormat("The Panel {0} prefab is nonexistent.Please check the path:{1}",
                name, _pathDic[name]);
        }

        /// <summary>
        /// 加载视图预设
        /// </summary>
        /// <param name="name">视图名</param>
        /// <returns></returns>
        private GameObject LoadPanel(string name)
        {
            string path = _pathDic.GetValue(name);
            if (string.IsNullOrEmpty(path))
            {
                Debug.LogWarningFormat("{0} is not exist in Config.Please check {1}.", name, "UIPathCfg");
                return null;
            }
            return ResLoader.Instance.LoadResFromResFile<GameObject>(path, false);
        }

        /// <summary>
        /// 初始化配置文件
        /// </summary>
        void InitPathDic()
        {
            _pathDic = new Dictionary<string, string> {
                {
                    "Main","UIPanel/MainPanel"
                }, {
                    "Root","UIPanel/RootPanel"
                }, {
                    "Login","UIPanel/LoginPanel"
                }, {
                    "HeroInfo","UIPanel/HeroInfoPanel"
                }, {
                    "PetInfo","UIPanel/PetInfoPanel"
                }, {
                    "DetailInfo","UIPanel/DetailInfoPanel"
                },
                {
                    "Email","UIPanel/EmailPanel"
                },
                {
                    "Award","UIPanel/AwardPanel"
                },
                {
                    "Letter","UIPanel/LetterPanel"
                },
                {
                    "Notice","UIPanel/NoticePanel"
                },
            };
        }

        #endregion

    }
}

