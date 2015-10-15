using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class Jobs : ucLanguage
{
    #region Global Variable
    public clsDefault clsDefault = new clsDefault();
    private clsSQL clsSQL = new clsSQL();
    private clsLanguage clsLanguage = new clsLanguage();
    #endregion
    #region Database Config
    string parameterChar = "@"; //SQLServer=@ MySQL=?
    clsSQL.DBType dbType = clsSQL.DBType.SQLServer;
    string cs = "cs";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region Language
            switch (clsLanguage.LanguageCurrent)
            {
                case "th-TH":
                    this.Title += " ร่วมงานกับเรา";
                    break;
                case "en-US":
                    this.Title += " Jobs";
                    break;
                default:
                    break;
            }
            #endregion
            #region Procedure
            DefaultBuilder();
            #endregion
        }
    }

    private void DefaultBuilder()
    {
        #region Variable
        var strSQL = new StringBuilder();
        var dt = new DataTable();
        #endregion
        #region Procedure
        #region SQLQuery
        strSQL.Append("SELECT ");
        strSQL.Append("UID,Name,Detail ");
        strSQL.Append("FROM ");
        strSQL.Append("Jobs ");
        strSQL.Append("WHERE ");
        strSQL.Append("StatusFlag='A' ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("Sort ASC;");
        #endregion
        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        if (dt != null && dt.Rows.Count > 0)
        {
            gvDefault.DataSource = dt;
            gvDefault.DataBind();
        }
        else
        {

        }
        #endregion
    }
}