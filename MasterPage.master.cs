using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.IO;

public partial class MasterPage : System.Web.UI.MasterPage
{
    #region Database Config
    string parameterChar = "@"; //SQLServer=@ MySQL=?
    clsSQL.DBType dbType = clsSQL.DBType.SQLServer;
    string cs = "cs";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            clsSQL clsSQL = new clsSQL();
            if (!clsSQL.IsConnected(dbType, cs))
            {
                Response.Write("ไม่สามารถเชื่อมต่อฐานข้อมูลได้");
                return;
            }
            MenuBuilder();
        }
    }

    private void MenuBuilder()
    {
        #region Variable
        clsSQL clsSQL = new clsSQL();
        clsDefault clsDefault = new clsDefault();
        ucMenuMega.Item item = new ucMenuMega.Item();
        StringBuilder strSQL = new StringBuilder();
        DataTable dt = new DataTable();
        DataTable dtService = new DataTable();
        #endregion

        item = new ucMenuMega.Item();
        item.UID = 1;
        switch (ucLanguageDB1.LanguageCurrent)
        {
            case "en-US":
                item.Name = "About Hospital";
                break;
            case "km-KH":
                item.Name = "គេហទំព័រដើម";
                break;
            default:
                item.Name = "รู้จักเรา";
                break;
        }
        item.Detail = "";
        item.URL = "";
        ucMenuMega1.Items.Add(item);

        #region รู้จักเรา
        item = new ucMenuMega.Item();
        item.UID = 11;
        item.ParentUID = 1;
        switch (ucLanguageDB1.LanguageCurrent)
        {
            case "en-US":
                item.Name = "Overview";
                break;
            case "km-KH":
                item.Name = "<span style='font-size:7.5pt;'>" + "ទស្សនីយភាពរួមមន្ទីពេទ្យបាងកកចាន់បុរី"+"</span>";
                break;
            default:
                item.Name = "โรงพยาบาลกรุงเทพจันทบุรี";
                break;
        }
        item.Detail = "";
        item.URL = "/AboutHospital";
        ucMenuMega1.Items.Add(item);

        item = new ucMenuMega.Item();
        item.UID = 12;
        item.ParentUID = 1;
        switch (ucLanguageDB1.LanguageCurrent)
        {
            case "en-US":
                item.Name = "Hospital Facilities";
                break;
            case "km-KH":
                item.Name = "<span style='font-size:7.5pt;'>" + "គ្រឿងបរិក្ខាផេ្សងៗ"+"</span>";
                break;
            default:
                item.Name = "สิ่งอำนวยความสะดวกในโรงพยาบาล";
                break;
        }
        item.Detail = "";
        item.URL = "/Facilities";
        ucMenuMega1.Items.Add(item);

        item = new ucMenuMega.Item();
        item.UID = 13;
        item.ParentUID = 1;
        switch (ucLanguageDB1.LanguageCurrent)
        {
            case "en-US":
                item.Name = "Award & Accreditations";
                break;
            case "km-KH":
                item.Name = "<span style='font-size:7.5pt;'>" + "រង្វាន់ធានាគុណភាពសេវាកម្ម"+"</span>";
                break;
            default:
                item.Name = "รางวัลและการประกันคุณภาพบริการ";
                break;
        }
        item.Detail = "";
        item.URL = "/Awards";
        ucMenuMega1.Items.Add(item);

        item = new ucMenuMega.Item();
        item.UID = 14;
        item.ParentUID = 1;
        switch (ucLanguageDB1.LanguageCurrent)
        {
            case "en-US":
                item.Name = "Advanced Technologies";
                break;
            case "km-KH":
                item.Name = "<span style='font-size:7.5pt;'>" + "បច្ទេកវិទ្យាទំនើប"+"</span>";
                break;
            default:
                item.Name = "เทคโนโลยีเพื่อการรักษาผู้ป่วย";
                break;
        }
        item.Detail = "";
        item.URL = "/AdvancedTechnologies";
        ucMenuMega1.Items.Add(item);

        item = new ucMenuMega.Item();
        item.UID = 15;
        item.ParentUID = 1;
        switch (ucLanguageDB1.LanguageCurrent)
        {
            case "en-US":
                item.Name = "Hospital Network";
                break;
            case "km-KH":
                item.Name = "<span style='font-size:7.5pt;'>" + "បណ្តាញរបស់មន្ទីពេទ្យ"+"</span>";
                break;
            default:
                item.Name = "กลุ่มโรงพยาบาลเครือข่าย";
                break;
        }
        item.Detail = "";
        item.URL = "/HospitalNetwork";
        ucMenuMega1.Items.Add(item);

        item = new ucMenuMega.Item();
        item.UID = 16;
        item.ParentUID = 1;
        switch (ucLanguageDB1.LanguageCurrent)
        {
            case "en-US":
                item.Name = "Vision & Mission";
                break;
            case "km-KH":
                item.Name = "<span style='font-size:7.5pt;'>" + "ទស្សនៈវិស័យរបស់" + "</span>";
                break;
            default:
                item.Name = "วิสัยทัศน์ และ พันธกิจ";
                break;
        }
        item.Detail = "";
        item.URL = "/VisionMission";
        ucMenuMega1.Items.Add(item);

        item = new ucMenuMega.Item();
        item.UID = 17;
        item.ParentUID = 1;
        switch (ucLanguageDB1.LanguageCurrent)
        {
            case "en-US":
                item.Name = "About Chanthaburi";
                break;
            case "km-KH":
                item.Name = "<span style='font-size:7.5pt;'>" + "About Chanthaburi" + "</span>";
                break;
            default:
                item.Name = "ข้อมูลทั่วไปของจังหวัดจันทบุรี";
                break;
        }
        item.Detail = "";
        item.URL = "/AboutChanthaburi";
        ucMenuMega1.Items.Add(item);
        #endregion

        item = new ucMenuMega.Item();
        item.UID = 2;
        switch (ucLanguageDB1.LanguageCurrent)
        {
            case "en-US":
                item.Name = "Healthcare Services";
                break;
            case "km-KH":
                item.Name = "<span style='font-size:7.5pt;'>" + "គ្លីនិក​​&​​ មជ្ឈមណ្ឌលព្យាបាលជំងឺផ្សេងៗ"+"</span>";
                break;
            default:
                item.Name = "บริการทางการแพทย์";
                break;
        }
        item.Detail = "";
        item.MegaData = CenterBuilder();
        ucMenuMega1.Items.Add(item);

        item = new ucMenuMega.Item();
        item.UID = 3;
        switch (ucLanguageDB1.LanguageCurrent)
        {
            case "en-US":
                item.Name = "Patient Services";
                break;
            case "km-KH":
                item.Name = "<span style='font-size:7.5pt;'>" + "សេវាកម្មអ្នកជម្ងឺ"+"</span>";
                break;
            default:
                item.Name = "บริการสำหรับผู้ป่วย";
                break;
        }
        item.Detail = "";
        ucMenuMega1.Items.Add(item);

        #region บริการสำหรับผู้ป่วย
        item = new ucMenuMega.Item();
        item.UID = 31;
        item.ParentUID = 3;
        switch (ucLanguageDB1.LanguageCurrent)
        {
            case "en-US":
                item.Name = "Room & Facilities";
                break;
            case "km-KH":
                item.Name = "<span style='font-size:7.5pt;'>" + "បរិក្ខាប្រើប្រាស់ក្នុងបន្ទប់"+"</span>";
                break;
            default:
                item.Name = "ห้องพักผู้ป่วยและสิ่งอำนวยความสะดวก";
                break;
        }
        item.Detail = "";
        item.URL = "";
        ucMenuMega1.Items.Add(item);

        #region Service
        #region ServiceGroup
        #region SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("ServiceGroup.UID,ServiceGroup.Name ");
        strSQL.Append("FROM ");
        strSQL.Append("ServiceGroup ");
        strSQL.Append("INNER JOIN Language ");
        strSQL.Append("ON ServiceGroup.LanguageUID=Language.UID AND Language.Active='1' ");
        strSQL.Append("WHERE ");
        strSQL.Append("ServiceGroup.Active='1' ");
        strSQL.Append("AND Language.Name='" + ucLanguageDB1.LanguageCurrent + "' ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("ServiceGroup.Sort ASC");
	    #endregion
        
        dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        strSQL.Length = 0; strSQL.Capacity = 0;

        if (dt != null && dt.Rows.Count > 0)
        {
            for (int g = 0; g < dt.Rows.Count; g++)
            {
                item = new ucMenuMega.Item();
                item.UID = int.Parse("31"+(g+1).ToString());
                item.ParentUID = 31;
                item.Name = dt.Rows[g]["Name"].ToString();
                item.Detail = "";
                item.URL = "";
                ucMenuMega1.Items.Add(item);

                #region Service
                #region SQL Query
                strSQL.Append("SELECT ");
                //strSQL.Append("UID,");
                strSQL.Append("Service.DepartmentUID UID,");
                strSQL.Append("Service.Name ");
                strSQL.Append("FROM ");
                strSQL.Append("Service ");
                strSQL.Append("INNER JOIN Language ");
                strSQL.Append("ON Service.LanguageUID=Language.UID AND Language.Active='1' ");
                strSQL.Append("WHERE ");
                strSQL.Append("Service.Active='1' ");
                strSQL.Append("AND Service.ServiceGroupUID='" + dt.Rows[g]["UID"].ToString() + "' ");
                strSQL.Append("AND Language.Name='" + ucLanguageDB1.LanguageCurrent + "' ");
                strSQL.Append("ORDER BY ");
                strSQL.Append("Service.Sort ASC");
                #endregion

                dtService = clsSQL.Bind(strSQL.ToString(), dbType, cs);
                strSQL.Length = 0; strSQL.Capacity = 0;

                if (dtService != null && dtService.Rows.Count > 0)
                {
                    for (int s = 0; s < dtService.Rows.Count; s++)
                    {
                        item = new ucMenuMega.Item();
                        item.UID = int.Parse("31" + (g + 1).ToString()+(s+1).ToString());
                        item.ParentUID = int.Parse("31" + (g + 1).ToString());
                        item.Name = dtService.Rows[s]["Name"].ToString();
                        item.Detail = "";
                        item.URL = "/Service/"+dtService.Rows[s]["UID"].ToString()+"/"+clsDefault.URLRoutingFilter(dtService.Rows[s]["Name"].ToString())+"/";
                        ucMenuMega1.Items.Add(item);
                    }
                    dtService = null;
                }
                #endregion
            }
            dt = null;
        }
        #endregion
        #endregion

        item = new ucMenuMega.Item();
        item.UID = 33;
        item.ParentUID = 3;
        switch (ucLanguageDB1.LanguageCurrent)
        {
            case "en-US":
                item.Name = "Chivawattana Membership Card";
                break;
            case "km-KH":
                item.Name = "<span style='font-size:7.5pt;'>" + "ចូលជាសមាជិកកម្មវិធីជីវះវឌ្ឍនះ"+"</span>";
                break;
            default:
                item.Name = "สมาชิกบัตรชีววัฒนะ";
                break;
        }
        item.Detail = "";
        item.URL = "/Chivawattana/";
        ucMenuMega1.Items.Add(item);

        /*
        item = new ucMenuMega.Item();
        item.UID = 34;
        item.ParentUID = 3;
        switch (ucLanguageDB1.LanguageCurrent)
        {
            case "en-US":
                item.Name = "Checkup Result";
                break;
            default:
                item.Name = "ผลตรวจสุขภาพ";
                break;
        }
        item.Detail = "";
        item.URL = "/CheckupCondition/";
        ucMenuMega1.Items.Add(item);
        */

        item = new ucMenuMega.Item();
        item.UID = 35;
        item.ParentUID = 3;
        switch (ucLanguageDB1.LanguageCurrent)
        {
            case "en-US":
                item.Name = "Health Packages";
                break;
            case "km-KH":
                item.Name = "<span style='font-size:7.5pt;'>" + "កញ្ចប់ សុខភាព"+"</span>";
                break;
            default:
                item.Name = "แพคเกจตรวจสุขภาพ";
                break;
        }
        item.Detail = "";
        item.URL = "/HealthPackage";
        ucMenuMega1.Items.Add(item);

        item = new ucMenuMega.Item();
        item.UID = 36;
        item.ParentUID = 3;
        switch (ucLanguageDB1.LanguageCurrent)
        {
            case "en-US":
                item.Name = "Packages";
                break;
            case "km-KH":
                item.Name = "<span style='font-size:7.5pt;'>" + "កញ្ចប់ សុខភាព"+"</span>";
                break;
            default:
                item.Name = "แพคเกจโรคทั่วไป";
                break;
        }
        item.Detail = "";
        item.URL = "/Package";
        ucMenuMega1.Items.Add(item);

        item = new ucMenuMega.Item();
        item.UID = 37;
        item.ParentUID = 3;
        switch (ucLanguageDB1.LanguageCurrent)
        {
            case "en-US":
                item.Name = "Promotions";
                break;
            case "km-KH":
                item.Name = "<span style='font-size:7.5pt;'>" + "ការផ្ដល់ជូនពិសេស"+"</span>";
                break;
            default:
                item.Name = "โปรโมชั่น";
                break;
        }
        item.Detail = "";
        item.URL = "/Promotion";
        ucMenuMega1.Items.Add(item);
        #endregion

        item = new ucMenuMega.Item();
        item.UID = 4;
        switch (ucLanguageDB1.LanguageCurrent)
        {
            case "en-US":
                item.Name = "Find a Doctor";
                break;
            case "km-KH":
                item.Name = "<span style='font-size:7.5pt;'>" + "ស្វែងរកគ្រូពេទ្យ &​​​​​​​​ ធ្វើការណាត់"+"</span>";
                break;
            default:
                item.Name = "ค้นหาและนัดหมายแพทย์";
                break;
        }
        item.Detail = "";
        item.URL = "/DoctorSchedule";
        ucMenuMega1.Items.Add(item);

        item = new ucMenuMega.Item();
        item.UID = 5;
        switch (ucLanguageDB1.LanguageCurrent)
        {
            case "en-US":
                item.Name = "Hospital News";
                break;
            case "km-KH":
                item.Name = "<span style='font-size:7.5pt;'>" + "ព័ត៍មាន"+"</span>";
                break;
            default:
                item.Name = "ข่าวสารโรงพยาบาล";
                break;
        }
        item.Detail = "";
        ucMenuMega1.Items.Add(item);

        #region ข่าวสารโรงพยาบาล
        item = new ucMenuMega.Item();
        item.UID = 51;
        item.ParentUID = 5;
        switch (ucLanguageDB1.LanguageCurrent)
        {
            case "en-US":
                item.Name = "Events";
                break;
            case "km-KH":
                item.Name = "<span style='font-size:7.5pt;'>" + "ព្រឹត្តការណ៍"+"</span>";
                break;
            default:
                item.Name = "กิจกรรม";
                break;
        }
        item.Detail = "";
        item.URL = "/Event";
        ucMenuMega1.Items.Add(item);

        item = new ucMenuMega.Item();
        item.UID = 52;
        item.ParentUID = 5;
        switch (ucLanguageDB1.LanguageCurrent)
        {
            case "en-US":
                item.Name = "News";
                break;
            case "km-KH":
                item.Name = "<span style='font-size:7.5pt;'>" + "ព័ត៍មាន"+"</span>";
                break;
            default:
                item.Name = "ข่าวประชาสัมพันธ์";
                break;
        }
        item.Detail = "";
        item.URL = "/News";
        ucMenuMega1.Items.Add(item);

        item = new ucMenuMega.Item();
        item.UID = 53;
        item.ParentUID = 5;
        switch (ucLanguageDB1.LanguageCurrent)
        {
            case "en-US":
                item.Name = "Health Articles";
                break;
            case "km-KH":
                item.Name = "<span style='font-size:7.5pt;'>" + "សុខភាព មាត្រា"+"</span>";
                break;
            default:
                item.Name = "บทความสุขภาพ";
                break;
        }
        item.Detail = "";
        item.URL = "/Article";
        ucMenuMega1.Items.Add(item);
        #endregion

        item = new ucMenuMega.Item();
        item.UID = 6;
        switch (ucLanguageDB1.LanguageCurrent)
        {
            case "en-US":
                item.Name = "Contact Us";
                break;
            case "km-KH":
                item.Name = "<span style='font-size:7.5pt;'>" + "ទំនាក់ទំនងមន្ទីពេទ្យបាងកកចាន់បុរី"+"</span>";
                break;
            default:
                item.Name = "ติดต่อเรา";
                break;
        }
        item.Detail = "";
        ucMenuMega1.Items.Add(item);

        #region ติดต่อเรา
        item = new ucMenuMega.Item();
        item.UID = 61;
        item.ParentUID = 6;
        switch (ucLanguageDB1.LanguageCurrent)
        {
            case "en-US":
                item.Name = "Inquiry";
                break;
            case "km-KH":
                item.Name = "<span style='font-size:7.5pt;'>" + "សាកសួរពត៌មានលំអិត"+"</span>";
                break;
            default:
                item.Name = "ฝากคำถามถึงโรงพยาบาล";
                break;
        }
        item.Detail = "";
        item.URL = "/Inquiry/";
        ucMenuMega1.Items.Add(item);

        item = new ucMenuMega.Item();
        item.UID = 62;
        item.ParentUID = 6;
        switch (ucLanguageDB1.LanguageCurrent)
        {
            case "en-US":
                item.Name = "Feedback";
                break;
            case "km-KH":
                item.Name = "<span style='font-size:7.5pt;'>" + "ផ្តល់មតិរិះគន់ដើម្បីកែប្រែ"+"</span>";
                break;
            default:
                item.Name = "แนะนำ/ติชม ถึงผู้บริหาร";
                break;
        }
        item.Detail = "";
        item.URL = "/Feedback/";
        ucMenuMega1.Items.Add(item);

        item = new ucMenuMega.Item();
        item.UID = 63;
        item.ParentUID = 6;
        switch (ucLanguageDB1.LanguageCurrent)
        {
            case "en-US":
                item.Name = "Webboard";
                break;
            case "km-KH":
                item.Name = "Webboard";
                break;
            default:
                item.Name = "เว็บบอร์ดตอบปัญหาสุขภาพ";
                break;
        }
        item.Detail = "";
        item.URL = "/Webboard/";
        //ucMenuMega1.Items.Add(item);

        item = new ucMenuMega.Item();
        item.UID = 64;
        item.ParentUID = 6;
        switch (ucLanguageDB1.LanguageCurrent)
        {
            case "en-US":
                item.Name = "Maps & Directions";
                break;
            case "km-KH":
                item.Name = "<span style='font-size:7.5pt;'>" + "ផែនទី"+"</span>";
                break;
            default:
                item.Name = "แผนที่และการเดินทาง";
                break;
        }
        item.Detail = "";
        item.URL = "/Maps/";
        ucMenuMega1.Items.Add(item);

        item = new ucMenuMega.Item();
        item.UID = 65;
        item.ParentUID = 6;
        switch (ucLanguageDB1.LanguageCurrent)
        {
            case "en-US":
                item.Name = "Jobs";
                break;
            case "km-KH":
                item.Name = "<span style='font-size:7.5pt;'>" + "រួមការងារជាមួយយើង"+"</span>";
                break;
            default:
                item.Name = "ร่วมงานกับเรา";
                break;
        }
        item.Detail = "";
        item.URL = "/Jobs/";
        ucMenuMega1.Items.Add(item);
        #endregion
    }

    private string CenterBuilder()
    {
        #region Variable
        clsDefault clsDefault = new clsDefault();
        clsSQL clsSQL = new clsSQL();
        clsLanguage clsLanguage = new clsLanguage();

        StringBuilder strSQL = new StringBuilder();
        StringBuilder strCenter = new StringBuilder();
        DataTable dtGroup = new DataTable();
        DataTable dt = new DataTable();
        #endregion
        #region MedicalCenterGroup : SQL Query
        strSQL.Append("SELECT ");
        strSQL.Append("MedicalCenterGroup.UID,");
        strSQL.Append("MedicalCenterGroup.Name ");
        strSQL.Append("FROM ");
        strSQL.Append("MedicalCenterGroup ");
        strSQL.Append("INNER JOIN Language ON MedicalCenterGroup.LanguageUID=Language.UID AND Language.Active='1' ");
        strSQL.Append("WHERE ");
        strSQL.Append("MedicalCenterGroup.Active='1' ");
        strSQL.Append("AND Language.Name='" + clsLanguage.LanguageCurrent + "' ");
        strSQL.Append("ORDER BY ");
        strSQL.Append("MedicalCenterGroup.Sort");
        #endregion

        dtGroup = clsSQL.Bind(strSQL.ToString(), dbType, cs);
        strSQL.Length = 0; strSQL.Capacity = 0;

        strCenter.Append("<div style='padding:0 10px 0 10px;width:400px;'>");
        if (dtGroup != null && dtGroup.Rows.Count > 0)
        {
            for (int i = 0; i < dtGroup.Rows.Count; i++)
            {
                strCenter.Append("<h4>" + dtGroup.Rows[i]["Name"].ToString() + "</h4>");

                #region MedicalCenter
                #region MedicalCenter : SQL Query
                strSQL.Append("SELECT ");
                //strSQL.Append("UID,");
                strSQL.Append("DepartmentUID UID,");
                strSQL.Append("Name ");
                strSQL.Append("FROM ");
                strSQL.Append("MedicalCenter ");
                strSQL.Append("WHERE ");
                strSQL.Append("MedicalCenterGroupUID=" + dtGroup.Rows[i]["UID"].ToString() + " ");
                strSQL.Append("AND Active='1' ");
                #endregion

                dt = clsSQL.Bind(strSQL.ToString(), dbType, cs);
                strSQL.Length = 0; strSQL.Capacity = 0;

                if (dt != null && dt.Rows.Count > 0)
                {
                    strCenter.Append("<table cellpadding='0' cellspacing='0'>");
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if ((j+1) % 2 != 0)
                        {
                            strCenter.Append("<tr>");
                            strCenter.Append("<td style='width:200px;'>");
                            strCenter.Append("<a href='/MedicalCenter/" + dt.Rows[j]["UID"].ToString() + "/" + clsDefault.URLRoutingFilter(dt.Rows[j]["Name"]) + "/'>");
                            strCenter.Append(dt.Rows[j]["Name"].ToString());
                            strCenter.Append("</a>");
                            strCenter.Append("</td>");
                        }
                        else
                        {
                            strCenter.Append("<td style='width:200px;'>");
                            strCenter.Append("<a href='/MedicalCenter/" + dt.Rows[j]["UID"].ToString() + "/" + clsDefault.URLRoutingFilter(dt.Rows[j]["Name"]) + "/'>");
                            strCenter.Append(dt.Rows[j]["Name"].ToString());
                            strCenter.Append("</a>");
                            strCenter.Append("</td>");
                            strCenter.Append("</tr>");
                        }
                    }
                    if (dt.Rows.Count % 2 != 0)
                    {
                        strCenter.Append("<td style='width:200px;'>");
                        strCenter.Append("</td>");
                        strCenter.Append("</tr>");
                    }
                    strCenter.Append("</table>");

                    dt = null;
                }
                else
                {
                    strCenter.Append("<div style='text-align:center;'>-</div>");
                }
                #endregion
            }
        }
        else
        {
            strCenter.Append("-");
        }
        strCenter.Append("</div>");

        /*
        
        
        strCenter.Append("<tr>");
        strCenter.Append("<td style='width:200px;'>");
        strCenter.Append("<a href='/Center/" + dt.Rows[i]["MedicalCenterUID"].ToString() + "/" + dt.Rows[i]["MedicalCenterName"].ToString() + "/'>");
        strCenter.Append(dt.Rows[i]["MedicalCenterName"].ToString());
        strCenter.Append("</a>");
        strCenter.Append("</td>");
        strCenter.Append("<td style='width:200px;'>");
        strCenter.Append("<a href='/Center/" + dt.Rows[i]["MedicalCenterUID"].ToString() + "/" + dt.Rows[i]["MedicalCenterName"].ToString() + "/'>");
        strCenter.Append(dt.Rows[i]["MedicalCenterName"].ToString());
        strCenter.Append("</a>");
        strCenter.Append("</td>");
        strCenter.Append("</tr>");
        
        
        */
        return strCenter.ToString();
    }
}
