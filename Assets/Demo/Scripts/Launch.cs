﻿/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  Launch Frame
 *    Description: 
 *        ***
 *                  
 *    Date: 2017
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using XUIF;

public class Launch : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		MessageDispatcher.ClearMsg ();
		Singleton<SettingMgr>.Instance.LoadSettingConfig ();
		Singleton<LanguageMgr>.Instance.LoadLangConfig ();
		Singleton<UIManager>.Create ();
	}


//	void OnGUI ()
//	{
//
////		GUI.Label (new Rect (400, 10, 600, 100), Singleton<UIManager>.Instance.Log ());
////		GUI.Label (new Rect (400, 100, 600, 100), Singleton<MediatorMgr>.Instance.Log ());
//
//	}
}
