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
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
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

            }
            catch (Exception)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            return new Tuple<string, string, string, string>(strHTML, string.Empty, string.Empty, string.Empty);
        }

        #region "GetManagerInfo"
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
                              H3MgrName = e3.LastName + ", " + e3.FirstName,
                              H3MgrTitle = j3.JobTitle1,
                              H3MgrEmail = m3.Email,
                              H2MgrEmploymentID = m2.EmploymentId,
                              H2MgrName = e2.LastName + ", " + e2.FirstName,
                              H2MgrTitle = j2.JobTitle1,
                              H2MgrEmail = m2.Email,
                              MgrEmploymentID = m1.EmploymentId,
                              MgrName = e1.LastName + ", " + e1.FirstName,
                              MgrTitle = j1.JobTitle1,
                              MgrEmail = m1.Email,
                              LicTechName = lt.LastName + ", " + lt.FirstName,
                              LicTechTitle = "LICENSING SPEC",
                              LicTechEmail = lt.LicenseTechEmail,
                              LicTechPhone = lt.LicenseTechPhone,
                              TMName = e.LastName + ", " + e.FirstName,
                              TMNumber = e.Geid,
                              TMTitle = j.JobTitle1,
                              TMEmail = m.Email
                          }).FirstOrDefault();

            return result ?? new ManagerInfo();

        }
        #endregion
    }
}
