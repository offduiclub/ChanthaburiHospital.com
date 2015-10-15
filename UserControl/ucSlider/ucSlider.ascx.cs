using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class UserControl_ucSlider : System.Web.UI.UserControl
{
    #region Example
    /*
        <uc2:ucSlider ID="ucSlider1" runat="server" Width="1000" Height="275"/>
    */
    #endregion
    #region Create Query
    /*
    CREATE TABLE Slider(
	    UID INT NOT NULL,
	
	    LanguageUID INT NOT NULL,
	    Photo VARCHAR(100) NOT NULL,
	    Name VARCHAR(200),
	    Detail VARCHAR(500),

	    CWhen DATETIME NOT NULL,
	    CUser INT NOT NULL,
	    MWhen DATETIME NOT NULL,
	    MUser INT NOT NULL,
	    Sort INT NOT NULL,
	    Active CHAR(1) NOT NULL,
    PRIMARY KEY(UID)
    );
    */
    #endregion
    #region Property
    private int _width = 560;
    private int _height = 215;
    private string _animation = "fade";
    private int _duration = 1000;

    public int Width
    {
        get
        {
            return _width;
        }
        set
        {
            _width = value;
        }
    }
    public int Height
    {
        get
        {
            return _height;
        }
        set
        {
            _height = value;
        }
    }
    public string Animation
    {
        get
        {
            return _animation;
        }
        set
        {
            _animation = value;
        }
    }
    public int Duration
    {
        get
        {
            return _duration;
        }
        set
        {
            _duration = value;
        }
    }
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
            SliderBuilder();
        }
    }

    private void SliderBuilder()
    {
        #region Variable
        clsDefault clsDefault = new clsDefault();
        clsSQL clsSQL = new clsSQL();
        clsLanguage clsLanguage = new clsLanguage();

        StringBuilder strSQL = new StringBuilder();
        StringBuilder strSliderItem = new StringBuilder();
        StringBuilder strOutput = new StringBuilder();
        StringBuilder strScript = new StringBuilder();
        DataTable dt = new DataTable();
        bool foundChecker = false;
        string languageDefault = "";
        #endregion

        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("Language.Name LanguageName,");
        strSQL.Append("Slider.Photo,");
        strSQL.Append("Slider.Name ");
        strSQL.Append("FROM ");
        strSQL.Append("Slider ");
        strSQL.Append("INNER JOIN Language ON ");
        strSQL.Append("Slider.LanguageUID=Language.UID ");
        strSQL.Append("AND Language.Active='1' ");
        strSQL.Append("WHERE ");
        strSQL.Append("Slider.Active='1' ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("Language.Sort ASC,Slider.Sort ASC");
        #endregion

        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);

        if (dt != null && dt.Rows.Count > 0)
        {
            #region Find Language
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0) languageDefault = dt.Rows[i]["LanguageName"].ToString();
                if (dt.Rows[i]["LanguageName"].ToString() == clsLanguage.LanguageCurrent)
                {
                    foundChecker = true;

                    strSliderItem.Append("<li>");
                    strSliderItem.Append("<img src='" + dt.Rows[i]["Photo"].ToString() + "' ");
                    strSliderItem.Append("alt='" + (dt.Rows[i]["Name"] != DBNull.Value ? dt.Rows[i]["Name"].ToString() : "") + "' ");
                    strSliderItem.Append("title='" + (dt.Rows[i]["Name"] != DBNull.Value ? dt.Rows[i]["Name"].ToString() : "") + "'/>");
                    strSliderItem.Append("</li>");
                }

            }
            #endregion
            #region Default Language
            if (!foundChecker)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["LanguageName"].ToString() == languageDefault)
                    {
                        strSliderItem.Append("<li>");
                        strSliderItem.Append("<img src='" + dt.Rows[i]["Photo"].ToString() + "' ");
                        strSliderItem.Append("alt='" + (dt.Rows[i]["Name"] != DBNull.Value ? dt.Rows[i]["Name"].ToString() : "") + "' ");
                        strSliderItem.Append("title='" + (dt.Rows[i]["Name"] != DBNull.Value ? dt.Rows[i]["Name"].ToString() : "") + "'/>");
                        strSliderItem.Append("</li>");
                    }
                }
            }
            #endregion
            #region div Builder
            strOutput.Append("<div id='Slider' style='display:block;width:" + _width + ";height:" + _height + ";overflow:hidden;'>");
            strOutput.Append("<ul class='bjqs'>");
            strOutput.Append(strSliderItem.ToString());
            strOutput.Append("</ul>");
            strOutput.Append("</div>");
            #endregion
        }
        else
        {
            strOutput.Append("");
        }

        lblSlider.Text = strOutput.ToString();
    }
}