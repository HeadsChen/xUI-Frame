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
            MessageDispatcher.SendMsg("bottom_notice", "All Email have been read.");
        });

        BindClickEvent("Add_Email", go =>
        {
            //if (_currentBox == null)
            //    return;
            MessageDispatcher.SendMsg("Notice");
        });
        
        InitEmail();
    }

    Transform _currentBox;
    
    public override void Display()
    {
        base.Display();

        OrderLabel();
    }
    
    /// <summary>
    /// 标签排序 显示第一个有邮件的标签
    ///没有则显示第一个
    /// </summary>
    private void OrderLabel()
    {

    }

    private void SelectLabel(GameObject go)
    {
        go.transform.SetAsLastSibling();
        _currentBox = go.transform.GetChild(go.transform.childCount - 1).GetChild(0);
        //string name = go.name;
        //Transform subPanel = panel.FindChild(name);
        //if (subPanel != null)
        //{
        //    subPanel.SetAsLastSibling();
        //    return;
        //}
        //Debug.LogFormat("SubPanel {0} could not show.Please check gameobject {1}",
        //            name, "Panel/" + go.name);
    }

    /// <summary>
    /// 初始化信箱
    /// </summary>
    private void InitEmail()
    {
        ReceiverBinder.BindReceiveEvent("Notice", tran =>
        {
            Debug.LogFormat("{0} box receive a Email.",tran.name);
        });

        ReceiverBinder.BindReceiveEvent("Notice", tran =>
        {
            Debug.LogFormat("{0} box receive a Email.", tran.name);
        });

        ReceiverBinder.BindReceiveEvent("Notice", tran =>
        {
            Debug.LogFormat("{0} box receive a Email.", tran.name);
        });
    }


    //private Transform panel;

    //private void SetSubPanel()
    //{
    //    string[] nameArr = GetSubPanelNameArr();

    //    panel = transform.GetChild(transform.childCount - 1);

    //    for (int i = 0; i < nameArr.Length; i++)
    //    {
    //        Mediator m = UIManager.Instance.OpenSubPanel(nameArr[i]);
    //        m.transform.SetParent(panel, false);
    //    }
    //}

    //private string[] GetSubPanelNameArr()
    //{
    //    return new string[3] { "Award", "Letter", "Notice" };
    //}
}
