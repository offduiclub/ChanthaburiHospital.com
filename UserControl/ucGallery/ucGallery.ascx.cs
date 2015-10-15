using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ucGallery : System.Web.UI.UserControl
{
    #region Example
    /*
    DataTable dt=new DataTable();
    clsSQL clsSQL = new clsSQL();
    dt = clsSQL.Bind("SELECT Name,Detail,Photo,PhotoPreview FROM PhotoGallery", clsSQL.DBType.SQLServer, "cs");
    
    ucGallery1.Source = dt;
    ucGallery1.SourceFieldName = "Name";
    ucGallery1.SourceFieldDetail = "Detail";
    ucGallery1.SourceFieldPhoto = "Photo";
    ucGallery1.SourceFieldPhotoPreview = "PhotoPreview";
    ucGallery1.PathPhoto = "";
    ucGallery1.Width = "auto";
    ucGallery1.Height = "auto";
    ucGallery1.PreviewWidth = "150px";
    ucGallery1.PreviewHeight = "150px";
    ucGallery1.LabelHeight = "35px";
    ucGallery1.RepeatColumns = 4;
    ucGallery1.Visible = true;
    ucGallery1.Enable = true;
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
    private string _sourceFieldDetail="Detail";
    public string SourceFieldDetail
    {
        get { return _sourceFieldDetail; }
        set { _sourceFieldDetail = value; }
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
    private string _width = "auto";
    public string Width
    {
        get { return _width; }
        set { _width = value; }
    }
    private string _height = "auto";
    public string Height
    {
        get { return _height; }
        set { _height = value; }
    }
    private string _previewWidth="150px";
    public string PreviewWidth
    {
        get { return _previewWidth; }
        set { _previewWidth = value; }
    }
    private string _previewHeight="auto";
    public string PreviewHeight
    {
        get { return _previewHeight; }
        set { _previewHeight = value; }
    }
    private string _labelHeight="auto";
    public string LabelHeight
    {
        get { return _labelHeight; }
        set { _labelHeight = value; }
    }
    private int _repeatColumns = 4;
    public int RepeatColumns
    {
        get { return _repeatColumns; }
        set { _repeatColumns = value; }
    }
    private bool _enable = true;
    public bool Enable
    {
        get { return _enable; }
        set { _enable = value; }
    }
    private bool _visible = true;
    public bool Visible
    {
        get { return _visible; }
        set { _visible = value; }
    }
    #endregion
    #region Global Variable
    clsDefault clsDefault = new clsDefault();
    clsSQL clsSQL = new clsSQL();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (_enable)
            {
                GalleryBuilder();
            }
        }
    }

    private void GalleryBuilder()
    {
        #region Variable
        bool errorCheck = false;
        #endregion
        #region Data Builder
        if (_source != null && _source.Rows.Count > 0)
        {
            lblGallery.Text = "";
            #region Error Check
            if (!_source.Columns.Contains(_sourceFieldName) && _visible)
            {
                lblGallery.Text += "<div>ไม่พบคอลัม " + _sourceFieldName + " ใน Source ที่ส่งมา</div>";
                errorCheck = true;
            }
            if (!_source.Columns.Contains(_sourceFieldDetail) && _visible)
            {
                lblGallery.Text += "<div>ไม่พบคอลัม " + _sourceFieldDetail + " ใน Source ที่ส่งมา</div>";
                errorCheck = true;
            }
            if (!_source.Columns.Contains(_sourceFieldPhoto) && _visible)
            {
                lblGallery.Text += "<div>ไม่พบคอลัม " + _sourceFieldPhoto + " ใน Source ที่ส่งมา</div>";
                errorCheck = true;
            }
            if (!_source.Columns.Contains(_sourceFieldPhotoPreview) && _visible)
            {
                lblGallery.Text += "<div>ไม่พบคอลัม " + _sourceFieldPhotoPreview + " ใน Source ที่ส่งมา</div>";
                errorCheck = true;
            }
            if (errorCheck)
            {
                return;
            }
            #endregion

            dlGallery.RepeatColumns = _repeatColumns;
            dlGallery.DataSource = _source;
            dlGallery.DataBind();
            dlGallery.Visible = _visible;
        }
        else
        {
            if (_visible)
            {
                lblGallery.Text = clsDefault.AlertMessageColor("ไม่พบข้อมูลใน Source", clsDefault.AlertType.Warn);
            }
        }
        #endregion
    }
}