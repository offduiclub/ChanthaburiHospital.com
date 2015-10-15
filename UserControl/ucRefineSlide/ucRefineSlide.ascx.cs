using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class UserControl_ucRefineSlide_ucRefineSlide : System.Web.UI.UserControl
{
    #region Example
    /*
    DataTable dt = new DataTable();
    dt = clsSQL.Bind("SELECT Name,Photo,PhotoPreview FROM Gallery WHERE GalleryGroupUID=1 AND Active='1' ORDER BY Sort ", clsSQL.DBType.MySQL, "cs");
    ucRefineSlide1.Source = dt;
    ucRefineSlide1.Path = "/Upload/Gallery/";
    ucRefineSlide1.AutoPlay = true;
    ucRefineSlide1.Width = "400px";
    ucRefineSlide1.Height = "400px";
    ucRefineSlide1.Transition = UserControl_ucRefineSlide_ucRefineSlide.Transitions.fade;
    */
    #endregion
    public enum Transitions
    {
        fade, custom, random, cubeH, cubeV, sliceH, sliceV, slideH, slideV, scale, blockScale, kaleidoscope, fan, blindH, blindV
    }

    private Transitions _transition;
    public Transitions Transition
    {
        get 
        { 
            return _transition; 
        }
        set { _transition = value; }
    }

    private DataTable _source;
    public DataTable Source
    {
        get { return _source; }
        set { _source = value; }
    }

    private string _path;
    public string Path
    {
        get { return _path; }
        set { _path = value; }
    }

    private string _width = "100%";
    public string Width
    {
        get { return _width; }
        set { _width = value; }
    }

    private string _height = "100%";
    public string Height
    {
        get { return _height; }
        set { _height = value; }
    }

    private bool _autoPlay = true;
    public bool AutoPlay
    {
        get { return _autoPlay; }
        set { _autoPlay = value; }
    }

    public StringBuilder strSlide = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (_source != null && _source.Rows.Count > 0)
            {
                BindGallery();
            }
        }
    }

    private void BindGallery()
    {
        int i;
        bool errorCheck = false;
        string fieldName = "Name";
        string fieldPhoto = "Photo";

        #region Error Check
        if (!_source.Columns.Contains(fieldName))
        {
            lblDefault.Text += "<div>ไม่พบคอลัม " + fieldName + " ใน Source ที่ส่งมา</div>";
            errorCheck = true;
        }
        if (!_source.Columns.Contains(fieldPhoto))
        {
            lblDefault.Text += "<div>ไม่พบคอลัม " + fieldPhoto + " ใน Source ที่ส่งมา</div>";
            errorCheck = true;
        }
        #endregion

        if (!errorCheck)
        {
            strSlide.Append("<div style='height:" + _height + ";width:" + _width + ";'>");
            strSlide.Append("<ul id='images' class='rs-slider'>");
            for (i = 0; i < _source.Rows.Count; i++)
            {
		        strSlide.Append("<li class='group'>");
			    strSlide.Append("<a href='#'>");
                strSlide.Append("<img src='" + _path + _source.Rows[i][fieldPhoto].ToString() + "' alt='" + _source.Rows[i][fieldName].ToString() + "' />");
			    strSlide.Append("</a>");
		        strSlide.Append("</li>");
            }
            strSlide.Append("</ul>");
            strSlide.Append("</div>");
            strSlide.Append("<div style='clear:both;'></div>");
        }
    }
}