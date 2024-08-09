using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using OneTrack_v2.DbData;
using OneTrak_v2.Services.Model;
using System.Text;

namespace OneTrak_v2.Server.Services.Email.Templates
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly AppDataContext _db;

        public EmailTemplateService(AppDataContext db)
        {
            _db = db;
        }

        public Tuple<string, string, string, string> GetMessageHTML(int vEmploymentID)
        {
            string emailHdrHTML = string.Empty;
            string emailFtrHTML = string.Empty;
            string strXML = string.Empty;
            string strReturnStatus = string.Empty;
            string strH2MgrEmail = string.Empty;
            string strMgrEmail = string.Empty;
            string strLicenseTechEmail = string.Empty;
            string strTMEmail = string.Empty;

            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strH2MgrName = managerInfo.H2MgrName ?? "";
                string strH2MgrTitle = managerInfo.H2MgrTitle ?? "";
                strH2MgrEmail = managerInfo.H2MgrEmail ?? "";
                string strMgrName = managerInfo.MgrName ?? "";
                string strMgrTitle = managerInfo.MgrTitle ?? "";
                strMgrEmail = managerInfo.MgrEmail ?? "";
                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                strTMEmail = managerInfo.TMEmail ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                // HEADER HTML
                emailHdrHTML = emailHdrHTML + @"<div><div>";
                emailHdrHTML = emailHdrHTML + @"<table style = ""width: 800px;""> ";
                emailHdrHTML = emailHdrHTML + @"<tr> ";
                emailHdrHTML = emailHdrHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"</tr> ";
                emailHdrHTML = emailHdrHTML + @"<tr> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"</tr> ";
                emailHdrHTML = emailHdrHTML + @"<tr> ";
                emailHdrHTML = emailHdrHTML + @"<td colspan = ""3"" > ";
                emailHdrHTML = emailHdrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                emailHdrHTML = emailHdrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                emailHdrHTML = emailHdrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                emailHdrHTML = emailHdrHTML + @"</td> ";
                emailHdrHTML = emailHdrHTML + @"<td>&nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td>&nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"</tr> ";
                emailHdrHTML = emailHdrHTML + @"<tr> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"</tr> ";
                emailHdrHTML = emailHdrHTML + @"<tr> ";
                emailHdrHTML = emailHdrHTML + @"<td colspan = ""3"" >";
                emailHdrHTML = emailHdrHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" > ACTION REQUIRED</span></td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"</tr> ";
                emailHdrHTML = emailHdrHTML + @"<tr> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"</tr> ";
                emailHdrHTML = emailHdrHTML + @"<tr> ";
                emailHdrHTML = emailHdrHTML + @"<td colspan = ""3"" >";
                emailHdrHTML = emailHdrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                emailHdrHTML = emailHdrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY </span>";
                emailHdrHTML = emailHdrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >to this email </span>";
                emailHdrHTML = emailHdrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >ONLY </span>";
                emailHdrHTML = emailHdrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >without copying your Licensing Specialist to expedite your license process. </span>";
                emailHdrHTML = emailHdrHTML + @"</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"</tr> ";
                emailHdrHTML = emailHdrHTML + @"<tr> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"</tr> ";
                emailHdrHTML = emailHdrHTML + @"</table> ";
                emailHdrHTML = emailHdrHTML + @"</div>";
                emailHdrHTML = emailHdrHTML + @"<div>";

                // FOOTER HTML
                emailFtrHTML = emailFtrHTML + @"<table style = ""width: 800px;""> ";
                emailFtrHTML = emailFtrHTML + @"<tr> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"</tr> ";
                emailFtrHTML = emailFtrHTML + @"<tr> ";
                emailFtrHTML = emailFtrHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" > Thank You,</span></td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"</tr> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"</tr> ";
                emailFtrHTML = emailFtrHTML + @"<tr> ";
                emailFtrHTML = emailFtrHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"</tr> ";
                emailFtrHTML = emailFtrHTML + @"<tr> ";
                emailFtrHTML = emailFtrHTML + @"<td>&nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td>&nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td>&nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"</tr> ";
                emailFtrHTML = emailFtrHTML + @"<td colspan = ""3"" > ";
                emailFtrHTML = emailFtrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"</tr> ";
                emailFtrHTML = emailFtrHTML + @"<tr> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"</tr> ";
                emailFtrHTML = emailFtrHTML + @"</table> ";
                emailFtrHTML = emailFtrHTML + @"</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"</tr> ";
                emailFtrHTML = emailFtrHTML + @"</table> ";
                emailFtrHTML = emailFtrHTML + @"</div></div>";
            }
            catch (Exception ex)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, ex.ToString());
            }

            var hearderHTML = emailHdrHTML;
            var footerHTML = emailFtrHTML;

            return new Tuple<string, string, string, string>(hearderHTML, footerHTML, strTMEmail, strMgrEmail);
        }

        public Tuple<string, string, string, string> GetCourtDocHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;

            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" ><span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" > ACTION REQUIRED</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >to this email attaching the described supporting documents to expedite processing of your license application.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Your insurance license application has been received. Additional documents are required to complete the license application process. </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >NOTE:  </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >For Questions 1a, 1b and 1c, </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >''Convicted'' </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >includes, but is not limited to, having been found guilty by verdict of a judge or jury, having entered a plea of guilty or nolo contendere or no contest, or having been given probation, a suspended sentence, or a fine.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >If you answer </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >YES </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >to any of these questions, the DOI requires the following to be attached to the application: </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >a) a written statement explaining the circumstances of each incident, </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >b) a copy of the charging document, and  </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >c) a copy of the official document, which demonstrates the resolution of the charges or any final </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >judgment. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" > Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
                strHTML = strHTML + @"</html> ";

            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetIncompleteHTML(int vEmploymentID)
        {
            string emailHdrHTML = string.Empty;
            string emailFtrHTML = string.Empty;
            string strXML = string.Empty;
            string strReturnStatus = string.Empty;
            string strH2MgrEmail = string.Empty;
            string strMgrEmail = string.Empty;
            string strLicenseTechEmail = string.Empty;
            string strTMEmail = string.Empty;

            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strH2MgrName = managerInfo.H2MgrName ?? "";
                string strH2MgrTitle = managerInfo.H2MgrTitle ?? "";
                strH2MgrEmail = managerInfo.H2MgrEmail ?? "";
                string strMgrName = managerInfo.MgrName ?? "";
                string strMgrTitle = managerInfo.MgrTitle ?? "";
                strMgrEmail = managerInfo.MgrEmail ?? "";
                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                strTMEmail = managerInfo.TMEmail ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                // HEADER HTML
                emailHdrHTML = emailHdrHTML + @"<div><div>";
                emailHdrHTML = emailHdrHTML + @"<table style = ""width: 800px;""> ";
                emailHdrHTML = emailHdrHTML + @"<tr> ";
                emailHdrHTML = emailHdrHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"</tr> ";
                emailHdrHTML = emailHdrHTML + @"<tr> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"</tr> ";
                emailHdrHTML = emailHdrHTML + @"<tr> ";
                emailHdrHTML = emailHdrHTML + @"<td colspan = ""3"" > ";
                emailHdrHTML = emailHdrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                emailHdrHTML = emailHdrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                emailHdrHTML = emailHdrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                emailHdrHTML = emailHdrHTML + @"</td> ";
                emailHdrHTML = emailHdrHTML + @"<td>&nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td>&nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"</tr> ";
                emailHdrHTML = emailHdrHTML + @"<tr> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"</tr> ";
                emailHdrHTML = emailHdrHTML + @"<tr> ";
                emailHdrHTML = emailHdrHTML + @"<td colspan = ""3"" ><span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" > ACTION REQUIRED</span></td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"</tr> ";
                emailHdrHTML = emailHdrHTML + @"<tr> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"</tr> ";
                emailHdrHTML = emailHdrHTML + @"<tr> ";
                emailHdrHTML = emailHdrHTML + @"<td colspan = ""3"" >";
                emailHdrHTML = emailHdrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                emailHdrHTML = emailHdrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY </span>";
                emailHdrHTML = emailHdrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >to this email attaching the described supporting documents to expedite processing of your license application.</span>";
                emailHdrHTML = emailHdrHTML + @"</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"</tr> ";
                emailHdrHTML = emailHdrHTML + @"<tr> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"</tr> ";
                emailHdrHTML = emailHdrHTML + @"<tr> ";
                emailHdrHTML = emailHdrHTML + @"<td colspan = ""3"" >";
                emailHdrHTML = emailHdrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Your insurance application has been received but it is incomplete.  The following information is needed. </span></td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"</tr> ";
                emailHdrHTML = emailHdrHTML + @"<tr> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"</tr> ";
                emailHdrHTML = emailHdrHTML + @"</table> ";
                emailHdrHTML = emailHdrHTML + @"</div>";
                emailHdrHTML = emailHdrHTML + @"<div>";

                // FOOTER HTML
                emailHdrHTML = emailHdrHTML + @"<div>";
                emailFtrHTML = emailFtrHTML + @"<table style = ""width: 800px;""> ";
                emailFtrHTML = emailFtrHTML + @"<tr> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"</tr> ";
                emailFtrHTML = emailFtrHTML + @"<tr> ";
                emailFtrHTML = emailFtrHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" > Thank You,</span></td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"</tr> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"</tr> ";
                emailFtrHTML = emailFtrHTML + @"<tr> ";
                emailFtrHTML = emailFtrHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"</tr> ";
                emailFtrHTML = emailFtrHTML + @"<tr> ";
                emailFtrHTML = emailFtrHTML + @"<td>&nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td>&nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td>&nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"</tr> ";
                emailFtrHTML = emailFtrHTML + @"<td colspan = ""3"" > ";
                emailFtrHTML = emailFtrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"</tr> ";
                emailFtrHTML = emailFtrHTML + @"<tr> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"</tr> ";
                emailFtrHTML = emailFtrHTML + @"</table> ";
                emailFtrHTML = emailFtrHTML + @"</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"</tr> ";
                emailFtrHTML = emailFtrHTML + @"</table> ";
                emailFtrHTML = emailFtrHTML + @"</div></div>";

            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            var hearderHTML = emailHdrHTML;
            var footerHTML = emailFtrHTML;

            return new Tuple<string, string, string, string>(hearderHTML, footerHTML, strTMEmail, strMgrEmail);
        }

        public Tuple<string, string, string, string> GetApplNotRecievedHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;

            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" ><span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" > ACTION REQUIRED</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >to this email attaching the described supporting documents to expedite processing of your license application.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Your insurance application and additional documents that were previously sent to you that are required to obtain your insurance license have not yet been received.  </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Fully complete the application and additional required documents to continue your licensing process. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" > Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
                //strHTML = strHTML + @"</html> ";

            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetAPPLicCopyDisplayGaKyMtWaWyHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;

            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" ><span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" > ACTION REQUIRED</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >to this email if you have questions. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Please print the attached license and have available for review by Auditors and Examiners.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Insurance Producer licenses in your state should be conspicuously displayed in the place of </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >business. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >You will receive authorization letter showing you are authorized to offer the corresponding</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >insurance. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" > Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
                //strHTML = strHTML + @"</html> ";

            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetAPPLicenseCopyHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;

            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" ><span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" > ACTION REQUIRED</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >to this email if you have questions. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Please print the attached license and have available for review by Auditors and Examiners. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >You will receive authorization letter showing you are authorized to offer the corresponding</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >insurance. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" > Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
                //strHTML = strHTML + @"</html> ";

            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetEmploymentHistoryHTML(int vEmploymentID)
        {
            string emailHdrHTML = string.Empty;
            string emailFtrHTML = string.Empty;
            string strXML = string.Empty;
            string strReturnStatus = string.Empty;
            string strH2MgrEmail = string.Empty;
            string strMgrEmail = string.Empty;
            string strLicenseTechEmail = string.Empty;
            string strTMEmail = string.Empty;

            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strH2MgrName = managerInfo.H2MgrName ?? "";
                string strH2MgrTitle = managerInfo.H2MgrTitle ?? "";
                strH2MgrEmail = managerInfo.H2MgrEmail ?? "";
                string strMgrName = managerInfo.MgrName ?? "";
                string strMgrTitle = managerInfo.MgrTitle ?? "";
                strMgrEmail = managerInfo.MgrEmail ?? "";
                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                strTMEmail = managerInfo.TMEmail ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                // HEADER HTML
                emailHdrHTML = emailHdrHTML + @"<div><div>";
                emailHdrHTML = emailHdrHTML + @"<table style = ""width: 800px;""> ";
                emailHdrHTML = emailHdrHTML + @"<tr> ";
                emailHdrHTML = emailHdrHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"</tr> ";
                emailHdrHTML = emailHdrHTML + @"<tr> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"</tr> ";
                emailHdrHTML = emailHdrHTML + @"<tr> ";
                emailHdrHTML = emailHdrHTML + @"<td colspan = ""3"" > ";
                emailHdrHTML = emailHdrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                emailHdrHTML = emailHdrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                emailHdrHTML = emailHdrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                emailHdrHTML = emailHdrHTML + @"</td> ";
                emailHdrHTML = emailHdrHTML + @"<td>&nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td>&nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"</tr> ";
                emailHdrHTML = emailHdrHTML + @"<tr> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"</tr> ";
                emailHdrHTML = emailHdrHTML + @"<tr> ";
                emailHdrHTML = emailHdrHTML + @"<td colspan = ""3"" ><span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" > ACTION REQUIRED</span></td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"</tr> ";
                emailHdrHTML = emailHdrHTML + @"<tr> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"</tr> ";
                emailHdrHTML = emailHdrHTML + @"<td colspan = ""3"" >";
                emailHdrHTML = emailHdrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                emailHdrHTML = emailHdrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY </span>";
                emailHdrHTML = emailHdrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >to this email with the requested information to expedite your license process.</span>";
                emailHdrHTML = emailHdrHTML + @"</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"</tr> ";
                emailHdrHTML = emailHdrHTML + @"<tr> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"</tr> ";
                emailHdrHTML = emailHdrHTML + @"<td colspan = ""3"" >";
                emailHdrHTML = emailHdrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Employment History on your license application says </span>";
                emailHdrHTML = emailHdrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >full 5 years with no gaps.  </span>";
                emailHdrHTML = emailHdrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >There are gaps in the work history and the DOI rejects incomplete applications.</span>";
                emailHdrHTML = emailHdrHTML + @"</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"</tr> ";
                emailHdrHTML = emailHdrHTML + @"<tr> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"<td> &nbsp;</td> ";
                emailHdrHTML = emailHdrHTML + @"</tr> ";
                emailHdrHTML = emailHdrHTML + @"</table> ";
                emailHdrHTML = emailHdrHTML + @"</div>";
                emailHdrHTML = emailHdrHTML + @"<div>";

                // FOOTER HTML
                emailFtrHTML = emailFtrHTML + @"<table style = ""width: 800px;""> ";
                emailFtrHTML = emailFtrHTML + @"<tr> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"</tr> ";
                emailFtrHTML = emailFtrHTML + @"<tr> ";
                emailFtrHTML = emailFtrHTML + @"<td colspan = ""3"" >";
                emailFtrHTML = emailFtrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Show </span>";
                emailFtrHTML = emailFtrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >UNEMPLOYED </span>";
                emailFtrHTML = emailFtrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >if you were not working </span>";
                emailFtrHTML = emailFtrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >and </span>";
                emailFtrHTML = emailFtrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >the City, State you lived with the </span>";
                emailFtrHTML = emailFtrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >FROM and TO dates.  </span>";
                emailFtrHTML = emailFtrHTML + @"</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"</tr> ";
                emailFtrHTML = emailFtrHTML + @"<tr> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"</tr> ";
                emailFtrHTML = emailFtrHTML + @"<tr> ";
                emailFtrHTML = emailFtrHTML + @"<td colspan = ""3"" >";
                emailFtrHTML = emailFtrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Show </span>";
                emailFtrHTML = emailFtrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >STUDENT </span>";
                emailFtrHTML = emailFtrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >if in school </span>";
                emailFtrHTML = emailFtrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >and </span>";
                emailFtrHTML = emailFtrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >the name of the school with the City, State with  </span>";
                emailFtrHTML = emailFtrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >FROM and TO dates.  </span>";
                emailFtrHTML = emailFtrHTML + @"</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"</tr> ";
                emailFtrHTML = emailFtrHTML + @"<tr> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"</tr> ";
                emailFtrHTML = emailFtrHTML + @"<tr> ";
                emailFtrHTML = emailFtrHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" > Thank You,</span></td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"</tr> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"</tr> ";
                emailFtrHTML = emailFtrHTML + @"<tr> ";
                emailFtrHTML = emailFtrHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"</tr> ";
                emailFtrHTML = emailFtrHTML + @"<tr> ";
                emailFtrHTML = emailFtrHTML + @"<td>&nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td>&nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td>&nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"</tr> ";
                emailFtrHTML = emailFtrHTML + @"<td colspan = ""3"" > ";
                emailFtrHTML = emailFtrHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"</tr> ";
                emailFtrHTML = emailFtrHTML + @"<tr> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"</tr> ";
                emailFtrHTML = emailFtrHTML + @"</table> ";
                emailFtrHTML = emailFtrHTML + @"</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"<td> &nbsp;</td> ";
                emailFtrHTML = emailFtrHTML + @"</tr> ";
                emailFtrHTML = emailFtrHTML + @"</table> ";
                emailFtrHTML = emailFtrHTML + @"</div></div>";

            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            var hearderHTML = emailHdrHTML;
            var footerHTML = emailFtrHTML;

            return new Tuple<string, string, string, string>(hearderHTML, footerHTML, strTMEmail, strMgrEmail);

        }

        public Tuple<string, string, string, string> GetExamScheduledHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;

            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED – </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >LICENSE EXAM SCHEDULED</span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >to this email if you have questions. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >As requested, your insurance exam is scheduled. Please see the attached confirmation and </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >follow the directions carefully.  </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >We recommend you print the confirmation and take it with you  </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >to the exam site.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >To complete the licensing process:</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(1)</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Arrive at the exam site 30 minutes early.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(2)</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Take two forms of signature ID (one with a photo) and your pre-licensing </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >certificate with you to the exam site.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(3)</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Take the Exam</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(4)</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >E-mail your score report to me.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(5)</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Submit your request for reimbursement of incurred expenses through Concur.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Company policy requires that you complete the </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Insurance Product and Compliance Training </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Program, Optional Products – Non-Credit Training, </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >prior to being appointed to offer non-credit </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >products.  Insurance Licensing will notify you when you can offer non-credit insurance. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" > Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";

            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetExamScheduledNoCertHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;

            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED - </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >LICENSE EXAM SCHEDULED</span>";
                strHTML = strHTML + @" </td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >to this email if you have questions. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >As requested, your insurance exam is scheduled. Please see the attached confirmation and follow the directions carefully.  </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >We recommend you print the confirmation and take it with you to the exam site. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >To complete the licensing process:</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(1)</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Arrive at the exam site 30 minutes early.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(2)</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Take two forms of signature ID (one with a photo) with you to the exam site. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(3)</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Take the Exam</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(4)</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >E-mail your score report to me.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(5)</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Submit your request for reimbursement of incurred expenses through Concur.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Company policy requires that you complete the </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Insurance Product and Compliance Training</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Program, Optional Products – Non-Credit training</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >, prior to being appointed to offer non-credit </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >products.  Insurance Licensing will notify you when you can offer non-credit insurance.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" > Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""../Pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";

            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetRENLicenseCopyCAHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;

            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >to this email if you have questions or call your Licensing Specialist shown below.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >The purpose of this email is to forward your renewed California Insurance License.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >The California Department of Insurance no longer issues renewed paper licenses.  All renewed </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >licenses must be printed from their website.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                ;
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Post your license in the branch for review by state auditors and examiners.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" > Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";

            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetRENLicenseCopyHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;

            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >to this email if you have questions or call your Licensing Specialist shown below.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Please print the attached license and have available for review by Auditors and Examiners.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" > Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";

            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetRENLicenseCopyGaKyMtWaWyHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >to this email if you have questions or call your Licensing Specialist shown below.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Please print the attached license and have available for review by Auditors and Examiners.  </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Insurance Producer licenses in your state should be conspicuously displayed in the place of</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >business.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" > Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetExamFxRegLifeHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetUmonitoredHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetCEReminderData(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";
                string strCEExpireDate = managerInfo.CEExpireDate?.ToString();

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED TO KEEP YOUR INSURANCE LICENSE IN GOOD STANDING</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >OMS is providing you with online continuing education </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >required </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >to keep your insurance license active. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Please complete before the compliance date of </span>";
                DateTime ceExpireDate;
                if (DateTime.TryParse(strCEExpireDate, out ceExpireDate))
                {
                    strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >" + ceExpireDate.ToString("MM-dd-yyyy") + ". </span> ";
                }
                else
                {
                    // Handle the case where strCEExpireDate is not a valid date
                    strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >" + strCEExpireDate + ". </span> ";
                }
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >You need to: </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(1)</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Go to </span>";
                strHTML = strHTML + @"<span  ><a href=""https://www.adbanker.com"" style = ""font-family: Arial; color: #0000FF; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"">www.adbanker.com</a> </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: italic; text-decoration: none;"" >. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(2)</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Login to your account by clicking on </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: italic; text-decoration: none;"" >Sign In  </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(not Sign Up) in the upper right corner. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Your login </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >is your business e-mail address (</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #0000FF; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >john.smith@omf.com</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >) </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Your password </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >is your first initial upper case, last initial lower case, the # symbol then your </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >7- digit team member number. Example </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >John Smith would be </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Js#l234567</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(3)</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Click on the courses and follow the instructions to complete each.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >Exams are required to be monitored by a disinterested 3rd party.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(4)</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Follow the proctor instructions found in the course and </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >submit the proctor affidavit  </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >on-Line </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >as part of the exam process.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: italic; text-decoration: none;"" >If you are a non-exempt employee who chooses to study outside regular business hours, you should </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: italic; text-decoration: none;"" >get prior approval from your manager and report time accordingly. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >NOTE:  Your certificates should be available immediately.   You can print for your records.  </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >AD Bankers will report completion status directly to the Department of Insurance and to us. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >You do not need to send completion certificates to us.  </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >to this email if you have questions or call your Licensing Specialist shown below.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetMonitoredHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetCEReminderData(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";
                string strCEExpireDate = managerInfo.CEExpireDate?.ToString();

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED TO KEEP YOUR INSURANCE LICENSE IN GOOD STANDING</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >OMS is providing you with online continuing education </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >required </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >to keep your insurance license active. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Please complete before the compliance date of </span>";
                DateTime ceExpireDate;
                if (DateTime.TryParse(strCEExpireDate, out ceExpireDate))
                {
                    strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >" + ceExpireDate.ToString("MM-dd-yyyy") + ". </span> ";
                }
                else
                {
                    // Handle the case where strCEExpireDate is not a valid date
                    strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >" + strCEExpireDate + ". </span> ";
                }
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >You need to: </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(1)</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Go to </span>";
                strHTML = strHTML + @"<span  ><a href=""https://www.adbanker.com"" style = ""font-family: Arial; color: #0000FF; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"">www.adbanker.com</a> </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: italic; text-decoration: none;"" >. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(2)</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Login to your account by clicking on </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: italic; text-decoration: none;"" >Sign In  </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(not Sign Up) in the upper right corner. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Your login </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >is your business e-mail address (</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #0000FF; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >john.smith@omf.com</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >) </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Your password </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >is your first initial upper case, last initial lower case, the # symbol then your </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >7- digit team member number. Example </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >John Smith would be </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Js#l234567</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(3)</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Click on the courses and follow the instructions to complete each.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >Exams are required to be monitored by a disinterested 3rd party.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(4)</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Follow the proctor instructions found in the course and </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >submit the proctor affidavit  </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >on-Line </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >as part of the exam process.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: italic; text-decoration: none;"" >If you are a non-exempt employee who chooses to study outside regular business hours, you should </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: italic; text-decoration: none;"" >get prior approval from your manager and report time accordingly. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >NOTE:  Your certificates should be available immediately.   You can print for your records.  </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >AD Bankers will report completion status directly to the Department of Insurance and to us. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >You do not need to send completion certificates to us.  </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >to this email if you have questions or call your Licensing Specialist shown below.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetMonitoredInHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetCEReminderData(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";
                string strCEExpireDate = managerInfo.CEExpireDate?.ToString();

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED TO KEEP YOUR INSURANCE LICENSE IN GOOD STANDING</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >OMS is providing you with online continuing education </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >required </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >to keep your insurance license active. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Please complete before the compliance date of </span>";
                DateTime ceExpireDate;
                if (DateTime.TryParse(strCEExpireDate, out ceExpireDate))
                {
                    strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >" + ceExpireDate.ToString("MM-dd-yyyy") + ". </span> ";
                }
                else
                {
                    // Handle the case where strCEExpireDate is not a valid date
                    strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >" + strCEExpireDate + ". </span> ";
                }
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >To complete your CE, you need to: </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(1)</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Go to </span>";
                strHTML = strHTML + @"<span  ><a href=""https://www.adbanker.com"" style = ""font-family: Arial; color: #0000FF; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"">www.adbanker.com</a> </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: italic; text-decoration: none;"" >. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(2)</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Login to your account by clicking on </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: italic; text-decoration: none;"" >Sign In  </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(not Sign Up) in the upper right corner. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Your login </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >is your business e-mail address (</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #0000FF; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >john.smith@omf.com</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >) </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Your password </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >is your first initial upper case, last initial lower case, the # symbol then your </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >7- digit team member number. Example </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >John Smith would be </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Js#l234567</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(3)</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Click on the courses and follow the instructions to complete each.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >Exams are required to be monitored by a licensed insurance producer.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(4)</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Follow the proctor instructions found in the course and </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >submit the proctor affidavit  </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >on-Line </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >as part of the exam process.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: italic; text-decoration: none;"" >If you are a non-exempt employee who chooses to study outside regular business hours, you should </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: italic; text-decoration: none;"" >get prior approval from your manager and report time accordingly. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >NOTE:  Your certificates should be available immediately.   You can print for your records.  </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >AD Bankers will report completion status directly to the Department of Insurance and to us. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >You do not need to send completion certificates to us.  </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >to this email if you have questions or call your Licensing Specialist shown below.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetWebinarIlHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >As part of your continuing education requirement, you must take a three-hour Ethics course.  A. D. Banker </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >is now offering webinars.   </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >To view the available courses, please go to the following link: </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span  ><a href=""https://www.adbanker.com"" style = ""font-family: Arial; color: #0000FF; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"">www.adbanker.com</a> </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Click on Continuing Education.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Select IL</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Click on Webinar and scroll down.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >View the </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >three-hour courses </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >that state in red print </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >''Satisfies Ethics Requirement''.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >Please contact me when you decide, and I will schedule. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Once scheduled, you will receive an email confirmation directly from </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #0000FF; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >notify2@adbanker.com. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >On the day of the seminar, you should receive another email from </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >GoToTraining </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >that will include a link and </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >instructions to access the course.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >If you have any questions, please contact me or A.D. Banker at 800-866-2468.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >to this email to expedite your licensing process. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetLifePLSHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please REPLY to this email </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >ONLY</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" > without copying your Licensing Specialist to expedite your </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >licensing process.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >We received a request for you to start the Life license process.  The Life license requires </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >passing</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >a state insurance exam and generally a </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >minimum</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" > of 20 hours of insurance related study</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >. OMS </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Licensing provides an online training course from an industry leader, and they recommend that the </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >student routinely dedicate at least </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >one to two hours</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > each day of concentrated study to complete the </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: italic; text-decoration: none;"" >course quickly for maximum retention.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Please discuss the on-line training with your Branch Manager and choose a time when you can </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >dedicate a couple of hours a day for several weeks to complete the training.  Reply to this email when </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >you are ready to begin training and I will register you.  </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >The course starts at registration, </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: underline;"" >not</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > your initial </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >access to the course and renewals are available with management approval and additional cost to the </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >company that may be charged to the branch.  The course offers various study styles and </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >unlimited </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >practice exams </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >to gauge the likelihood of passing the state exam.  </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Progress reports are provided to the Senior Managing Directors and Regional Directors that show your </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >progress.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >We are attaching instructions and application related documents that </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >need to be </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >FULLY </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >completed and returned before you can become licensed.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetLifePlsPlusHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please REPLY to this email </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >ONLY</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" > without copying your Licensing Specialist to expedite your </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >licensing process. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >You have a job title that requires you to hold a life license to sell non-credit insurance to OMF </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >customers.  The Life license requires </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >passing a state insurance exam and generally a </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >minimum </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >of 20 hours of insurance related study</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Please go to the link below to view the available locations and dates. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span  ><a href=""https://www.adbanker.com/pre-licensing.aspx#.IL.LH.1"" style = ""font-family: Arial; color: #0000FF; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"">www.adbanker.com/pre-licensing...</a> </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Click on Web Class</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Review the dates</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >For </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: underline;"" >Life only</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >, you would only need the </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: underline;"" >first day</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > of a two-day class from 8:00am to 5:30pm.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >PLEASE DO NOT SCHEDULE - Please let me know when you decide, and I will schedule the </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >webinar and order the online tools.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >Important</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > – There is an online course as part of the study package and AD Banker would like </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >you to get as far into the online course as possible prior to your class date so when you go to </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >class, it is more like a review.  After the class you would continue to study the online tools </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >and would need to pass their Comprehensive Exam with an 80% before scheduling the state </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >exam.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetOkToSELLHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var okayToSellInfo = GetOkayToSellData(vEmploymentID);

                bool isCalState = okayToSellInfo.SellStates.Any(state => state.StateProvinceName.Equals("CALIFORNIA", StringComparison.OrdinalIgnoreCase));

                string strEffectiveDate = string.Empty;
                string strTMName = okayToSellInfo.TMName ?? "";
                string strTMNumber = okayToSellInfo.TMNumber ?? "";
                string strTMTitle = okayToSellInfo.TMTitle ?? "";
                string strLicenseTechName = okayToSellInfo.LicenseTechName ?? "";
                string strLicenseTechTitle = okayToSellInfo.LicenseTechTitle ?? "";
                string strLicenseTechPhone = okayToSellInfo.LicenseTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";

                foreach (var item in okayToSellInfo.SellStates)
                {
                    //strLicenseState = item.StateProvinceName;

                    strHTML = strHTML + @"<tr> ";
                    strHTML = strHTML + @"<td  colspan = ""3"" ><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 700; font-style: normal; text-decoration: none;"" >" + item.StateProvinceName + "</span></td>";
                    strHTML = strHTML + @"<td> &nbsp;</td> ";
                    strHTML = strHTML + @"<td> &nbsp;</td> ";
                    strHTML = strHTML + @"</tr> ";
                    foreach (var license in okayToSellInfo.licenseStateItems)
                    {
                        if (license.StateAbbr == item.StateProvinceAbv)
                        {
                            strHTML = strHTML + @"<tr> ";
                            strHTML = strHTML + @"<td  colspan = ""2"" ><span style = ""padding-top: 1rem; font-family: Arial; color: #000000; font-size: 12pt; font-weight: 500; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;" + license.LineOfAuthorityName + " - LICENSE NUMBER: " + license.LicenseNumber + "</span></td>";
                            strHTML = strHTML + @"<td> &nbsp;</td> ";
                            strHTML = strHTML + @"<td> &nbsp;</td> ";
                            strHTML = strHTML + @"</tr> ";
                            foreach (var effective in okayToSellInfo.licenseEffectiveDates)
                            {
                                if (effective.EmployeeLicenseID == license.EmployeeLicenseID)
                                {
                                    strEffectiveDate = effective.AppointmentEffectiveDate.ToString();
                                    strHTML = strHTML + @"<tr> ";
                                    DateTime effectiveDate;
                                    if (DateTime.TryParse(strEffectiveDate, out effectiveDate))
                                    {
                                        strHTML = strHTML + @"<td><span style = ""margin-left: 30px; font-family: Arial; color: #000000; font-size: 10pt; font-weight: 300; font-style: normal; text-decoration: none;"" >" + effective.CompanyName + " - EFFECTIVE DATE: " + effectiveDate.ToString("MM-dd-yyyy") + "</span></td>";
                                    }
                                    else
                                    {
                                        strHTML = strHTML + @"<td><span style = ""margin-left: 30px; font-family: Arial; color: #000000; font-size: 10pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >" + effective.CompanyName + " - EFFECTIVE DATE: " + effectiveDate + ". </span></td>";
                                    }
                                    strHTML = strHTML + @"</tr> ";
                                }
                            }
                        }
                    }
                }

                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >You are now authorized to offer insurance products covered under your license(s) shown above. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Remember that you are responsible for knowing the licensing requirements of your state and </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >keeping your license (or registration for Auto Club in certain states) in good standing by </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >completing mandatory continuing education if applicable, and/or submitting renewal </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >documents within the time frame required by the Department of Insurance (DOI) in your state. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >You must also keep the DOI and us advised of your current resident address or change in </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >criminal history.  </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >Any felony or crime of moral turpitude, criminal convictions, or enforcement actions taken by </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >any state regulatory authority must be reported to us and to the DOI within 10 - 30 days, </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >depending on the laws of your state. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";

                if (isCalState)
                {
                    strHTML = strHTML + @"<tr> <td colspan = ""3"">";
                    strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; background-color: #FFFF00;"" >California law now requires individuals with Life and Health licenses to include their license number in their email signature. </span>";
                    strHTML = strHTML + @"</td> </tr>";
                    strHTML = strHTML + @"<tr> <td colspan = ""3""> &nbsp;</td> </tr>";

                    strHTML = strHTML + @"<tr> <td colspan = ""3"">";
                    strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal;"" >According to this law, there are certain requirements on size and placement of the license number in the email signature. These requirements include:</span>";
                    strHTML = strHTML + @"</td> </tr>";
                    strHTML = strHTML + @"<tr> <td colspan = ""3""> &nbsp;</td> </tr>";

                    strHTML = strHTML + @"<tr> <td colspan = ""3"">";
                    strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                    strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"">The license number must be in a type size no smaller than the largest of any street address, email address, or telephone number of the licensee. For example, if an email included a 10-point street address, an 11-point email address, and a 12-point telephone number, then the license number must be at least 12-point.</span> ";
                    strHTML = strHTML + @"</td> </tr>";
                    strHTML = strHTML + @"<tr> <td colspan = ""3""> &nbsp;</td> </tr>";

                    strHTML = strHTML + @"<tr> <td colspan = ""3"">";
                    strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                    strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"">The license number of an individual licensee must appear adjacent to or on the line below the individuals name or title. </span> ";
                    strHTML = strHTML + @"</td> </tr>";
                    strHTML = strHTML + @"<tr> <td colspan = ""3""> &nbsp;</td> </tr>";

                    strHTML = strHTML + @"<tr> <td colspan = ""3"">";
                    strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal;"" >Below is an example of how a signature should look: </span>";
                    strHTML = strHTML + @"</td> </tr>";
                    strHTML = strHTML + @"<tr> <td colspan = ""3""> &nbsp;</td> </tr>";

                    strHTML = strHTML + @"<tr><td colspan = ""3""> ";
                    strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Jane Doe</span>";
                    strHTML = strHTML + @"</td></tr> ";
                    strHTML = strHTML + @"<tr><td colspan = ""3"">";
                    strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Personal Loan Specialist</span>";
                    strHTML = strHTML + @"</td></tr> ";
                    strHTML = strHTML + @"<tr><td colspan = ""3"">";
                    strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Life/Health Insurance License #0M44444</span>";
                    strHTML = strHTML + @"</td></tr> ";
                    strHTML = strHTML + @"<tr><td colspan = ""3"">";
                    strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >1234 University Dr. Anytown, CA 98765</span>";
                    strHTML = strHTML + @"</td></tr> ";
                    strHTML = strHTML + @"<tr><td colspan = ""3"">";
                    strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >P: 555-111-8888</span>";
                    strHTML = strHTML + @"</td></tr> ";
                    strHTML = strHTML + @"<tr><td colspan = ""3"">";
                    strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >F: 333-999-1111</span>";
                    strHTML = strHTML + @"</td></tr> ";
                    strHTML = strHTML + @"<tr> <td colspan = ""3""> &nbsp;</td> </tr>";

                    strHTML = strHTML + @"<tr><td colspan = ""3"" >";
                    strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >If you have any questions regarding your license, please contact your Licensing Specialist shown below</span>";
                    strHTML = strHTML + @"</td><td> &nbsp;</td><td> &nbsp;</td></tr> ";
                }
                else
                {
                    strHTML = strHTML + @"<td colspan = ""3"" >";
                    strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >MD Licensees Only</span>";
                    strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >: This document shall be retained while your appointment is in effect and for </span>";
                    strHTML = strHTML + @"</td> ";
                    strHTML = strHTML + @"<td> &nbsp;</td> ";
                    strHTML = strHTML + @"<td> &nbsp;</td> ";
                    strHTML = strHTML + @"</tr> ";
                    strHTML = strHTML + @"<tr> ";
                    strHTML = strHTML + @"<td colspan = ""3"" >";
                    strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >at least 5 years after the termination of your appointment.</span>";
                    strHTML = strHTML + @"</td> ";
                    strHTML = strHTML + @"<td> &nbsp;</td> ";
                    strHTML = strHTML + @"<td> &nbsp;</td> ";
                    strHTML = strHTML + @"</tr> ";
                    strHTML = strHTML + @"<tr> ";
                    strHTML = strHTML + @"<td> &nbsp;</td> ";
                    strHTML = strHTML + @"<td> &nbsp;</td> ";
                    strHTML = strHTML + @"<td> &nbsp;</td> ";
                    strHTML = strHTML + @"</tr> ";
                    strHTML = strHTML + @"<tr> ";
                    strHTML = strHTML + @"<td colspan = ""3"" >";
                    strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >If you have any questions regarding your license, please contact your Licensing Specialist shown </span>";
                    strHTML = strHTML + @"</td> ";
                    strHTML = strHTML + @"<td> &nbsp;</td> ";
                    strHTML = strHTML + @"<td> &nbsp;</td> ";
                    strHTML = strHTML + @"</tr> ";
                    strHTML = strHTML + @"<tr> ";
                    strHTML = strHTML + @"<td colspan = ""3"" >";
                    strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >below. </span>";
                    strHTML = strHTML + @"</td> ";
                    strHTML = strHTML + @"<td> &nbsp;</td> ";
                    strHTML = strHTML + @"<td> &nbsp;</td> ";
                    strHTML = strHTML + @"</tr> ";
                }

                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";

                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" > Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" ><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetBackgroundReleaseHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" > to this email attaching the requested document to expedite processing of your insurance license application.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Fully complete the attached Background Authorization Release form for us to process your license application. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetBackgroundDisclosureLinkHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" > to this email if you have questions.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >In accordance with the Department of Insurance regulations, we are required to obtain a </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >background investigation on prospective licensees.  For the licensing process to continue, please </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >follow the link shown below and complete the background check disclosure.  </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >If the Additional Disclosure doesn’t come up for your viewing to attest, attached is the </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >disclosure for your reference. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >You will then receive a notice from HireRight requesting information for your report.  Thanks!</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span  ><a href=""https://onemainfinancial.csod.com/samldefault.aspx?returnUrl=%252fDeepLink%252fProcessRedirect.aspx%253fmodule%253dlodetails%2526lo%253d57df6e4e-f76c-4edc-a59d-b252f5f5faac"" style = ""font-family: Arial; color: #0000FF; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"">Background Disclosure Link</a> </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetExamScheduledCreditHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED - </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >LICENSE EXAM SCHEDULED</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" > to this email if you have questions.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >As requested, your insurance exam is scheduled. Please see the attached confirmation and </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >follow the directions carefully. </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >We recommend you print the confirmation and take it with you </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >to the exam site.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >To complete the licensing process:</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(1)</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Arrive at the exam site 30 minutes early.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(2)</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Take two forms of signature ID (one with a photo) with you to the exam site.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(3)</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Take the Exam</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(4)</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >E-mail your score report to me.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(5)</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Submit your request for reimbursement of incurred expenses through Concur.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Company policy requires that you complete the </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Insurance Product and Compliance Training </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Program, Optional Products – Credit, Marketing and Sales training,  </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >prior to being appointed to </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >offer credit products.  Insurance Licensing will notify you when you can offer credit insurance.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetClearenceLetterHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Please print and complete the attached form.  </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" > to this email with the FULLY completed documents.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetLifePLsPlusILHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please REPLY to this email </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >ONLY</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" > without copying your Licensing Specialist to expedite your </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >licensing process. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >We received a request to start you on the Life licensing process.  The Life license requires</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >passing a state insurance exam and generally a </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >minimum</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" > of 20 hours of insurance related </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >study.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Please go to the link below to view the available locations and dates. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span  ><a href=""https://www.adbanker.com/pre-licensing.aspx#.IL.LH.1"" style = ""font-family: Arial; color: #0000FF; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"">www.adbanker.com/pre-licensing...</a> </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Click on Web Class</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Review the dates</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >For </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: underline;"" >Life only</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >, you would only need the </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: underline;"" >first day</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > of a two-day class from 8:00am to 5:30pm.  </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >PLEASE DO NOT SCHEDULE - Please let me know when you decide, and I will schedule the </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >webinar and order the online tools.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >Important</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >– There is an online course as part of the study package and AD Banker would like</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >you to get as far into the online course as possible prior to your class date so when you go to</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >class, it is more like a review.  After the class you would continue to study the online tools </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >and would need to pass their Comprehensive Exam with an 80% before scheduling the state </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >exam.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetChildSupportHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" > to this email attaching the requested documents to expedite processing of your </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >license application.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >We received your insurance application and additional documents are required for your license </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >application to be processed.  </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: Underline;"" >NOTE:</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > For Question 7, If you answer yes to any of these questions a, b, or c, you must attach to </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >this application - documentation showing proof of current payments or an approved repayment</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >plan from the appropriate state child support agency.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >a) by how many months are you in arrearage? </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >b) are you currently subject to and in compliance with repayment agreement? </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >c) are you subject of a child support related subpoena/warrant?</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetCitizenDocumentHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" > to this email attaching the described supporting documents to expedite </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >processing of your license application.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Your insurance application has been received and additional documents are required by the </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Department of Insurance for you to get licensed.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >If you answer </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >“NO” </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > to question 16, you must attach to this application:</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >a) which country are you a citizen? </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >b) supply proof of eligibility to work in the U.S. and State ID.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetNotoryMissingHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" > to this email attaching the requested documents to expedite processing of your </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >license application.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Your insurance application has been received.  The application requires the notary signature &</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >seal which is missing. </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Please have notarized and attach to your reply.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetNotoryMissingTnHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" > to this email attaching the requested documents to expedite processing of your </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >license application.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Your insurance application has been received.  The 2nd page of the motor club application</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >requires notary signature & seal which is missing. </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Please have notarized and overnight mail</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >original </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >to the attention of your licensing specialist for processing:</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >OneMain Solutions</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Attn: Licensing Dept.</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >3001 Meacham Blvd, Suite 100</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Fort Worth, TX 76137</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetApplicationRequiredHIHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" > to this email attaching the requested documents to expedite processing of your </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >license application.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Attached is the paperwork required to become licensed.  Upon full completion, </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >scan, attach,</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >and reply</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >.  Then </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >overnight mail originals</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > to the attention of your licensing specialist for </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >processing:</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >OneMain Solutions</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Attn: Licensing Dept.</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >3001 Meacham Blvd, Suite 100</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Fort Worth, TX 76137</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please include copy of your drivers’ license.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetApplicationRequiredTNHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" > to this email attaching the requested documents to expedite processing of your</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >license application.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Attached is the paperwork required to become licensed.  Upon full completion, </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >scan, attach,</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >and reply</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >.  Then </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >overnight mail originals</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > to the attention of your licensing specialist for </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >processing:</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >OneMain Solutions</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Attn: Licensing Dept.</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >3001 Meacham Blvd, Suite 100</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Fort Worth, TX 76137</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please note, the 2nd page of the motor club application must be </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >signed & notarized with seal.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetFingerprintRequiredAZHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" > to this email with the requested information to expedite processing of your </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >license application.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Fingerprints are required to be submitted to the Arizona Department of Insurance if you are an </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Arizona resident and do not already hold an Arizona insurance license or relocating to Arizona </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >from another state.  </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Attached is a list of locations of where you can get fingerprinted. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Please advise which location is closer to you.</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Are you available any day of the week to get fingerprinted? </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Do you want morning or afternoon?  </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Once you get fingerprinted, overnight them to the address below along with a copy of your </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >driver’s license to identify who the prints belong to.</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Overnight mail to the address below:  </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >OneMain Solutions</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Attn: Licensing Dept.</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >3001 Meacham Blvd, Suite 100</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Fort Worth, TX 76137</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Once your fingerprints are received, your application will be submitted electronically, and your </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >prints will be forwarded to the state.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetFingerprintRequiredLAHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" > to this email to expedite processing of your license application.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";

                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >I have attached the fingerprinting locations and instructions.  Please select location and complete within the </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >next week.  Upon completion, send copy of receipt to me for processing.   Please request payment </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >reimbursement through concur. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<br /><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >If you are unable to self-pay and request reimbursement, contact your licensing specialist to request a check </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >for fingerprinting fees. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetFingerprintRequiredCreditHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" > to this email with the requested information to expedite processing of your</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >license application.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >The Department of Insurance requires that you be fingerprinted for a credit insurance license.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Please advise which location, day and time you prefer so you can be scheduled, and payment </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >made on your behalf. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetFingerprintScheduledALHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >The fingerprint receipt is attached with a list of locations. Please choose location and have </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >fingerprints completed at your earliest convenience.  </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Click the company name for site </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >information.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" > to this email if you have questions. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetFingerprintScheduledTNHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Please take the following items to the fingerprint site with you:</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >1)  Fingerprint Confirmation </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >(see attached)</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >2)  Valid Driver’s License</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >3)  Payment in the amount listed on the attached confirmation via Credit Card, Certified Check or Money Order.  (if you use your own means of payment, you can request reimbursement through Concur). </span>";
                //strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >$35.15 </span>";
                //strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >via Cashier Check, Money Order or Credit Card.   (if you use </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                //strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >your own means of payment, you can request reimbursement through Concur).</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" > to this email if you have questions. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetFingerprintScheduledNMHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" > to this email once you have been fingerprinted to expedite submission. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";

                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >The Office of Superintendent of Insurance requires a person to be fingerprinted for any </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >insurance license if they have never held an insurance license in New Mexico or if it has been </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >over two years since you were fingerprinted for licensing by the state.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Attached is the fingerprint location and your receipt that you must take with you for </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >fingerprinting.  If you fail to take the receipt with you, they will turn you away.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";

            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetFingerprintScheduledPAHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Please take the following items to the fingerprint site with you:</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >1)  Fingerprint Confirmation </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >(see attached)</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >2)  Valid Driver’s License</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >3) Payment in the amount listed on the attached confirmation via Business Check, Money Order or Credit/Debit Card(if you use your own means of payment, you can request reimbursement through Concur). </span>";
                //strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >$23.85 </span>";
                //strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >via Business Check, Money Order or Credit/Debit Card. (if </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                //strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >you use your own means of payment, you can request reimbursement through Concur).</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" > to this email if you have questions. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetComplianceCertificateHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<html> ";
                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" > to this email with the completed form to expedite your licensing process.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >To finalize your licensing process, the Company requires you sign a Certificate of Compliance </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >which is attached for your convenience.  </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
                strHTML = strHTML + @"</html> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetComplianceCertificateEndHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<html> ";
                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" > to this email with the completed form to expedite your licensing process.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >To finalize your licensing process, the Company requires you sign a Certificate of Compliance </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >which is attached for your convenience.  </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
                strHTML = strHTML + @"</html> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetLifePlsILHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<html> ";
                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200;  padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please REPLY to this email </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >ONLY</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" > without copying your Licensing Specialist to expedite your </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >licensing process. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >We received a request to start you on the Life licensing process.  The Life license requires</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >passing a state insurance exam and generally a </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >minimum</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" > of 20 hours of insurance related </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >study.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Please go to the link below to view the available locations and dates. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span  ><a href=""https://www.adbanker.com/pre-licensing.aspx#.IL.LH.1"" style = ""font-family: Arial; color: #0000FF; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"">www.adbanker.com/pre-licensing...</a> </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Click on Web Class</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Review the dates</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >For </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: underline;"" >Life only</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >, you would only need the </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: underline;"" >first day</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > of a two-day class from 8:00am to 5:30pm.  </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >PLEASE DO NOT SCHEDULE - Please let me know when you decide, and I will schedule the </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >webinar and order the online tools.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >Important</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >– There is an online course as part of the study package and AD Banker would like</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >you to get as far into the online course as possible prior to your class date so when you go to</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >class, it is more like a review.  After the class you would continue to study the online tools </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >and would need to pass their Comprehensive Exam with an 80% before scheduling the state </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >exam.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
                strHTML = strHTML + @"</html> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetFingerprintRequiredUTHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<html> ";
                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" > to this email to expedite processing of your license application.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Your insurance application has been received and fingerprints are required.  Please take the </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >following items to the fingerprint site with you:</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >1)  SIRCON Application Confirmation </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >(see attached)</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >2)  Valid Driver’s License</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >3)  Payment in the amount of </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >$20.00 </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >via Cashier Check, Money Order or Credit Card.   (if you use </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >your own means of payment, you can request reimbursement through Concur).</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please complete within a week from today to avoid state delays.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Once you’ve been fingerprinted, please advise </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >location and date of fingerprinting</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > as needed by</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >the state.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
                strHTML = strHTML + @"</html> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetADBankerRegistrationILHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<html> ";
                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED - YOUR TRAINING COURSE IS ACTIVATED</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >You have been scheduled for a Life only webinar as requested.  It should show on your </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >dashboard in the online account.  AD Banker suggests completing the online course or getting as </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >much completed as possible prior to attending the webinar because they consider the webinar </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >to be more of a review.  When ready to take the webinar, while logged into your account, </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >can click on “Retake Webinar” to schedule your Life only webinar.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Before/After attending the webinar, you will need to pass the </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >Certification Exam</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > located in the </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >online course as part of the required pre-licensing to earn the certificate.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >OMS is providing you with an online training course from AD Banker, one of the leaders in the </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >industry to help prepare you for the state license exam.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >•	Most students need a </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >minimum of 15 hours</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > to read the insurance topics and an </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >additional </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >5 hours for Chapter Quizzes and Comprehensive Exams</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >.</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >•	The enrollment to the course starts</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal;font-style: italic; text-decoration: none;"" > TODAY</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >.</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >•	For better retention complete the course quickly. </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >20 or 30 days</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > is recommended.</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >•	</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >80%</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > is required on the </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >Comprehensive Exam</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > to schedule the state license exam.</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >The online course includes:</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >•	Video Lectures, Flash Cards, Unlimited Chapter and Comprehensive Exams</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >•	Comprehensive Exams, Drag & Drop Learning Activities, Guarantee to Pass</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >•	Printable PDF Study Manual, Search terms and phrases, Support from Licensing</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Specialists</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >•	Online Note Taking, Mobile friendly, Audio Enabled.</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACCESS</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > the course, at </span>";
                strHTML = strHTML + @"<span  ><a href=""http://www.adbanker.com/"" style = ""font-family: Arial; color: #0000FF; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"">www.adbanker.com</a> </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >.</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >LOG IN</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > is your business e-mail address. </span>";
                //strHTML = strHTML + @" </ td > ";
                //strHTML = strHTML + @"<td> &nbsp;</td> ";
                //strHTML = strHTML + @"<td> &nbsp;</td> ";
                //strHTML = strHTML + @"</tr> ";
                //strHTML = strHTML + @"<tr> ";
                //strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Example: </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >jane.doe@</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >omf</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >.com</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" > (NOT @Onemainfinancial.com)</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >.</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";

                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >PASSWORD</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >  is your first initial upper case, last initial lower case, the # symbol then your 7-digit team member number.</span>";
                //strHTML = strHTML + @" </ td > ";
                //strHTML = strHTML + @"<td> &nbsp;</td> ";
                //strHTML = strHTML + @"<td> &nbsp;</td> ";
                //strHTML = strHTML + @"</tr> ";
                //strHTML = strHTML + @"<tr> ";
                //strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Example: </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">John Smith</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > would be </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >Js #1234567</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >.</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >DO NOT CHANGE THE ASSIGNED BUSINESS EMAIL OR PASSWORD</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >CHOOSE </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold font-style: normal; text-decoration: none;"" >LAUNCH </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >Illinois Life Online Course</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >.</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >READ </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >State specific requirements are covered in the course requirements.</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >CONTACT </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold font-style: normal; text-decoration: none;"" >AD Banker at 800-866-2468</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > if you have difficulty accessing the website or need </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >information about the content. </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Course certification exams may be taken without a proctor provided that the student presents </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >an affidavit certifying that no written materials were consulted, and no outside assistance of </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >any kind was received from any person during the exam. </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Training/course study time is paid and should be completed during normal company hours or </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >pre-approved overtime. </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" > to this email </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >ONLY</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" > without copying your Licensing Specialist if you have </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >questions or call your Licensing Specialist shown below. </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
                strHTML = strHTML + @"</html> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetExamScheduledPAHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<html> ";
                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED – </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"" >LICENSE EXAM SCHEDULED</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >to this email if you have questions. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >As requested, your insurance exam is scheduled.  You should receive an email confirmation </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >directly from PSI.  </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >We recommend you print the confirmation and take it with you to the exam </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >site.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >To complete the licensing process:</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(1)	Arrive at the exam site 30 minutes early.</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(2)	You are required to bring one (1) form of valid (non-expired) signature bearing </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >identification to the test site.  See exam confirmation for a list of allowed </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >documents.  </span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(3)	PSI Exam Confirmation</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(4)	Pre-licensing Course Certificate of Completion</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(5)	Take the Exam</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(6)	Email your score report to me.</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >(7)	Submit your request for reimbursement of incurred expenses through Concur.</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Company policy requires that you complete the </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Insurance Product and Compliance Training </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Program, Optional Products – Non-Credit Training</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >, prior to being appointed to offer non-credit </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >products.  Insurance Licensing will notify you when you can offer non-credit insurance. </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
                strHTML = strHTML + @"</html> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetAddressChangeHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<html> ";
                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Our records show your address changed.  Please complete the attached form and scan/email </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >back to me for processing.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" > to this email with the documents to expedite your licensing process.  </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
                strHTML = strHTML + @"</html> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetNameChangeILHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var nameChangeInfoInfo = GetNameChangeInfo(vEmploymentID);

                string strTMName = nameChangeInfoInfo.TMName ?? "";
                string strTMNumber = nameChangeInfoInfo.TMNumber ?? "";
                string strTMTitle = nameChangeInfoInfo.TMTitle ?? "";
                string strLicenseTechName = nameChangeInfoInfo.LicTechName ?? "";
                string strLicenseTechTitle = nameChangeInfoInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = nameChangeInfoInfo.LicTechPhone ?? "";
                string strNameOld = nameChangeInfoInfo.NameOld ?? "";
                string strNameNew = nameChangeInfoInfo.NameNew ?? "";

                strHTML = strHTML + @"<html> ";
                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" > to this email to expedite handling.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">Our records show your name changed from </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >" + strNameOld + "</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;""> to </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >" + strNameNew + ". </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Please do the following to change your name with the state for your license:</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >1)  Print the attached letter, check it for accuracy and sign it.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >2)  Email the letter along with one of the choices of proof of name change to </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span  ><a href=""DOI.Licensing@illinois.gov"" style = ""font-family: Arial; color: #0000FF; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline;"">DOI.Licensing@illinois.gov </a> </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >Please copy me on this email</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > so I know to follow up with the state </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >and print a license showing the new name.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
                strHTML = strHTML + @"</html> ";
            }
            catch (Exception ex)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, ex.Message);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        public Tuple<string, string, string, string> GetFingerprintScheduledGAHTML(int vEmploymentID)
        {
            string strHTML = string.Empty;
            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                string strTMName = managerInfo.TMName ?? "";
                string strTMNumber = managerInfo.TMNumber ?? "";
                string strTMTitle = managerInfo.TMTitle ?? "";
                string strLicenseTechName = managerInfo.LicTechName ?? "";
                string strLicenseTechTitle = managerInfo.LicTechTitle ?? "";
                string strLicenseTechPhone = managerInfo.LicTechPhone ?? "";

                strHTML = strHTML + @"<html> ";
                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200; padding: 5px;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">To: </span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">&nbsp;&nbsp;&nbsp;</span> ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"">" + strTMName + " - " + strTMNumber + " - " + strTMTitle + " </span> ";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >ACTION REQUIRED</span>";
                strHTML = strHTML + @" </ td > ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >Please </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >REPLY</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" > to this email with the TCN# the fingerprint center provides you.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >Please contact </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > <input id = ""Text1"" type = ""text"" name = ""Text1"" style = ""width: 200px; height: 23px"" /></span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > to schedule your appointment for fingerprinting.</span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >•	Take the attached registration with you.  </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" >";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >•	Try to go within the next week.  </span>";
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >Thank You,</span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td><img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg"" width = ""100""/></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"<td>&nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<td colspan = ""3"" > ";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" >" + strLicenseTechName + " - " + strLicenseTechTitle + " - " + strLicenseTechPhone + " </span></td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
                strHTML = strHTML + @"</html> ";
            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }



        #region "Get Local Data"
        protected ManagerInfo GetManagerInfo(int vEmploymentID)
        {
            var result = (from e in _db.Employees
                          join m in _db.Employments on e.EmployeeId equals m.EmployeeId
                          where m.EmploymentId == vEmploymentID
                          join ej in _db.EmploymentJobTitles on m.EmploymentId equals ej.EmploymentId
                          where ej.IsCurrent == true
                          join j in _db.JobTitles on ej.JobTitleId equals j.JobTitleId
                          from m1 in _db.Employments.Where(m1 => m.H1employmentId == m1.EmploymentId.ToString()).DefaultIfEmpty()
                          from ej1 in _db.EmploymentJobTitles.Where(ej1 => m1.EmploymentId == ej1.EmploymentId && ej1.IsCurrent).DefaultIfEmpty()
                          from j1 in _db.JobTitles.Where(j1 => ej1.JobTitleId == j1.JobTitleId).DefaultIfEmpty()
                          from e1 in _db.Employees.Where(e1 => m1.EmployeeId == e1.EmployeeId).DefaultIfEmpty()
                          from m2 in _db.Employments.Where(m2 => m1.H1employmentId == m2.EmploymentId.ToString()).DefaultIfEmpty()
                          from ej2 in _db.EmploymentJobTitles.Where(ej2 => m2.EmploymentId == ej2.EmploymentId && ej2.IsCurrent).DefaultIfEmpty()
                          from j2 in _db.JobTitles.Where(j2 => ej2.JobTitleId == j2.JobTitleId).DefaultIfEmpty()
                          from e2 in _db.Employees.Where(e2 => m2.EmployeeId == e2.EmployeeId).DefaultIfEmpty()
                          from m3 in _db.Employments.Where(m3 => m2.H1employmentId == m3.EmploymentId.ToString()).DefaultIfEmpty()
                          from ej3 in _db.EmploymentJobTitles.Where(ej3 => m3.EmploymentId == ej3.EmploymentId && ej3.IsCurrent).DefaultIfEmpty()
                          from j3 in _db.JobTitles.Where(j3 => ej3.JobTitleId == j3.JobTitleId).DefaultIfEmpty()
                          from e3 in _db.Employees.Where(e3 => m3.EmployeeId == e3.EmployeeId).DefaultIfEmpty()
                          from th in _db.TransferHistories.Where(th => m.EmploymentId == th.EmploymentId && th.IsCurrent).DefaultIfEmpty()
                          from s in _db.StateProvinces.Where(s => th.WorkStateAbv == s.StateProvinceAbv).DefaultIfEmpty()
                          from lt in _db.LicenseTeches.Where(lt => s.LicenseTechId == lt.LicenseTechId).DefaultIfEmpty()
                          select new ManagerInfo
                          {
                              H3MgrEmploymentID = m3.EmploymentId,
                              H3MgrName = (e3.LastName ?? "") + ", " + (e3.FirstName ?? ""),
                              H3MgrTitle = j3.JobTitle1 ?? "",
                              H3MgrEmail = m3.Email ?? "",
                              H2MgrEmploymentID = m2.EmploymentId,
                              H2MgrName = (e2.LastName ?? "") + ", " + (e2.FirstName ?? ""),
                              H2MgrTitle = j2.JobTitle1 ?? "",
                              H2MgrEmail = m2.Email ?? "",
                              MgrEmploymentID = m1.EmploymentId,
                              MgrName = (e1.LastName ?? "") + ", " + (e1.FirstName ?? ""),
                              MgrTitle = j1.JobTitle1 ?? "",
                              MgrEmail = m1.Email ?? "",
                              LicTechName = (lt.LastName ?? "") + ", " + (lt.FirstName ?? ""),
                              LicTechTitle = "LICENSING SPEC",
                              LicTechEmail = lt.LicenseTechEmail ?? "",
                              LicTechPhone = lt.LicenseTechPhone ?? "",
                              TMName = (e.LastName ?? "") + ", " + (e.FirstName ?? ""),
                              TMNumber = e.Geid,
                              TMTitle = j.JobTitle1 ?? "",
                              TMEmail = m.Email ?? ""
                          }).FirstOrDefault();

            return result ?? new ManagerInfo();

        }
        protected CEReminder GetCEReminderData(int vEmploymentID)
        {
            var result = (from e in _db.Employees
                          join m in _db.Employments on e.EmployeeId equals m.EmployeeId
                          join ej in _db.EmploymentJobTitles on m.EmploymentId equals ej.EmploymentId
                          join j in _db.JobTitles on ej.JobTitleId equals j.JobTitleId
                          from m1 in _db.Employments.Where(m1 => m.H1employmentId == m1.EmploymentId.ToString()).DefaultIfEmpty()
                          join ej1 in _db.EmploymentJobTitles on m1.EmploymentId equals ej1.EmploymentId
                          join j1 in _db.JobTitles on ej1.JobTitleId equals j1.JobTitleId
                          join e1 in _db.Employees on m1.EmployeeId equals e1.EmployeeId
                          from m2 in _db.Employments.Where(m2 => m1.H1employmentId == m2.EmploymentId.ToString()).DefaultIfEmpty()
                          join ej2 in _db.EmploymentJobTitles on m2.EmploymentId equals ej2.EmploymentId
                          join j2 in _db.JobTitles on ej2.JobTitleId equals j2.JobTitleId
                          join e2 in _db.Employees on m2.EmployeeId equals e2.EmployeeId
                          join th in _db.TransferHistories on m.EmploymentId equals th.EmploymentId into thGroup
                          from th in thGroup.DefaultIfEmpty()
                          join s in _db.StateProvinces on th.WorkStateAbv equals s.StateProvinceAbv into sGroup
                          from s in sGroup.DefaultIfEmpty()
                          join lt in _db.LicenseTeches on s.LicenseTechId equals lt.LicenseTechId into ltGroup
                          from lt in ltGroup.DefaultIfEmpty()
                          join el in _db.EmployeeLicenses on m.EmploymentId equals el.EmploymentId
                          join ce in _db.ContEducationRequirements on m.EmploymentId equals ce.EmploymentId
                          join l in _db.Licenses on el.LicenseId equals l.LicenseId
                          join la in _db.LineOfAuthorities on l.LineOfAuthorityId equals la.LineOfAuthorityId
                          where ej.IsCurrent == true && ej1.IsCurrent == true && ej2.IsCurrent == true
                                && m.EmploymentId == vEmploymentID && el.LicenseStatus == "Active"
                                && m.Cerequired == true && el.LicenseStatus == "Active" && ce.IsExempt == false
                          select new CEReminder
                          {
                              H2MgrName = e2.LastName.Trim() + ", " + e2.FirstName,
                              H2MgrTitle = j2.JobTitle1,
                              H2MgrEmail = m2.Email,
                              MgrName = e1.LastName.Trim() + ", " + e1.FirstName,
                              MgrTitle = j1.JobTitle1,
                              MgrEmail = m1.Email,
                              LicTechName = lt.LastName.Trim() + ", " + lt.FirstName,
                              LicTechTitle = "LICENSING SPEC",
                              LicTechEmail = lt.LicenseTechEmail,
                              LicTechPhone = lt.LicenseTechPhone,
                              TMName = e.LastName.Trim() + ", " + e.FirstName,
                              TMNumber = e.Geid,
                              TMTitle = j.JobTitle1,
                              TMEmail = m.Email,
                              //m.EmploymentId,
                              CEExpireDate = ce.EducationEndDate,
                              //ce.RequiredCreditHours
                          }).FirstOrDefault();

            return result ?? new CEReminder();
        }
        protected OkToSellData GetOkayToSellData(int vEmploymentID)
        {
            var queryResult = (from e in _db.Employees
                               join em in _db.Employments on e.EmployeeId equals em.EmployeeId
                               join ej in _db.EmploymentJobTitles on em.EmploymentId equals ej.EmploymentId
                               join j in _db.JobTitles on ej.JobTitleId equals j.JobTitleId
                               join el in _db.EmployeeLicenses on em.EmploymentId equals el.EmploymentId
                               join l in _db.Licenses on el.LicenseId equals l.LicenseId
                               join ea in _db.EmployeeAppointments on el.EmployeeLicenseId equals ea.EmployeeLicenseId
                               join th in _db.TransferHistories on em.EmploymentId equals th.EmploymentId
                               join s in _db.StateProvinces on l.StateProvinceAbv equals s.StateProvinceAbv
                               join lt in _db.LicenseTeches on s.LicenseTechId equals lt.LicenseTechId
                               join lc in _db.LicenseCompanies on l.LicenseId equals lc.LicenseId
                               join c in _db.Companies on lc.CompanyId equals c.CompanyId
                               join la in _db.LineOfAuthorities on l.LineOfAuthorityId equals la.LineOfAuthorityId
                               where em.EmployeeStatus == "Active"
                                     && ej.IsCurrent == true
                                     && el.LicenseStatus == "Active"
                                     && ea.AppointmentStatus == "Active"
                                     && th.IsCurrent == true
                                     && lc.IsActive == true
                                     && em.EmploymentId == vEmploymentID
                               orderby e.EmployeeId, em.EmploymentId
                               select new OkToSellData
                               {
                                   TMName = e.FirstName + " " + e.LastName,
                                   TMNumber = e.Geid,
                                   TMTitle = j.JobTitle1,
                                   LicenseTechName = lt.FirstName + " " + lt.LastName,
                                   LicenseTechPhone = lt.LicenseTechPhone,
                                   LicenseTechTitle = "LICENSING SPEC"
                               })
                               .AsNoTracking()
                               .FirstOrDefault();

            var sellStates = (from e in _db.Employees
                              join em in _db.Employments on e.EmployeeId equals em.EmployeeId
                              join el in _db.EmployeeLicenses on em.EmploymentId equals el.EmploymentId
                              join l in _db.Licenses on el.LicenseId equals l.LicenseId
                              join ea in _db.EmployeeAppointments on el.EmployeeLicenseId equals ea.EmployeeLicenseId
                              join th in _db.TransferHistories on em.EmploymentId equals th.EmploymentId
                              join s in _db.StateProvinces on l.StateProvinceAbv equals s.StateProvinceAbv
                              join lt in _db.LicenseTeches on s.LicenseTechId equals lt.LicenseTechId
                              join lc in _db.LicenseCompanies on l.LicenseId equals lc.LicenseId
                              join c in _db.Companies on lc.CompanyId equals c.CompanyId
                              join la in _db.LineOfAuthorities on l.LineOfAuthorityId equals la.LineOfAuthorityId
                              where em.EmployeeStatus == "Active"
                                    && el.LicenseStatus == "Active"
                                    && ea.AppointmentStatus == "Active"
                                    && th.IsCurrent == true
                                    && lc.IsActive == true
                                    && em.EmploymentId == vEmploymentID
                              orderby s.StateProvinceName
                              select new SellStateItem
                              {
                                  StateProvinceAbv = l.StateProvinceAbv,
                                  StateProvinceName = s.StateProvinceName
                              })
                              .AsNoTracking()
                              .Distinct()
                              .ToList();

            queryResult.SellStates = sellStates;

            var licenses = (from e in _db.Employees
                          join em in _db.Employments on e.EmployeeId equals em.EmployeeId
                          join el in _db.EmployeeLicenses on em.EmploymentId equals el.EmploymentId
                          join l in _db.Licenses on el.LicenseId equals l.LicenseId
                          join ea in _db.EmployeeAppointments on el.EmployeeLicenseId equals ea.EmployeeLicenseId
                          join th in _db.TransferHistories on em.EmploymentId equals th.EmploymentId
                          join s in _db.StateProvinces on l.StateProvinceAbv equals s.StateProvinceAbv
                          join lt in _db.LicenseTeches on s.LicenseTechId equals lt.LicenseTechId
                          join lc in _db.LicenseCompanies on l.LicenseId equals lc.LicenseId
                          join c in _db.Companies on lc.CompanyId equals c.CompanyId
                          join la in _db.LineOfAuthorities on l.LineOfAuthorityId equals la.LineOfAuthorityId
                          where em.EmployeeStatus == "Active"
                                && el.LicenseStatus == "Active"
                                && ea.AppointmentStatus == "Active"
                                && th.IsCurrent == true
                                && lc.IsActive == true
                                && em.EmploymentId == vEmploymentID
                          orderby la.LineOfAuthorityName
                          select new LicenseStateItem()
                          {
                              StateAbbr = l.StateProvinceAbv,
                              EmployeeLicenseID = el.EmployeeLicenseId,
                              LineOfAuthorityName = la.LineOfAuthorityName,
                              LicenseNumber = el.LicenseNumber
                          })
                          .AsNoTracking()
                          .Distinct()
                          .ToList();

            queryResult.licenseStateItems = licenses;

            var effective = (from e in _db.Employees
                             join em in _db.Employments on e.EmployeeId equals em.EmployeeId
                             join el in _db.EmployeeLicenses on em.EmploymentId equals el.EmploymentId
                             join l in _db.Licenses on el.LicenseId equals l.LicenseId
                             join ea in _db.EmployeeAppointments on el.EmployeeLicenseId equals ea.EmployeeLicenseId
                             join th in _db.TransferHistories on em.EmploymentId equals th.EmploymentId
                             join s in _db.StateProvinces on l.StateProvinceAbv equals s.StateProvinceAbv
                             join lt in _db.LicenseTeches on s.LicenseTechId equals lt.LicenseTechId
                             join lc in _db.LicenseCompanies on l.LicenseId equals lc.LicenseId
                             join c in _db.Companies on lc.CompanyId equals c.CompanyId
                             join la in _db.LineOfAuthorities on l.LineOfAuthorityId equals la.LineOfAuthorityId
                             where em.EmployeeStatus == "Active"
                                   && el.LicenseStatus == "Active"
                                   && ea.AppointmentStatus == "Active"
                                   && th.IsCurrent == true
                                   && lc.IsActive == true
                                   && em.EmploymentId == vEmploymentID
                             group new { el.EmployeeLicenseId, c.CompanyName, ea.AppointmentEffectiveDate } by new { el.EmployeeLicenseId, c.CompanyName } into g
                             orderby g.Key.CompanyName, g.Min(x => x.AppointmentEffectiveDate)
                             select new LicenseEffectiveDate()
                             {
                                 EmployeeLicenseID = g.Key.EmployeeLicenseId,
                                 CompanyName = g.Key.CompanyName,
                                 AppointmentEffectiveDate = g.Min(x => x.AppointmentEffectiveDate)
                             })
                 .AsNoTracking()
                 .Distinct()
                 .ToList();

            queryResult.licenseEffectiveDates = effective;

            return queryResult ?? null;
        }
        protected NameChangeInfo GetNameChangeInfo(int vEmploymentID)
        {
                var sql = @"SELECT TOP 1 
                                X.EmployeeID
                                , m.EmploymentID
                                , e.FirstName + ' ' + e.LastName AS TMName
                                , e.GEID AS TMNumber
                                , j.JobTitle AS TMTitle
                                , m.Email AS TMEmail
                                , lt.[FirstName] + ' ' + lt.[LastName] AS LicTechName
                                , [LicenseTechPhone] AS LicTechPhone
                                , 'LICENSING SPEC' AS LicTechTitle
                                , ISNULL(FirstName_Old, e.FirstName) + ' ' + ISNULL(MiddleName_Old
                                , e.MiddleName)  + ' ' + ISNULL(LastName_Old, e.LastName) AS NameOld
                                , ISNULL(e.FirstName, '') + ' ' + ISNULL(e.MiddleName, '')  + ' ' + ISNULL(e.LastName, '') AS NameNew
                                , ChangeDate
                            FROM dbo.Employee e 
                            INNER JOIN dbo.Employment m ON e.EmployeeID = m.EmployeeID 
                                AND m.EmploymentID = @EmploymentID
                            INNER JOIN [dbo].[EmploymentJobTitle] ej ON m.EmploymentID = ej.EmploymentID 
                                AND ej.[IsCurrent] = 1
                            INNER JOIN [dbo].[JobTitles] j ON ej.JobTitleID = j.JobTitleID
                            INNER JOIN TransferHistory th ON m.EmploymentID = th.EmploymentID 
                                AND th.IsCurrent = 1
                            INNER JOIN StateProvince s ON th.WorkStateAbv = s.StateProvinceAbv 
                            INNER JOIN [dbo].[LicenseTech] lt ON s.[LicenseTechID] = lt.LicenseTechID 
                            INNER JOIN
                            (SELECT Convert(Int,EmployeeID) as EmployeeID, 
	                            FirstName as FirstName_Old,
	                            MiddleName as MiddleName_Old,
	                            LastName as LastName_Old,
	                            Convert(varchar,ModifyDate,110) as ChangeDate
                            FROM
                                (SELECT 
                                    A.BaseTableKeyValue AS EmployeeID
                                    , A.AuditFieldName
                                    , A.AuditValueBefore
                                    , A.ModifyDate 
		                         FROM dbo.AuditLog A
		                         INNER JOIN dbo.Employment m ON a.BaseTableKeyValue = m.EmployeeID 
                                    AND m.EmploymentID = @EmploymentID
		                         INNER JOIN
				                    (SELECT max(ModifyDate) as ModifyDate, AuditFieldName, BaseTableName, BaseTableKeyValue, AuditAction
				                     FROM dbo.AuditLog WHERE BaseTableName ='Employee' AND AuditAction='Update'
				                        AND AuditFieldName in('FirstName','MiddleName','LastName')
				                     GROUP BY AuditFieldName, BaseTableName, BaseTableKeyValue, AuditFieldName, AuditAction) B
			                            ON A.BaseTableName= B.BaseTableName AND A.AuditAction=B.AuditAction
			                            AND A.BaseTableKeyValue=B.BaseTableKeyValue
			                            AND A.ModifyDate = B.ModifyDate
			                            AND A.AuditFieldName = B.AuditFieldName)P
                            PIVOT
                            (MAX(AuditValueBefore)
                            FOR AuditFieldName IN ([FirstName],[MiddleName],[LastName])) as pvt) X
                            ON m.EmployeeID = X.EmployeeID

                            ORDER BY ChangeDate DESC  ";

                var parameters = new[]
                            {
                                new SqlParameter("@EmploymentID", vEmploymentID)
                            };

                var queryResults = _db.NameChangeInfo
                                .FromSqlRaw(sql, parameters)
                                .AsNoTracking()
                                .FirstOrDefault();

            return queryResults;
        }
        #endregion

    }
}
