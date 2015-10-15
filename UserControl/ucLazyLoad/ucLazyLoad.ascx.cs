using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_ucLazyLoad_ucLazyLoad : System.Web.UI.UserControl
{
    private Effects _effect=Effects.Spinner;
    public Effects Effect
    {
        get { return _effect; }
        set { _effect = value; }
    }

    public enum Effects
    {
        Spinner,
        Fadein
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <example>
    /// <uc1:ucLazyLoad ID="ucLazyLoad1" runat="server" Effect="Spinner"/>
    /// <uc1:ucLazyLoad ID="ucLazyLoad1" runat="server" Effect="Fadein"/>
    /// <uc1:ucLazyLoad ID="ucLazyLoad1" runat="server"/>
    /// </example>
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
}