using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class ucTabs : System.Web.UI.UserControl
{
    #region Example
    /* # Design
        <uc1:ucTabs ID="ucTabs1" runat="server"/>
    */
    /* # Code Behind
        ucTabs1.UID = "Tabs1";
        ucTabs1.Effect = ucTabs.Effects.scaleUp;

        ucTabs.Tab tab = new ucTabs.Tab();
        tab.Name = "ทดสอบแทปแรก";
        tab.Content = "ทดสอบข้อความในแทปแรก<br/>ทดสอบหลายๆบรรทัดหน่อย<br/>ดูว่าโอเคป่าว";
        ucTabs1.Tabs.Add(tab);

        tab = new ucTabs.Tab();
        tab.Name = "ทดสอบแทป 2";
        tab.Content = "ทดสอบข้อความในแทป 2";
        ucTabs1.Tabs.Add(tab);

        tab = new ucTabs.Tab();
        tab.Name = "ทดสอบแทป 3";
        tab.Content = "ทดสอบข้อความในแทป 3";
        ucTabs1.Tabs.Add(tab);
    */
    #endregion
    #region Global Variable
    public enum Effects
    {
        scale,slideLeft,scaleUp,flip
    }
    #endregion
    #region Property
    private string _uid = "Tabs";
    public string UID
    {
        get { return _uid; }
        set { _uid = value; }
    }
    private Effects _effect=Effects.scale;
    public Effects Effect
    {
        get { return _effect; }
        set { _effect = value; }
    }
    private string _width="100%";
    public string Width
    {
        get { return _width; }
        set { _width = value; }
    }

    private List<Tab> _tabs = new List<Tab>();
    public List<Tab> Tabs
    {
        get { return _tabs; }
        set { _tabs = value; }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    if (_tabs != null)
        //    {
        //        TabsBuilder();
        //    }
        //}
        if (_tabs != null)
        {
            TabsBuilder();
        }
    }

    private void TabsBuilder()
    {
        #region Variable
        StringBuilder strScript = new StringBuilder();
        StringBuilder strName = new StringBuilder();
        StringBuilder strContent = new StringBuilder();
        #endregion
        #region Data Builder
        #region Name & Content Builder
        for (int i = 0; i < _tabs.Count; i++)
        {
            strName.Append("<li>");
            strName.Append("<a class='nav' href='#"+UID+(i+1).ToString()+"' title=''>");
            strName.Append(_tabs[i].Name);
            strName.Append("</a>");
            strName.Append("</li>");

            strContent.Append("<div id='"+UID+(i+1).ToString()+"'>");
            strContent.Append(_tabs[i].Content);
            strContent.Append("</div>");
        }
        #endregion
        strScript.Append("<div id='"+_uid+"' style='width:"+_width+";'>");
        strScript.Append("<ul>");
        strScript.Append(strName.ToString());
        strScript.Append("</ul>");
        strScript.Append("<div id='tabs_container'>");
        strScript.Append(strContent.ToString());
        strScript.Append("</div>");
        strScript.Append("</div>");
        #endregion

        lblTabs.Text = strScript.ToString();
        strName.Length = 0; strName.Capacity = 0;
        strContent.Length = 0; strContent.Capacity = 0;
        strScript.Length = 0; strScript.Capacity = 0;
    }

    public class Tab
    {
        public string Name { get; set; }
        public string Content { get; set; }
    }
}