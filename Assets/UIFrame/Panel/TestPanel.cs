/***
 *    Project:
 *		  ***
 *    Title: 
 *		  ***
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

public class TestPanel : Panel {

    public Text text;
    public Image img;
    public Button btn;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void InitPanel()
    {
        RegisterButton("btn", btn.gameObject);

        AddMessage("text", o =>
        {
            text.text = o.ToString();
        });

        AddMessage("img", o =>
        {
            img.color = (Color)o;
        });
    }
}
