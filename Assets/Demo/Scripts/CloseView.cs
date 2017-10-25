/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  Close View
 *    Description: 
 *        Close self panel
 *                  
 *    Date: 2017/10/25
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;
using XUIF;

public class CloseView : MonoBehaviour {

    [SerializeField]
    private bool isSubPanel;

    private string viewName;

	// Use this for initialization
	void Start () {

        viewName = transform.parent.name;

        EventTriggerListener listen = EventTriggerListener.GetListener(gameObject);
        listen.onClick = go =>
        {
            if (isSubPanel)
            {
                if (string.IsNullOrEmpty(viewName))
                {
                    Debug.LogErrorFormat("Close SubPanel must assign a subpanel name.");
                    return;
                }
                Singleton<UIManager>.Instance.CloseSubPanel(viewName);
                return;
            }
            Singleton<UIManager>.Instance.Return2Panel();
        };
	}
}
