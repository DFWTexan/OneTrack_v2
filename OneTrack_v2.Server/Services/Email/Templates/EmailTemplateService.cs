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
            string strHTML = string.Empty;
            string strXML = string.Empty;
            string strReturnStatus = string.Empty;
            string strH2MgrEmail = string.Empty;
            string strMgrEmail = string.Empty;
            string strLicenseTechEmail = string.Empty;
            string strTMEmail = string.Empty;

            //   H2MgrName	H2MgrTitle	H2MgrEmail	EmploymentID	MgrName	MgrTitle	MgrEmail	LichTecName	LicTechTitle	LicenseTechEmail	TMName	TMNumber	TMTitle	TMEmail

            try
            {
                var managerInfo = GetManagerInfo(vEmploymentID);

                //strHTML = strHTML + @"<html>";

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

                strHTML = strHTML + @"<table style = ""width: 800px;""> ";
                strHTML = strHTML + @"<tr> ";
                strHTML = strHTML + @"<td style = ""text-align: left; background-color: #F69200;""> <span style = ""font-family: Arial; color: #FFFFFF; font-size: 16pt; font-weight: bold; font-style: normal; text-decoration: none;"">OneMain Insurance Licensing Department</span></td> ";
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
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #FF0000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none;"" > ACTION REQUIRED</span></td> ";
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
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >to this email </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >ONLY </span>";
                strHTML = strHTML + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: none; background-color: #FFFF00;"" >without copying your Licensing Specialist to expedite your license process. </span>";
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
                strHTML = strHTML + @"<td colspan = ""3"">";
                strHTML = strHTML + @"<textarea id = ""TextArea1"" cols = ""1"" name = ""TextArea1"" rows = ""20"" style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none; width: 600px; height: 200px""></textarea>";
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
                strHTML = strHTML + @"</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"<td> &nbsp;</td> ";
                strHTML = strHTML + @"</tr> ";
                strHTML = strHTML + @"</table> ";
                //strHTML = strHTML + @"</html> ";
            }
            //catch (SqlException mySQLEx)
            //{
            //    return new Tuple<string, string, string, string>(String.Empty, String.Empty, String.Empty, String.Empty);
            //}
            catch (Exception myex)
            {
                return new Tuple<string, string, string, string>(string.Empty, string.Empty, string.Empty, string.Empty);
            }

            var finalHTML = strHTML.ToString();
            return new Tuple<string, string, string, string>(finalHTML, strXML, strTMEmail, strMgrEmail);
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
