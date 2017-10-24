/***
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
using XUIF;

public class Launch : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Singleton<UIManager>.Create();
	}
	
}
