/***
 *    Project:
 *		  xUI Frame
 *    Title: 
 *		  Email Mediator
 *    Description: 
 *        Demo email mediator
 *                  
 *    Date: 2017/10/22
 *    Version: 0.1
 *    Modify Recoder: 
 *    
 *   
 */

using UnityEngine;
using XUIF;

public class EmailMediator : Mediator {

    public override void OnRegister()
    {
        BindClickEvent("Notice", go =>
        {
            SelectLabel(go);
        });

        BindClickEvent("Letter", go =>
        {
            SelectLabel(go);
        });

        BindClickEvent("Award", go =>
        {
            SelectLabel(go);
        });

        BindClickEvent("Get_All", go =>
        {
            MessageCenter.SendMessage("bottom_notice", "All Email have been read.");
        });

        BindClickEvent("Close_Email", go =>
        {
            ClosePanel();
        });
    }

    public override void Display()
    {
        base.Display();

        SetSubPanel();
    }

    private void SelectLabel(GameObject go)
    {
        string name = go.name + "Panel";
        Transform subPanel = panel.FindChild(name);
        if (subPanel != null)
        {
            subPanel.SetAsLastSibling();
            return;
        }
        Debug.LogFormat("SubPanel {0} could not show.Please check gameobject {1}",
                    name, "Label/" + go.name);
    }

    private Transform panel;

    private void SetSubPanel()
    {
        string[] nameArr = GetSubPanelNameArr();

        panel = transform.GetChild(transform.childCount - 1);

        for (int i = 0; i < nameArr.Length; i++)
        {
            UIManager.Instance.OpenSubPanel(nameArr[i], panel);
        }
    }

    private string[] GetSubPanelNameArr()
    {
        return new string[3] { "Award", "Letter", "Notice" };
    }
}
