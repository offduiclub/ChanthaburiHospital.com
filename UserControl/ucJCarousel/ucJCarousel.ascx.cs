using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class ucJCarousel : System.Web.UI.UserControl
{
    #region Example
    /*
    DataTable dt=new DataTable();
    clsSQL clsSQL = new clsSQL();
    dt = clsSQL.Bind("SELECT Name,Photo,PhotoPreview FROM PhotoGallery", clsSQL.DBType.SQLServer, "cs");

    ucJCarousel1.Source = dt;
    ucJCarousel1.SourceFieldName = "Name";
    ucJCarousel1.SourceFieldPhoto = "Photo";
    ucJCarousel1.SourceFieldPhotoPreview = "PhotoPreview";
    ucJCarousel1.PathPhoto = "";
    ucJCarousel1.AutoPlay = true;
    ucJCarousel1.Width = 600;
    ucJCarousel1.Height = 400;
    ucJCarousel1.NavigatorWidth = 240;
    ucJCarousel1.NavigatorHeight = 45;
    ucJCarousel1.Credit = "<a href='http://www.bkmhome.com'>www.bkmhome.com</a>";
    ucJCarousel1.Enable = true;
    ucJCarousel1.Visible = true;
    */
    #endregion
    #region Property
    private DataTable _source;
    public DataTable Source
    {
        get { return _source; }
        set { _source = value; }
    }
    private string _sourceFieldName = "Name";
    public string SourceFieldName
    {
        get { return _sourceFieldName; }
        set { _sourceFieldName = value; }
    }
    private string _sourceFieldPhoto = "Photo";
    public string SourceFieldPhoto
    {
        get { return _sourceFieldPhoto; }
        set { _sourceFieldPhoto = value; }
    }
    private string _sourceFieldPhotoPreview = "PhotoPreview";
    public string SourceFieldPhotoPreview
    {
        get { return _sourceFieldPhotoPreview; }
        set { _sourceFieldPhotoPreview = value; }
    }
    private string _pathPhoto = "";
    public string PathPhoto
    {
        get { return _pathPhoto; }
        set { _pathPhoto = value; }
    }
    private string _credit = "";
    public string Credit
    {
        get { return _credit; }
        set { _credit = value; }
    }
    private int _width = 620;
    public int Width
    {
        get { return _width; }
        set { _width = value; }
    }
    private int _height = 400;
    public int Height
    {
        get { return _height; }
        set { _height = value; }
    }
    private int _navigatorWidth = 340;
    public int NavigatorWidth
    {
        get { return _navigatorWidth; }
        set { _navigatorWidth = value; }
    }
    private int _navigatorHeight = 60;
    public int NavigatorHeight
    {
        get { return _navigatorHeight; }
        set { _navigatorHeight = value; }
    }
    private bool _autoPlay = true;
    public bool AutoPlay
    {
        get { return _autoPlay; }
        set { _autoPlay = value; }
    }
    private bool _enable=true;
    public bool Enable
    {
        get { return _enable; }
        set { _enable = value; }
    }
    private bool _visible=true;
    public bool Visible
    {
        get { return _visible; }
        set { _visible = value; }
    }
    #endregion
    #region Global Variable
    public StringBuilder strGallery = new StringBuilder();
    private StringBuilder strPhoto = new StringBuilder();
    private StringBuilder strPhotoPreview = new StringBuilder();
    #endregion

    //public ucJCarousel(DataTable source, string sourceFieldName = "Name", string sourceFieldPhoto = "Photo", string sourceFieldPhotoPreview = "PhotoPreview", string pathPhoto = "/Upload/Gallery/", bool autoPlay = true, int width = 620, int height = 400, int navigatorWidth = 340, int navigatorHeight = 60, string credit = "")
    //{
    //    _source = source;
    //    _sourceFieldName = sourceFieldName;
    //    _sourceFieldPhoto = sourceFieldPhoto;
    //    _sourceFieldPhotoPreview = sourceFieldPhotoPreview;
    //    _pathPhoto = pathPhoto;
    //    _width = width;
    //    _height = height;
    //    _navigatorWidth = navigatorWidth;
    //    _navigatorHeight = navigatorHeight;
    //    _autoPlay = autoPlay;
    //    _credit = credit;
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (_enable)
            {
                if (_source != null && _source.Rows.Count > 0)
                {
                    CarouselBuilder();
                }
            }
        }
    }

    private void CarouselBuilder()
    {
        int i;
        bool errorCheck = false;

        #region Error Check
        if (!_source.Columns.Contains(_sourceFieldName) && _visible)
        {
            lblDefault.Text += "<div>ไม่พบคอลัม " + _sourceFieldName + " ใน Source ที่ส่งมา</div>";
            errorCheck = true;
        }
        if (!_source.Columns.Contains(_sourceFieldPhoto) && _visible)
        {
            lblDefault.Text += "<div>ไม่พบคอลัม " + _sourceFieldPhoto + " ใน Source ที่ส่งมา</div>";
            errorCheck = true;
        }
        if (!_source.Columns.Contains(_sourceFieldPhotoPreview) && _visible)
        {
            lblDefault.Text += "<div>ไม่พบคอลัม " + _sourceFieldPhotoPreview + " ใน Source ที่ส่งมา</div>";
            errorCheck = true;
        }
        #endregion

        if (!errorCheck)
        {
            for (i = 0; i < _source.Rows.Count; i++)
            {
                strPhoto.Append("<li>");
                strPhoto.Append("<img src='" + _pathPhoto + _source.Rows[i][_sourceFieldPhoto].ToString() + "' width='" + _width.ToString() + "' ");
                strPhoto.Append("alt='" + _source.Rows[i][_sourceFieldName].ToString() + "' title='" + _source.Rows[i][_sourceFieldName].ToString() + "'>");
                strPhoto.Append("</li>");

                strPhotoPreview.Append("<li>");
                strPhotoPreview.Append("<img src='" + _pathPhoto + _source.Rows[i][_sourceFieldPhotoPreview].ToString() + "' width='50' alt=''>");
                strPhotoPreview.Append("</li>");
            }

            strGallery.Append("<div class='connected-carousels'>");
            strGallery.Append("<div class='stage'>");
            strGallery.Append("<div class='carousel carousel-stage'>");
            strGallery.Append("<ul>");
            strGallery.Append(strPhoto.ToString());
            strGallery.Append("</ul>");
            strGallery.Append("</div>");
            strGallery.Append("<p class='photo-credits'>");
            strGallery.Append(_credit);
            strGallery.Append("</p>");
            strGallery.Append("<a href='#' class='prev prev-stage'><span>&lsaquo;</span></a>");
            strGallery.Append("<a href='#' class='next next-stage'><span>&rsaquo;</span></a>");
            strGallery.Append("</div>");
            strGallery.Append("<div class='navigation'>");
            strGallery.Append("<a href='#' class='prev prev-navigation'>‹</a>");
            strGallery.Append("<a href='#' class='next next-navigation'>›</a>");
            strGallery.Append("<div class='carousel carousel-navigation'>");
            strGallery.Append("<ul>");
            strGallery.Append(strPhotoPreview.ToString());
            strGallery.Append("</ul>");
            strGallery.Append("</div>");
            strGallery.Append("</div>");
            strGallery.Append("</div>");
        }
    }
}