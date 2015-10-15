using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SQLInsert : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            clsSQL clsSQL = new clsSQL();
            clsSQL.Execute(
                "INSERT INTO Content(UID,LanguageUID,Name,Detail,Content,CWhen,CUser,MWhen,MUser,Sort,Active)" +
                "VALUES(17,1,'HospitalNetwork','กลุ่มโรงพยาบาลเครือข่าย','กลุ่มโรงพยาบาลเครือข่าย',GETDATE(),0,GETDATE(),0,0,'1');" +
                "INSERT INTO Content(UID,LanguageUID,Name,Detail,Content,CWhen,CUser,MWhen,MUser,Sort,Active)" +
                "VALUES(18,2,'HospitalNetwork','กลุ่มโรงพยาบาลเครือข่าย','กลุ่มโรงพยาบาลเครือข่าย',GETDATE(),0,GETDATE(),0,0,'1');",
                clsSQL.DBType.SQLServer, "cs");
        }
    }
}