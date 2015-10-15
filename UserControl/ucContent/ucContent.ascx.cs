using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class ucContent : System.Web.UI.UserControl
{
    #region Example
    /*
    <uc2:ucContent ID="ucContentDefault" runat="server" ContentName="AboutHospital" ModalHeight="95%" ModalWidth="80%" ModalRefreshOnClose="false"/>
    */
    #endregion
    #region Property
    private string _contentName;
    public string ContentName
    {
        get { return _contentName; }
        set { _contentName = value; }
    }
    private string _modalWidth="800px";
    public string ModalWidth
    {
        get { return _modalWidth; }
        set { _modalWidth = value; }
    }
    private string _modalHeight="90%";
    public string ModalHeight
    {
        get { return _modalHeight; }
        set { _modalHeight = value; }
    }
    private bool _modalRefreshOnClose=false;
    public bool ModalRefreshOnClose
    {
        get { return _modalRefreshOnClose; }
        set { _modalRefreshOnClose = value; }
    }
    #endregion
    #region Database Config
    string parameterChar = "@"; //SQLServer=@ MySQL=?
    clsSQL.DBType dbType = clsSQL.DBType.SQLServer;
    string cs = "cs";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && !string.IsNullOrEmpty(_contentName))
        {
            ContentBuilder();
        }
    }

    private void ContentBuilder()
    {
        #region Variable
        StringBuilder strSQL = new StringBuilder();
        StringBuilder strScript = new StringBuilder();
        DataTable dt = new DataTable();
        bool foundChecker = false;
        int dtIndex=0;

        clsSQL clsSQL = new clsSQL();
        clsLanguage clsLanguage = new clsLanguage();
        clsSecurity clsSecurity = new clsSecurity();
        #endregion

        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("Language.UID LanguageUID,");
        strSQL.Append("Language.Name LanguageName,");
        strSQL.Append("Content.UID,");
        strSQL.Append("Content.Name,");
        strSQL.Append("Content.Detail,");
        strSQL.Append("Content.Content ");
        strSQL.Append("FROM ");
        strSQL.Append("Content ");
        strSQL.Append("INNER JOIN Language ON Content.LanguageUID=Language.UID ");
        strSQL.Append("AND Language.Active='1' ");
        strSQL.Append("WHERE ");
        strSQL.Append("Content.Active='1' ");
        strSQL.Append("AND Content.Name='"+_contentName.Trim()+"' ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("Language.Sort ASC");
        #endregion

        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);

        if (dt != null && dt.Rows.Count > 0)
        {
            strScript.Append("<div class='" + (clsSecurity.LoginChecker("Admin") ? "dvContent" : "dvContentNormal") + "'>");

            #region Find Language
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["LanguageName"].ToString() == clsLanguage.LanguageCurrent)
                {
                    foundChecker = true;
                    dtIndex=i;

                    #region Content Builder
                    if (dt.Rows[i]["Content"] != DBNull.Value)
                    {
                        strScript.Append(dt.Rows[i]["Content"].ToString());
                    }
                    #endregion

                    break;
                }
            }
            #endregion
            #region Default Builder
            if (!foundChecker)
            {
                if (dt.Rows[0]["Content"] != DBNull.Value)
                {
                    strScript.Append(dt.Rows[0]["Content"].ToString());
                }
            }
            #endregion
            #region Admin Builder
            if (clsSecurity.LoginChecker("Admin"))
            {
                strScript.Append("<div class='dvContentMenu'>");
                strScript.Append("<a href='/Management/ContentManage.aspx?id="+dt.Rows[dtIndex]["UID"].ToString()+"&command=edit' ");
                strScript.Append("title='แก้ไขข้อมูล' ");
                if (_modalRefreshOnClose)
                {
                    strScript.Append("class='cbIFrameRefreshOnClose'");
                }
                else
                {
                    strScript.Append("class='cbIFrame'");
                }
                strScript.Append(">");
                strScript.Append("<span class='Icon16 Edit' />");
                strScript.Append("</a>");
                strScript.Append("</div>");
            }
            #endregion

            strScript.Append("</div>");
            lblContent.Text = strScript.ToString();
        }
    }
}