using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CheckupCondition : System.Web.UI.Page
{
    DBClassDataContext db = new DBClassDataContext();
    DataSet ds = new DataSet();
    DataTable dtPatient = new DataTable();
    CheckupService.Service chkService = new CheckupService.Service();
    ucTabs.Tab tab = new ucTabs.Tab();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindPatient();
            BindTabs();
        }
    }
    private void BindPatient()
    {
        string HN = Session["HN"].ToString();
        string EN = Session["EN"].ToString();
        ds = chkService.PatientInformation(HN, EN);
        //Bind ข้อมูลพื้นฐานของคนไข้
        dtPatient = ds.Tables["patient"];
        if (dtPatient.Rows.Count > 0 && dtPatient != null)
        {
            lblHN.Text = dtPatient.Rows[0]["HN"].ToString();
            lblFullName.Text = dtPatient.Rows[0]["Name"].ToString() + "  " + dtPatient.Rows[0]["LastName"].ToString();
            lblAddress.Text = dtPatient.Rows[0]["Address"].ToString();
            lblNo.Text = dtPatient.Rows[0]["NO"].ToString();
            lblEmpID.Text = dtPatient.Rows[0]["EMPID"].ToString();
            lblLine.Text = dtPatient.Rows[0]["Line"].ToString();
            lblDiv.Text = dtPatient.Rows[0]["DIV"].ToString();
            lblDep.Text = dtPatient.Rows[0]["DEP"].ToString();
            lblDOE.Text = dtPatient.Rows[0]["DOE"].ToString();
            lblSex.Text = dtPatient.Rows[0]["SEX"].ToString();
            lblAge.Text = dtPatient.Rows[0]["Age"].ToString();
        }
    }
    private void BindTabs()
    {
        ucTabs1.UID = "Tabs1";
        ucTabs1.Effect = ucTabs.Effects.scaleUp;

        //PE
        BindPE();

        //EKG
        BindEKG();

        //Xray
        BindXray();

        //Audiogram
        BindAudiogram();

        //Titmus
        BindTitmus();

        //Spiro
        BindSpiro();

        //Lab
        BindLab();
    }

    private void BindLab()
    {
        DataTable dtLab = new DataTable();
        dtLab = ds.Tables["VW_LabResultComplete"];
        FindLabAbNormal(dtLab);
        tab = new ucTabs.Tab();
        tab.Name = "LAB";
        tab.Content = "ผลการตรวจทางห้องปฏิบัติการ (LAB)";
        gvLab.DataSource = dtLab;
        gvLab.DataBind();
        ucTabs1.Tabs.Add(tab);
    }
    private void BindSpiro()
    {
        DataTable dtSpiro = new DataTable();
        dtSpiro = ds.Tables["tbSpiro"];
        tab = new ucTabs.Tab();
        tab.Name = "Spiro";
        tab.Content = @"<p><h5><font color='#66ccff'>การตรวจสมรรถภาพการทำงานปอด (Spirometry)</font></h5></p><br/>
<table cellpadding='0' cellspacing='0' align='center' 
                                        width='780px'>
                                    <tr>
                                        <td style='width:480px;background-color:Menu;'>
                                            <p>
                                            </p>
                                        </td>
                                        <td style='width:100px;background-color:Menu;'>
                                            <p>
                                                <b>ค่าที่วัดได้</b></p>
                                        </td>
                                        <td style='width:100px;background-color:Menu;'>
                                            <p>
                                                <b>ค่าที่ควรได้</b></p>
                                        </td>
                                        <td style='width:100px;background-color:Menu;'>
                                            <p>
                                                <b>ร้อยละ(%)</b></p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style='text-align:left;'>
                                            <p>
                                                <b>FVC (lit.)</b></p>
                                        </td>
                                        <td>
                                            <p>
                                                @FVC</p>
                                        </td>
                                        <td>
                                            <p>
                                                @FVCPred</p>
                                        </td>
                                        <td>
                                            <p>
                                                @FVCPer</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style='text-align:left;'>
                                            <p>
                                                <b>FEV1 (lit.)</b></p>
                                        </td>
                                        <td>
                                            <p>
                                                @FEV1</p>
                                        </td>
                                        <td>
                                            <p>
                                                @FEV1Pred</p>
                                        </td>
                                        <td>
                                            <p>
                                                @FEV1Per</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style='text-align:left;'>
                                            <p>
                                                <b>FEV1/FVC (%)</b></p>
                                        </td>
                                        <td>
                                            <p>
                                                @FEV1FVC</p>
                                        </td>
                                        <td>
                                            <p>
                                                @FEV1FVCPred</p>
                                        </td>
                                        <td>
                                            <p>
                                                @FEV1FVCPer</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style='text-align:left;'>
                                            <p>
                                                <b>FEF25 - 75% (lit./sec.)</b></p>
                                        </td>
                                        <td>
                                            <p>
                                                @FEF25</p>
                                        </td>
                                        <td>
                                            <p>
                                                @FEF25Pred</p>
                                        </td>
                                        <td>
                                            <p>
                                                @FEF25Per</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan='4' style='text-align:left;'>
                                            <p>
                                                @SpiroDetail</p><br /><br />
                                        </td>
                                    </tr>
                                </table>";
        if (dtSpiro.Rows.Count > 0 && dtSpiro != null)
        {
            tab.Content = tab.Content.Replace("@FEF25Per", dtSpiro.Rows[0]["FEF25Per"].ToString().Trim());
            tab.Content = tab.Content.Replace("@FEF25Pred", dtSpiro.Rows[0]["FEF25Pred"].ToString().Trim());
            tab.Content = tab.Content.Replace("@FEF25", dtSpiro.Rows[0]["FEF25"].ToString().Trim());
            tab.Content = tab.Content.Replace("@FEV1FVCPer", dtSpiro.Rows[0]["FEV1FVCPer"].ToString().Trim());
            tab.Content = tab.Content.Replace("@FEV1FVCPred", dtSpiro.Rows[0]["FEV1FVCPred"].ToString().Trim());
            tab.Content = tab.Content.Replace("@FEV1FVC", dtSpiro.Rows[0]["FEV1FVC"].ToString().Trim());
            tab.Content = tab.Content.Replace("@FEV1Per", dtSpiro.Rows[0]["FEV1Per"].ToString().Trim());
            tab.Content = tab.Content.Replace("@FEV1Pred", dtSpiro.Rows[0]["FEV1Pred"].ToString().Trim());
            tab.Content = tab.Content.Replace("@FEV1", dtSpiro.Rows[0]["FEV1"].ToString().Trim());
            tab.Content = tab.Content.Replace("@FVCPer", dtSpiro.Rows[0]["FVCPer"].ToString().Trim());
            tab.Content = tab.Content.Replace("@FVCPred", dtSpiro.Rows[0]["FVCPred"].ToString().Trim());
            tab.Content = tab.Content.Replace("@FVC", dtSpiro.Rows[0]["FVC"].ToString().Trim());
            tab.Content = tab.Content.Replace("@SpiroDetail", dtSpiro.Rows[0]["SpiroDetail"].ToString().Trim());
        }
        else
        {
            tab.Content = tab.Content.Replace("@FEF25Per", "");
            tab.Content = tab.Content.Replace("@FEF25Pred", "");
            tab.Content = tab.Content.Replace("@FEF25", "");
            tab.Content = tab.Content.Replace("@FEV1FVCPer", "");
            tab.Content = tab.Content.Replace("@FEV1FVCPred", "");
            tab.Content = tab.Content.Replace("@FEV1FVC", "");
            tab.Content = tab.Content.Replace("@FEV1Per", "");
            tab.Content = tab.Content.Replace("@FEV1Pred", "");
            tab.Content = tab.Content.Replace("@FEV1", "");
            tab.Content = tab.Content.Replace("@FVCPer", "");
            tab.Content = tab.Content.Replace("@FVCPred", "");
            tab.Content = tab.Content.Replace("@FVC", "");
            tab.Content = tab.Content.Replace("@SpiroDetail", "");
        }
        
        ucTabs1.Tabs.Add(tab);
    }
    private void BindTitmus()
    {
        tab = new ucTabs.Tab();
        tab.Name = "Vision Test";
        tab.Content = "ผลการตรวจสมรรถภาพการมองเห็น (Vision Test)";
        ucTabs1.Tabs.Add(tab);
    }
    private void BindAudiogram()
    {
        DataTable dtAudio = new DataTable();
        dtAudio = ds.Tables["tbAudio"];
        tab = new ucTabs.Tab();
        tab.Name = "Audiogram";
        tab.Content = @"
                                    <p><h5><font color='#66ccff'>ผลการตรวจการได้ยิน (Annual Check Up Audiogram)</font></h5></p><br/>
                                    <table cellpadding='0' cellspacing='0' align='center' 
                                        width='780'>
                                        <tr>
                                            <td style='background-color:Menu;'>
                                                <p><b>ความถี่ (Hz.)</b></p></td>
                                            <td style='background-color:Menu;'>
                                                <p><b>500</b></p></td>
                                            <td style='background-color:Menu;'>
                                                <p><b>1000</b></p></td>
                                            <td style='background-color:Menu;'>
                                                <p><b>2000</b></p></td>
                                            <td style='background-color:Menu;'>
                                                <p><b>3000</b></p></td>
                                            <td style='background-color:Menu;'>
                                                <p><b>4000</b></p></td>
                                            <td style='background-color:Menu;'>
                                                <p><b>6000</b></p></td>
                                            <td style='background-color:Menu;'>
                                                <p><b>8000</b></p></td>
                                        </tr>
                                        <tr>
                                            <td style='background-color:Menu;'>
                                                <p><b>หูขวา (dB)</b></p></td>
                                            <td>
                                                <p>@R500</p>
                                            </td>
                                            <td>
                                                <p>@R1000</p>
                                            </td>
                                            <td>
                                                <p>@R2000</p>
                                            </td>
                                            <td>
                                                <p>@R3000</p>
                                            </td>
                                            <td>
                                                <p>@R4000</p>
                                            </td>
                                            <td>
                                                <p>@R6000</p>
                                            </td>
                                            <td>
                                                <p>@R8000</p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='background-color:Menu;'>
                                                <p><b>หูซ้าย (dB)</b></p></td>
                                            <td>
                                                <p>@L500</p>
                                            </td>
                                            <td>
                                                <p>@L1000</p>
                                            </td>
                                            <td>
                                                <p>@L2000</p>
                                            </td>
                                            <td>
                                                <p>@L3000</p>
                                            </td>
                                            <td>
                                                <p>@L4000</p>
                                            </td>
                                            <td>
                                                <p>@L6000</p>
                                            </td>
                                            <td>
                                                <p>@L8000</p>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table cellpadding='0' cellspacing='0' class='style1'>
                                        <tr class='GridViewSubHeader'>
                                            <td>
                                                <p><b>ผลการตรวจหูขวา</b></p></td>
                                            <td>
                                                <p><b>ผลการตรวจหูซ้าย</b></p></td>
                                        </tr>
                                        <tr>
                                            <td style='text-align: left; padding-left: 10;'>
                                                <p><b>ค่าเฉลี่ย 500 - 3000 Hz</b> =&nbsp;@R5003000Avg
                                                <br />
                                                <b>ค่าเฉลี่ย 4000 - 6000 Hz</b> =&nbsp;@R40006000Avg<br />
                                                <br />@RightEarDetail</p><br /><br />
                                            </td>
                                            <td style='text-align: left; padding-left: 10;'>
                                                <p><b>ค่าเฉลี่ย 500 - 3000 Hz</b> = &nbsp;@L5003000Avg
                                                <br />
                                               <b>ค่าเฉลี่ย 4000 - 6000 Hz</b> =&nbsp;@L40006000Avg<br />
                                                <br />@LeftEarDetail</p><br /><br />
                                            </td>
                                        </tr>
                                        <tr class='GridViewSubHeader'>
                                            <td colspan='2'>
                                                <p><b>คำแนะนำผลตรวจการได้ยิน (Audiogram Suggestion))</b></p></td>
                                        </tr>
                                        <tr>
                                            <td colspan='2'><p>@RecomEarDetail</p><br /><br />
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    
                                ";
        if (dtAudio.Rows.Count > 0 && dtAudio != null)
        {
            int R500 = Convert.ToInt32(dtAudio.Rows[0]["R500"]);
            int R1000 = Convert.ToInt32(dtAudio.Rows[0]["R1000"]);
            int R2000 = Convert.ToInt32(dtAudio.Rows[0]["R2000"]);
            int R3000 = Convert.ToInt32(dtAudio.Rows[0]["R3000"]);
            int R4000 = Convert.ToInt32(dtAudio.Rows[0]["R4000"]);
            int R6000 = Convert.ToInt32(dtAudio.Rows[0]["R6000"]);
            int R8000 = Convert.ToInt32(dtAudio.Rows[0]["R8000"]);
            int L500 = Convert.ToInt32(dtAudio.Rows[0]["L500"]);
            int L1000 = Convert.ToInt32(dtAudio.Rows[0]["L1000"]);
            int L2000 = Convert.ToInt32(dtAudio.Rows[0]["L2000"]);
            int L3000 = Convert.ToInt32(dtAudio.Rows[0]["L3000"]);
            int L4000 = Convert.ToInt32(dtAudio.Rows[0]["L4000"]);
            int L6000 = Convert.ToInt32(dtAudio.Rows[0]["L6000"]);
            int L8000 = Convert.ToInt32(dtAudio.Rows[0]["L8000"]);
            int R5003000Avg = (R500 + R1000 + R2000 + R3000)/4;
            int R400060000Avg = (R4000 + R6000)/2;
            int L5003000Avg = (L500 + L1000 + L2000 + L3000)/4;
            int L40006000Avg = (L4000 + L6000)/2;

            tab.Content = tab.Content.Replace("@L5003000Avg", L5003000Avg.ToString());
            tab.Content = tab.Content.Replace("@L40006000Avg", L40006000Avg.ToString());
            tab.Content = tab.Content.Replace("@R5003000Avg", R5003000Avg.ToString());
            tab.Content = tab.Content.Replace("@R40006000Avg", R400060000Avg.ToString());
            tab.Content = tab.Content.Replace("@R500", R500.ToString());
            tab.Content = tab.Content.Replace("@R1000", R1000.ToString());
            tab.Content = tab.Content.Replace("@R2000", R2000.ToString());
            tab.Content = tab.Content.Replace("@R3000", R3000.ToString());
            tab.Content = tab.Content.Replace("@R4000", R4000.ToString());
            tab.Content = tab.Content.Replace("@R6000", R6000.ToString());
            tab.Content = tab.Content.Replace("@R8000", R8000.ToString());
            tab.Content = tab.Content.Replace("@L500", L500.ToString());
            tab.Content = tab.Content.Replace("@L1000", L1000.ToString());
            tab.Content = tab.Content.Replace("@L2000", L2000.ToString());
            tab.Content = tab.Content.Replace("@L3000", L3000.ToString());
            tab.Content = tab.Content.Replace("@L4000", L4000.ToString());
            tab.Content = tab.Content.Replace("@L6000", L6000.ToString());
            tab.Content = tab.Content.Replace("@L8000", L8000.ToString());
            tab.Content = tab.Content.Replace("@RightEarDetail", dtAudio.Rows[0]["RightEarDetail"].ToString().Trim());
            tab.Content = tab.Content.Replace("@LeftEarDetail", dtAudio.Rows[0]["LeftEarDetail"].ToString().Trim());
            tab.Content = tab.Content.Replace("@RecomEarDetail", dtAudio.Rows[0]["RecomEarDetail"].ToString().Trim());
        }
        else
        {
            tab.Content = tab.Content.Replace("@R500", "");
            tab.Content = tab.Content.Replace("@R1000", "");
            tab.Content = tab.Content.Replace("@R2000", "");
            tab.Content = tab.Content.Replace("@R3000", "");
            tab.Content = tab.Content.Replace("@R4000", "");
            tab.Content = tab.Content.Replace("@R6000", "");
            tab.Content = tab.Content.Replace("@R8000", "");
            tab.Content = tab.Content.Replace("@L500", "");
            tab.Content = tab.Content.Replace("@L1000", "");
            tab.Content = tab.Content.Replace("@L2000", "");
            tab.Content = tab.Content.Replace("@L3000", "");
            tab.Content = tab.Content.Replace("@L4000", "");
            tab.Content = tab.Content.Replace("@L6000", "");
            tab.Content = tab.Content.Replace("@L8000", "");
            tab.Content = tab.Content.Replace("@R5003000Avg", "");
            tab.Content = tab.Content.Replace("@R40006000Avg", "");
            tab.Content = tab.Content.Replace("@RightEarDetail", "");
            tab.Content = tab.Content.Replace("@L5003000Avg", "");
            tab.Content = tab.Content.Replace("@L40006000Avg", "");
            tab.Content = tab.Content.Replace("@LeftEarDetail", "");
            tab.Content = tab.Content.Replace("@RecomEarDetail", "");
        }
        ucTabs1.Tabs.Add(tab);
    }
    private void BindXray()
    {
        DataTable dtXray = new DataTable();
        dtXray = ds.Tables["tblXray"];
        tab = new ucTabs.Tab();
        tab.Name = "X-Ray";
        tab.Content = @"<p><h5><font color='#66ccff'>ผลการตรวจเอ็กเรย์ปอดและทรวงอก (Chest X-Ray)</font></h5></p><br/>
                        <p>@InfoEN<br /><br />@InfoTH</p>";
        if (dtXray.Rows.Count > 0 && dtXray != null)
        {
            tab.Content = tab.Content.Replace("@InfoEN", dtXray.Rows[0]["InfoEN"].ToString().Trim());
            tab.Content = tab.Content.Replace("@InfoTH", dtXray.Rows[0]["InfoTH"].ToString().Trim());
        }
        else
        {
            tab.Content = tab.Content.Replace("@InfoEN", "None");
            tab.Content = tab.Content.Replace("@InfoTH", "");
        }
        ucTabs1.Tabs.Add(tab);
    }
    private void BindPE()
    {
        DataTable dtVitalSign = new DataTable();
        dtVitalSign = ds.Tables["tbVitalSign"];
        tab = new ucTabs.Tab();
        tab.Name = "PE";
        tab.Content = @"<p><h5><font color='#66ccff'>ผลการตรวจร่างกายโดยแพทย์ (Physical Examination)</font></h5></p><br/>
                                    <table cellpadding='0' cellspacing='0' align='center' 
                                        width='780'>
                                        <tr class='GridViewSubHeader'>
                                            <td style='text-align: left; padding-left: 10;'>
                                                <p><b>ส่วนสูง (Height (cms)) :</b>&nbsp;@Height
                                                &nbsp;&nbsp; <b>น้ำหนัก (Weight (kgs)) :</b>&nbsp;@Weight
                                                &nbsp;&nbsp;&nbsp; 
                                                <b>BMI :</b>
                                                &nbsp;@BMI</p>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='text-align: left; padding-left: 10;'>
                                                <p>@BMIDetail</p><br/><br/>
                                            </td>
                                        </tr>
                                        <tr class='GridViewSubHeader'>
                                            <td style='text-align: left; padding-left: 10;'>
                                                <p><b>เส้นรอบเอว (Waist) : </b>
                                                &nbsp;@Waist</p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='text-align: left; padding-left: 10;'>
                                                <p>@WaistDetail</p><br/><br/>
                                            </td>
                                        </tr>
                                        <tr class='GridViewSubHeader'>
                                            <td style='text-align: left; padding-left: 10;'>
                                                <p><b>ความดันโลหิต (Blood Pressure (mm. Hg)) : </b>&nbsp;
                                                @BPSys&nbsp;
                                                /&nbsp;@BPDias</p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='text-align: left; padding-left: 10;'>
                                                <p>@BPDetail</p><br/><br/>
                                            </td>
                                        </tr>
                                        <tr class='GridViewSubHeader'>
                                            <td style='text-align: left; padding-left: 10;'>
                                                <p><b>ชีพจร (Pulse rate (bpm)) : </b>
                                                &nbsp;@Pulse</p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style='text-align: left; padding-left: 10;'>
                                                <p>@PulseDetail</p><br/><br/>
                                            </td>
                                        </tr>
                                        <tr class='GridViewSubHeader'>
                                            <td style='text-align: left; padding-left: 10;'>
                                                <p><b>การตรวจร่างกายโดยแพทย์ (Physical Examination)</b></p></td>
                                        </tr>
                                        <tr>
                                            <td style='text-align: left; padding-left: 10;'>
                                                <p>@PE</p><br/><br/>
                                                </td>
                                        </tr>
                                    </table>";
        if (dtVitalSign.Rows.Count > 0 && dtVitalSign != null)
        {
            tab.Content = tab.Content.Replace("@Height", dtVitalSign.Rows[0]["PEHeight"].ToString().Trim());
            tab.Content = tab.Content.Replace("@Weight", dtVitalSign.Rows[0]["PEWeight"].ToString().Trim());
            tab.Content = tab.Content.Replace("@BMIDetail", dtVitalSign.Rows[0]["BMIDetail"].ToString().Trim());
            tab.Content = tab.Content.Replace("@BMI", dtVitalSign.Rows[0]["PEBMI"].ToString().Trim());
            tab.Content = tab.Content.Replace("@WaistDetail", dtVitalSign.Rows[0]["WaistDetail"].ToString().Trim());
            tab.Content = tab.Content.Replace("@Waist", dtVitalSign.Rows[0]["PEAbdomen"].ToString().Trim());
            tab.Content = tab.Content.Replace("@BPDetail", dtVitalSign.Rows[0]["BPDetail"].ToString().Trim());
            tab.Content = tab.Content.Replace("@BPDias", dtVitalSign.Rows[0]["PEBPDias"].ToString().Trim());
            tab.Content = tab.Content.Replace("@BPSys", dtVitalSign.Rows[0]["PEBPSys"].ToString().Trim());
            tab.Content = tab.Content.Replace("@PulseDetail", dtVitalSign.Rows[0]["PulseDetail"].ToString().Trim());
            tab.Content = tab.Content.Replace("@Pulse", dtVitalSign.Rows[0]["PEPulseRate"].ToString().Trim());
            tab.Content = tab.Content.Replace("@PE", dtVitalSign.Rows[0]["DocDiageInfoTH"].ToString().Trim());
        }
        else
        {
            tab.Content = tab.Content.Replace("@Height", "");
            tab.Content = tab.Content.Replace("@Weight", "");
            tab.Content = tab.Content.Replace("@BMIDetail", "");
            tab.Content = tab.Content.Replace("@BMI", "");
            tab.Content = tab.Content.Replace("@WaistDetail", "");
            tab.Content = tab.Content.Replace("@Waist", "");
            tab.Content = tab.Content.Replace("@BPDetail", "");
            tab.Content = tab.Content.Replace("@BPDias", "");
            tab.Content = tab.Content.Replace("@BPSys", "");
            tab.Content = tab.Content.Replace("@PulseDetail", "");
            tab.Content = tab.Content.Replace("@Pulse", "");
            tab.Content = tab.Content.Replace("@PE", "");
        }
        ucTabs1.Tabs.Add(tab);
    }
    private void BindEKG()
    {
        DataTable dtEKG = new DataTable();
        dtEKG = ds.Tables["tblEKG"];
        tab = new ucTabs.Tab();
        tab.Name = "EKG";
        tab.Content = @"<p><h5><font color='#66ccff'>ผลการตรวจคลื่นไฟฟ้าหัวใจ (EKG)</font></h5></p><br/>
                        <p>@EKG</p><br /><br />";
        if (dtEKG.Rows.Count > 0 && dtEKG != null)
        {
            tab.Content = tab.Content.Replace("@EKG", dtEKG.Rows[0]["HEEKGInfoTH"].ToString().Trim());
        }
        else
        {
            tab.Content = tab.Content.Replace("@EKG", "None");
        }
        ucTabs1.Tabs.Add(tab);
    }
    private void FindLabAbNormal(DataTable dtLab)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Detail", typeof(string));
        if (dtLab.Rows.Count > 0 && dtLab != null)
        {
            for (int i = 0; i <= dtLab.Rows.Count - 1; i++)
            {
                if (!string.IsNullOrEmpty(dtLab.Rows[i]["NormalStatus"].ToString()))
                {
                    if (Convert.ToBoolean(dtLab.Rows[i]["NormalStatus"]) == false)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Detail"] = dtLab.Rows[i]["Summary_T"].ToString();
                        dt.Rows.Add(dr);
                    }
                }
            }
        }

        if (dt.Rows.Count > 0 && dt != null)
        {
            gvLabDetail.DataSource = dt;
            gvLabDetail.DataBind();
        }
    }

    protected void gvLab_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLab.PageIndex = e.NewPageIndex;

    }
    protected void gvLab_PageIndexChanged(object sender, EventArgs e)
    {

    }
}