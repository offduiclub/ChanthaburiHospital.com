using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CheckupResult : System.Web.UI.Page
{
    DBClassDataContext db = new DBClassDataContext();
    DataSet ds = new DataSet();
    DataTable dtPatient = new DataTable();
    CheckupService.Service chkService = new CheckupService.Service();
    ucTabs.Tab tab = new ucTabs.Tab();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btFind_Click(object sender, EventArgs e)
    {
        //ถ้าค้นหาด้วยเงื่อนไข HN
        if (rdbHN.Checked == true)
        {
            gvPatient.DataSource = chkService.PatientInformationByHN(txtHN.Text.Trim());
        }
        //ถ้าค้นหาด้วยเงื่อนไขชื่อ-สกุล
        else
        {
            gvPatient.DataSource = chkService.PatientInformationName(txtConName.Text.Trim(), txtConSurname.Text.Trim());
        }
        gvPatient.DataBind();
    }

    protected void gvPatient_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "SelectPatient")
        {
            
        }
    }
    protected void gvPatient_PageIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void gvPatient_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["HN"] = gvPatient.SelectedRow.Cells[1].Text.ToString().Trim();
        Session["EN"] = gvPatient.SelectedRow.Cells[2].Text.ToString().Trim();
        Response.Redirect("/CheckupResult/");

    }

    protected void rdbName_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void rdbHN_CheckedChanged(object sender, EventArgs e)
    {

    }
}