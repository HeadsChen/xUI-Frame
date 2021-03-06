﻿/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  Login Mediator
 *    Description: 
 *        Demo Login
 *                  
 *    Date: 2017/10/22
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;
using XUIF;

public class LoginMediator : Mediator {

    public override void OnRegister()
    {
        BindClickEvent("login", go =>
        {
            //TODO
            //连接服务器校验密码，登录等

            OpenPanel(ViewDefine.MAIN);
        });

        MessageDispatcher.SendMsg("login_title", GetText("Hello"));
    }

    string GetText(string strId)
    {
        //从文本配置表获取

        return Singleton<LanguageMgr>.Instance.GetText(strId);
    }

    public override void Freeze()
    {
        gameObject.SetActive(false);
    }
}
