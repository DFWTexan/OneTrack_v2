using Azure;
using Azure.Core;
using DataModel.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OneTrack_v2.DbData;
using OneTrack_v2.DbData.Models;
using OneTrak_v2.DataModel;
using OneTrak_v2.Server.Services.Email.Templates;
using OneTrak_v2.Services.Model;
using System.Data;
using System.Net.Mail;
using System.Reflection;
using System.Web;

namespace OneTrack_v2.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly AppDataContext _db;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IUtilityHelpService _utilityService;
        private readonly string? _connectionString;

        private readonly string? _mailServer;
        private readonly string? _mailFromAddress;
        private readonly string? _mailToAddress;
        private readonly string? _mailCCAddress;
        private readonly string? _mailBCCAddress;


        public EmailService(AppDataContext db, IConfiguration config, IUtilityHelpService utilityHelpService, IEmailTemplateService emailTemplateService)
        {
            _db = db;
            _config = config;
            _connectionString = _config.GetConnectionString(name: "DefaultConnection");
            _utilityService = utilityHelpService;
            _emailTemplateService = emailTemplateService;
            // Retrieve the current environment setting
            string environment = _config.GetValue<string>("Environment") ?? "DVLP";

            // Construct the keys for accessing environment-specific settings
            string mailServerKey = $"EnvironmentSettings:{environment}:mailServer";
            string mailFromAddressKey = $"EnvironmentSettings:{environment}:mailFromAddress";

            // Retrieve the values based on the constructed keys
            _mailServer = _config.GetValue<string>(mailServerKey);
            _mailFromAddress = _config.GetValue<string>(mailFromAddressKey);

        }

        public ReturnResult GetEmailComTemplates()
        {
            var result = new ReturnResult();
            try
            {
                var emailComTemplates = _db.Communications
                                    .Where(c => c.DocAppType == "Template" && c.IsActive)
                                    .OrderBy(c => c.DocTypeAbv + "-" + c.CommunicationName)
                                    .Select(c => new OputEmailComTemplate
                                    {
                                        CommunicationID = c.CommunicationId,
                                        CommunicationName = c.CommunicationName ?? "",
                                        DocType = c.DocType ?? "",
                                        DocTypeAbv = c.DocTypeAbv ?? "",
                                        DocSubType = c.DocSubType ?? "",
                                        EmailAttachments = c.EmailAttachments ?? "",
                                        HasNote = c.HasNote,
                                        DocTypeDocSubType = c.DocTypeAbv + "-" + c.CommunicationName
                                    })
                                    .ToList();

                result.ObjData = emailComTemplates;
                result.Success = true;
                result.StatusCode = 200;

                return result;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;
                return result;
            }
        }

        public ReturnResult GetEmailTemplate(int vCommunicationID, int vEmploymentID)
        {
            var result = new ReturnResult();
            try
            {
                switch (vCommunicationID)
                {
                    case 33: // "APP-{MESSAGE}"
                        var appHTML = _emailTemplateService.GetMessageHTML(vEmploymentID);
                        result.ObjData = new { Header = appHTML.Item1.ToString(), Footer = appHTML.Item2.ToString() };
                        break;
                    case 35: // "CE-{MESSAGE}"
                        var ceHTML = _emailTemplateService.GetMessageHTML(vEmploymentID);
                        result.ObjData = ceHTML.Item1.ToString();
                        break;
                    case 36: // "REN-{MESSAGE}"
                        var renHTML = _emailTemplateService.GetMessageHTML(vEmploymentID);
                        result.ObjData = renHTML.Item1.ToString();
                        break;
                    case 37: // "PRO-{MESSAGE}"
                        var proHTML = _emailTemplateService.GetMessageHTML(vEmploymentID);
                        result.ObjData = proHTML.Item1.ToString();
                        break;
                    default:
                        result.ObjData = @"<div class=""col d-flex justify-content-center mt-5"">
                                                <span class=""material-symbols-outlined"">unknown_document</span>
                                                <div>
                                                    <h3>Unknown Document Type</h3>
                                                    <p>Document type not found.</p>
                                                </div>
                                           </div>"; 
                        break;
                }

                result.Success = true;
                result.StatusCode = 200;

            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = ex.Message;
                result.ErrMessage = "Server Error - Please Contact Support [REF# EMAIL-7512-49031].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
            }

            return result;
        }

        public ReturnResult Send([FromBody] IputSendEmail vInput)
        {
            var comType = _db.Communications
                            .Where(c => c.CommunicationId == vInput.CommunicationID)
                            .Select(c => c.DocTypeAbv + "-" + c.CommunicationName)
                            .FirstOrDefault();

            var result = new ReturnResult();
            try
            {

                String strSubject = String.Empty;
                String strBody = String.Empty;
                String strEmailTo = String.Empty;
                String strEmailCC = String.Empty;
                String strEmploymentCommunicationID = String.Empty;
                int intEmploymentCommunicationID = 0;
                //String strCommunicationID = String.Empty;
                String strMessage = String.Empty;
                String strHTMLNew = String.Empty;
                String strHTMLOld = String.Empty;
                //"Ok To Sell - Ref# 000000000000001"
                strEmailTo = vInput.EmailTo ?? "";
                strEmailCC = vInput.CcEmail != null ? string.Join(", ", vInput.CcEmail) : "";
                strSubject = vInput.EmailSubject ?? "";
                strBody = vInput.EmailContent ?? "";
                
                switch (comType)
                {
                    case "APP-{MESSAGE}":
                        //strHTMLNew = @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >***MESSAGE***</span>";
                        //strHTMLOld = @"<textarea id = ""TextArea1"" cols = ""1"" name = ""TextArea1"" rows = ""20"" style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none; width: 600px; height: 200px""></textarea>";
                        //strMessage = HttpUtility.HtmlEncode(Request.Form["TextArea1"].ToString());
                        //strMessage = strMessage.Replace("\n", "<br/>");
                        //if (strMessage == "")
                        //{ strMessage = " "; }
                        //strHTMLNew = strHTMLNew.Replace(@"***MESSAGE***", strMessage.ToString());
                        //strBody = DIV2.InnerHtml.Replace(strHTMLOld, strHTMLNew);
                        //Session["strCompareXML"] = @"<Communication>Message</Communication>";
                        break;
                    case "CE-{MESSAGE}":
                        //        strHTMLNew = @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >***MESSAGE***</span>";
                        //        strHTMLOld = @"<textarea id = ""TextArea1"" cols = ""1"" name = ""TextArea1"" rows = ""20"" style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none; width: 600px; height: 200px""></textarea>";
                        //        strMessage = HttpUtility.HtmlEncode(Request.Form["TextArea1"].ToString());
                        //        strMessage = strMessage.Replace("\n", "<br/>");
                        //        if (strMessage == "")
                        //        { strMessage = " "; }
                        //        strHTMLNew = strHTMLNew.Replace(@"***MESSAGE***", strMessage.ToString());
                        //        strBody = DIV2.InnerHtml.Replace(strHTMLOld, strHTMLNew);
                        //        Session["strCompareXML"] = @"<Communication>Message</Communication>";
                        break;
                    case "REN-{MESSAGE}":
                        //        strHTMLNew = @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >***MESSAGE***</span>";
                        //        strHTMLOld = @"<textarea id = ""TextArea1"" cols = ""1"" name = ""TextArea1"" rows = ""20"" style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none; width: 600px; height: 200px""></textarea>";
                        //        strMessage = HttpUtility.HtmlEncode(Request.Form["TextArea1"].ToString());
                        //        strMessage = strMessage.Replace("\n", "<br/>");
                        //        if (strMessage == "")
                        //        { strMessage = " "; }
                        //        strHTMLNew = strHTMLNew.Replace(@"***MESSAGE***", strMessage.ToString());
                        //        strBody = DIV2.InnerHtml.Replace(strHTMLOld, strHTMLNew);
                        //        Session["strCompareXML"] = @"<Communication>Message</Communication>";
                        break;
                    case "PRO-{MESSAGE}":
                        //        strHTMLNew = @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >***MESSAGE***</span>";
                        //        strHTMLOld = @"<textarea id = ""TextArea1"" cols = ""1"" name = ""TextArea1"" rows = ""20"" style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none; width: 600px; height: 200px""></textarea>";
                        //        strMessage = HttpUtility.HtmlEncode(Request.Form["TextArea1"].ToString());
                        //        strMessage = strMessage.Replace("\n", "<br/>");
                        //        if (strMessage == "")
                        //        { strMessage = " "; }
                        //        strHTMLNew = strHTMLNew.Replace(@"***MESSAGE***", strMessage.ToString());
                        //        strBody = DIV2.InnerHtml.Replace(strHTMLOld, strHTMLNew);
                        //        Session["strCompareXML"] = @"<Communication>Message</Communication>";
                        break;
                    case "APP-Incomplete":
                        //        strHTMLNew = @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >***MESSAGE***</span>";
                        //        strHTMLOld = @"<textarea id = ""TextArea1"" cols = ""1"" name = ""TextArea1"" rows = ""20"" style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none; width: 600px; height: 200px""></textarea>";
                        //        strMessage = HttpUtility.HtmlEncode(Request.Form["TextArea1"].ToString());
                        //        strMessage = strMessage.Replace("\n", "<br/>");
                        //        if (strMessage == "")
                        //        { strMessage = " "; }
                        //        strHTMLNew = strHTMLNew.Replace(@"***MESSAGE***", strMessage.ToString());
                        //        strBody = DIV2.InnerHtml.Replace(strHTMLOld, strHTMLNew);
                        //        Session["strCompareXML"] = @"<Communication>Message</Communication>";
                        break;
                    case "APP-Employment History":
                        //        strHTMLNew = @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >***MESSAGE***</span>";
                        //        strHTMLOld = @"<textarea id = ""TextArea1"" cols = ""1"" name = ""TextArea1"" rows = ""20"" style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none; width: 600px; height: 200px""></textarea>";
                        //        strMessage = HttpUtility.HtmlEncode(Request.Form["TextArea1"].ToString());
                        //        strMessage = strMessage.Replace("\n", "<br/>");
                        //        if (strMessage == "")
                        //        { strMessage = " "; }
                        //        strHTMLNew = strHTMLNew.Replace(@"***MESSAGE***", strMessage.ToString());
                        //        strBody = DIV2.InnerHtml.Replace(strHTMLOld, strHTMLNew);
                        //        Session["strCompareXML"] = @"<Communication>Message</Communication>";
                        break;
                    case "APP-Fingerprint Scheduled-GA":
                        //        strHTMLNew = @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >***MESSAGE***</span>";
                        //        strHTMLOld = @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > <input id = ""Text1"" type = ""text"" name = ""Text1"" style = ""width: 200px; height: 23px"" /></span>";
                        //        strMessage = HttpUtility.HtmlEncode(Request.Form["Text1"].ToString());
                        //        strMessage = strMessage.Replace("\n", "<br/>");
                        //        if (strMessage == "")
                        //        { strMessage = " "; }
                        //        strHTMLNew = strHTMLNew.Replace(@"***MESSAGE***", strMessage.ToString());
                        //        strBody = DIV2.InnerHtml.Replace(strHTMLOld, strHTMLNew);
                        //        Session["strCompareXML"] = @"<Communication>Message</Communication>";
                        break;
                    case "APP-Expired Certificate-MD":
                        //        strHTMLNew = @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >***MESSAGE***.</span>";
                        //        strHTMLOld = @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > <input id = ""Text1"" type = ""text"" name = ""Text1"" style = ""width: 200px; height: 23px"" />.</span>";
                        //        strMessage = HttpUtility.HtmlEncode(Request.Form["Text1"].ToString());
                        //        strMessage = strMessage.Replace("\n", "<br/>");
                        //        if (strMessage == "")
                        //        { strMessage = " "; }
                        //        strHTMLNew = strHTMLNew.Replace(@"***MESSAGE***", strMessage.ToString());
                        //        strBody = DIV2.InnerHtml.Replace(strHTMLOld, strHTMLNew);
                        //        Session["strCompareXML"] = @"<Communication>Message</Communication>";
                        break;
                    case "APP-Expired Certificate-AL":
                        //        strHTMLNew = @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >***MESSAGE****.</span>";
                        //        strHTMLOld = @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > <input id = ""Text1"" type = ""text"" name = ""Text1"" style = ""width: 200px; height: 23px"" />.</span>";
                        //        strMessage = HttpUtility.HtmlEncode(Request.Form["Text1"].ToString());
                        //        strMessage = strMessage.Replace("\n", "<br/>");
                        //        if (strMessage == "")
                        //        { strMessage = " "; }
                        //        strHTMLNew = strHTMLNew.Replace(@"***MESSAGE***", strMessage.ToString());
                        //        strBody = DIV2.InnerHtml.Replace(strHTMLOld, strHTMLNew);
                        //        Session["strCompareXML"] = @"<Communication>Message</Communication>";
                        break;
                    case "APP-Expired Certificate-TN":
                        //        strHTMLNew = @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >***MESSAGE***.</span>";
                        //        strHTMLOld = @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > <input id = ""Text1"" type = ""text"" name = ""Text1"" style = ""width: 200px; height: 23px"" />.</span>";
                        //        strMessage = HttpUtility.HtmlEncode(Request.Form["Text1"].ToString());
                        //        strMessage = strMessage.Replace("\n", "<br/>");
                        //        if (strMessage == "")
                        //        { strMessage = " "; }
                        //        strHTMLNew = strHTMLNew.Replace(@"***MESSAGE***", strMessage.ToString());
                        //        strBody = DIV2.InnerHtml.Replace(strHTMLOld, strHTMLNew);
                        //        Session["strCompareXML"] = @"<Communication>Message</Communication>";
                        break;
                    case "APP-State Exam Exception":
                        //        strHTMLNew = String.Empty;
                        //        strHTMLOld = String.Empty;
                        //        strBody = DIV2.InnerHtml;
                        //        //////////////Checkbox2//Checkbox2//Checkbox2//Checkbox2//Checkbox2////////////////////////////
                        //        strHTMLOld = @"<tr> ";
                        //        strHTMLOld = strHTMLOld + @"<td colspan = ""3"" >";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" ><input id=""Checkbox2"" type=""checkbox"" name=""Checkbox2"" checked=""checked""/></span>";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > The TM’s highest simulated exams score is </span>";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" ><input id=""Text2"" type=""text"" name=""Text2"" style=""width: 200px; height: 23px"" />.</span>";
                        //        strHTMLOld = strHTMLOld + @"</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"</tr> ";
                        //        strHTMLOld = strHTMLOld + @"<tr> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"</tr> ";
                        //        if (Request.Form["Checkbox2"] == null)
                        //        {
                        //            strBody = strBody.Replace(strHTMLOld, "");
                        //        }
                        //        else
                        //        {
                        //            strHTMLNew = @"<tr> ";
                        //            strHTMLNew = strHTMLNew + @"<td colspan = ""3"" >";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > The TM’s highest simulated exams score is </span>";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >***MESSAGE***.</span>";
                        //            strHTMLNew = strHTMLNew + @"</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td>";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"</tr> ";
                        //            strHTMLNew = strHTMLNew + @"<tr> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"</tr> ";
                        //            strMessage = HttpUtility.HtmlEncode(Request.Form["Text2"].ToString());
                        //            strMessage = strMessage.Replace("\n", "<br/>");
                        //            if (strMessage == "")
                        //            { strMessage = " "; }
                        //            strHTMLNew = strHTMLNew.Replace(@"***MESSAGE***", strMessage.ToString());
                        //            strBody = strBody.Replace(strHTMLOld, strHTMLNew);
                        //        }
                        //        //////////////Checkbox2//Checkbox2//Checkbox2//Checkbox2//Checkbox2////////////////////////////
                        //        //////////////Checkbox3//Checkbox3//Checkbox3//Checkbox3//Checkbox3////////////////////////////
                        //        strHTMLOld = @"<tr> ";
                        //        strHTMLOld = strHTMLOld + @"<td colspan = ""3"" >";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" ><input id=""Checkbox3"" type=""checkbox"" name=""Checkbox3"" checked=""checked""/></span>";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > The highest score was reached some time back on </span>";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" ><input id=""Text3"" type=""text"" name=""Text3"" style=""width: 200px; height: 23px"" />.</span>";
                        //        strHTMLOld = strHTMLOld + @"</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"</tr> ";
                        //        strHTMLOld = strHTMLOld + @"<tr> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"</tr> ";
                        //        if (Request.Form["Checkbox3"] == null)
                        //        {
                        //            strBody = strBody.Replace(strHTMLOld, "");
                        //        }
                        //        else
                        //        {
                        //            strHTMLNew = @"<tr> ";
                        //            strHTMLNew = strHTMLNew + @"<td colspan = ""3"" >";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > The highest score was reached some time back on </span>";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >***MESSAGE***.</span>";
                        //            strHTMLNew = strHTMLNew + @"</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td>";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"</tr> ";
                        //            strHTMLNew = strHTMLNew + @"<tr> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"</tr> ";
                        //            strMessage = HttpUtility.HtmlEncode(Request.Form["Text3"].ToString());
                        //            strMessage = strMessage.Replace("\n", "<br/>");
                        //            if (strMessage == "")
                        //            { strMessage = " "; }
                        //            strHTMLNew = strHTMLNew.Replace(@"***MESSAGE***", strMessage.ToString());
                        //            strBody = strBody.Replace(strHTMLOld, strHTMLNew);
                        //        }
                        //        //////////////Checkbox3//Checkbox3//Checkbox3//Checkbox3//Checkbox3////////////////////////////
                        //        //////////////Checkbox4//Checkbox4//Checkbox4//Checkbox4//Checkbox4////////////////////////////
                        //        strHTMLOld = @"<tr> ";
                        //        strHTMLOld = strHTMLOld + @"<td colspan = ""3"" >";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" ><input id=""Checkbox4"" type=""checkbox"" name=""Checkbox4"" checked=""checked""/></span>";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > This is attempt # </span>";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" ><input id=""Text4"" type=""text"" name=""Text4"" style=""width: 200px; height: 23px"" />.</span>";
                        //        strHTMLOld = strHTMLOld + @"</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"</tr> ";
                        //        strHTMLOld = strHTMLOld + @"<tr> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"</tr> ";
                        //        if (Request.Form["Checkbox4"] == null)
                        //        {
                        //            strBody = strBody.Replace(strHTMLOld, "");
                        //        }
                        //        else
                        //        {
                        //            strHTMLNew = @"<tr> ";
                        //            strHTMLNew = strHTMLNew + @"<td colspan = ""3"" >";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > This is attempt # </span>";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >***MESSAGE***.</span>";
                        //            strHTMLNew = strHTMLNew + @"</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"</tr> ";
                        //            strHTMLNew = strHTMLNew + @"<tr> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"</tr> ";
                        //            strMessage = HttpUtility.HtmlEncode(Request.Form["Text4"].ToString());
                        //            strMessage = strMessage.Replace("\n", "<br/>");
                        //            if (strMessage == "")
                        //            { strMessage = " "; }
                        //            strHTMLNew = strHTMLNew.Replace(@"***MESSAGE***", strMessage.ToString());
                        //            strBody = strBody.Replace(strHTMLOld, strHTMLNew);
                        //        }
                        //        //////////////Checkbox4//Checkbox4//Checkbox4//Checkbox4//Checkbox4////////////////////////////

                        //        //////////////Checkbox5//Checkbox5//Checkbox5//Checkbox5//Checkbox5////////////////////////////
                        //        strHTMLOld = @"<tr> ";
                        //        strHTMLOld = strHTMLOld + @"<td colspan = ""3"" >";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" ><input id=""Checkbox5"" type=""checkbox"" name=""Checkbox5"" checked=""checked""/></span>";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > The TM has not accessed the course since </span>";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" ><input id=""Text5"" type=""text"" name=""Text5"" style=""width: 200px; height: 23px"" />.</span>";
                        //        strHTMLOld = strHTMLOld + @"</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"</tr> ";
                        //        strHTMLOld = strHTMLOld + @"<tr> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"</tr> ";
                        //        if (Request.Form["Checkbox5"] == null)
                        //        {
                        //            strBody = strBody.Replace(strHTMLOld, "");
                        //        }
                        //        else
                        //        {
                        //            strHTMLNew = @"<tr> ";
                        //            strHTMLNew = strHTMLNew + @"<td colspan = ""3"" >";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > The TM has not accessed the course since </span>";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >***MESSAGE***</span>";
                        //            strHTMLNew = strHTMLNew + @"</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td>";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"</tr> ";
                        //            strHTMLNew = strHTMLNew + @"<tr> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"</tr> ";
                        //            strMessage = HttpUtility.HtmlEncode(Request.Form["Text5"].ToString());
                        //            strMessage = strMessage.Replace("\n", "<br/>");
                        //            if (strMessage == "")
                        //            { strMessage = " "; }
                        //            strHTMLNew = strHTMLNew.Replace(@"***MESSAGE***", strMessage.ToString());
                        //            strBody = strBody.Replace(strHTMLOld, strHTMLNew);
                        //        }
                        //        //////////////Checkbox5//Checkbox5//Checkbox5//Checkbox5//Checkbox5////////////////////////////
                        //        //////////////Checkbox6//Checkbox6//Checkbox6//Checkbox6//Checkbox6////////////////////////////
                        //        strHTMLOld = @"<tr> ";
                        //        strHTMLOld = strHTMLOld + @"<td colspan = ""3"" >";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" ><input id=""Checkbox6"" type=""checkbox"" name=""Checkbox6"" checked=""checked""/></span>";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > The percent of progress for the TM’s study is only at </span>";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" ><input id=""Text6"" type=""text"" name=""Text6"" style=""width: 200px; height: 23px"" />.</span>";
                        //        strHTMLOld = strHTMLOld + @"</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"</tr> ";
                        //        strHTMLOld = strHTMLOld + @"<tr> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"</tr> ";
                        //        if (Request.Form["Checkbox6"] == null)
                        //        {
                        //            strBody = strBody.Replace(strHTMLOld, "");
                        //        }
                        //        else
                        //        {
                        //            strHTMLNew = @"<tr> ";
                        //            strHTMLNew = strHTMLNew + @"<td colspan = ""3"" >";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > The percent of progress for the TM’s study is only at </span>";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >***MESSAGE***</span>";
                        //            strHTMLNew = strHTMLNew + @"</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td>";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"</tr> ";
                        //            strHTMLNew = strHTMLNew + @"<tr> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"</tr> ";
                        //            strMessage = HttpUtility.HtmlEncode(Request.Form["Text6"].ToString());
                        //            strMessage = strMessage.Replace("\n", "<br/>");
                        //            if (strMessage == "")
                        //            { strMessage = " "; }
                        //            strHTMLNew = strHTMLNew.Replace(@"***MESSAGE***", strMessage.ToString());
                        //            strBody = strBody.Replace(strHTMLOld, strHTMLNew);
                        //        }
                        //        //////////////Checkbox6//Checkbox6//Checkbox6//Checkbox6//Checkbox6////////////////////////////
                        //        Session["strCompareXML"] = @"<Communication>Message</Communication>";
                        break;
                    case "APP-ExamFX Course Renewal":
                        //        strHTMLNew = @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >***MESSAGE***</span>";
                        //        strHTMLOld = @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" ><input id=""Text1"" type=""text"" name=""Text1"" style=""width: 200px; height: 23px"" /></span>";
                        //        strMessage = HttpUtility.HtmlEncode(Request.Form["Text1"].ToString());
                        //        strMessage = strMessage.Replace("\n", "<br/>");
                        //        if (strMessage == "")
                        //        { strMessage = " "; }
                        //        strHTMLNew = strHTMLNew.Replace(@"***MESSAGE***", strMessage.ToString());
                        //        strBody = DIV2.InnerHtml.Replace(strHTMLOld, strHTMLNew);
                        //        //////////////Checkbox2//Checkbox2//Checkbox2//Checkbox2//Checkbox2////////////////////////////
                        //        strHTMLOld = @"<tr> ";
                        //        strHTMLOld = strHTMLOld + @"<td colspan = ""3"" >";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" ><input id=""Checkbox2"" type=""checkbox"" name=""Checkbox2"" checked=""checked""/></span>";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > they haven’t accessed the course since </span>";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" ><input id=""Text2"" type=""text"" name=""Text2"" style=""width: 200px; height: 23px"" />.</span>";
                        //        strHTMLOld = strHTMLOld + @"</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"</tr> ";
                        //        strHTMLOld = strHTMLOld + @"<tr> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"</tr> ";
                        //        if (Request.Form["Checkbox2"] == null)
                        //        {
                        //            strBody = strBody.Replace(strHTMLOld, "");
                        //        }
                        //        else
                        //        {
                        //            strHTMLNew = @"<tr> ";
                        //            strHTMLNew = strHTMLNew + @"<td colspan = ""3"" >";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > they haven’t accessed the course since </span>";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >***MESSAGE***.</span>";
                        //            strHTMLNew = strHTMLNew + @"</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td>";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"</tr> ";
                        //            strHTMLNew = strHTMLNew + @"<tr> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"</tr> ";
                        //            strMessage = HttpUtility.HtmlEncode(Request.Form["Text2"].ToString());
                        //            strMessage = strMessage.Replace("\n", "<br/>");
                        //            if (strMessage == "")
                        //            { strMessage = " "; }
                        //            strHTMLNew = strHTMLNew.Replace(@"***MESSAGE***", strMessage.ToString());
                        //            strBody = strBody.Replace(strHTMLOld, strHTMLNew);
                        //        }
                        //        //////////////Checkbox2//Checkbox2//Checkbox2//Checkbox2//Checkbox2////////////////////////////
                        //        //////////////Checkbox3//Checkbox3//Checkbox3//Checkbox3//Checkbox3////////////////////////////
                        //        strHTMLOld = @"<tr> ";
                        //        strHTMLOld = strHTMLOld + @"<td colspan = ""3"" >";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" ><input id=""Checkbox3"" type=""checkbox"" name=""Checkbox3"" checked=""checked""/></span>";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > they have not started the course. </span>";
                        //        strHTMLOld = strHTMLOld + @"</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"</tr> ";
                        //        strHTMLOld = strHTMLOld + @"<tr> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"</tr> ";
                        //        if (Request.Form["Checkbox3"] == null)
                        //        {
                        //            strBody = strBody.Replace(strHTMLOld, "");
                        //        }
                        //        else
                        //        {
                        //            strHTMLNew = @"<tr> ";
                        //            strHTMLNew = strHTMLNew + @"<td colspan = ""3"" >";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > they have not started the course.  </span>";
                        //            strHTMLNew = strHTMLNew + @"</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td>";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"</tr> ";
                        //            strHTMLNew = strHTMLNew + @"<tr> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"</tr> ";
                        //            strBody = strBody.Replace(strHTMLOld, strHTMLNew);
                        //        }
                        //        //////////////Checkbox3//Checkbox3//Checkbox3//Checkbox3//Checkbox3////////////////////////////
                        //        //////////////Checkbox4//Checkbox4//Checkbox4//Checkbox4//Checkbox4////////////////////////////
                        //        strHTMLOld = @"<tr> ";
                        //        strHTMLOld = strHTMLOld + @"<td colspan = ""3"" >";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" ><input id=""Checkbox4"" type=""checkbox"" name=""Checkbox4"" checked=""checked""/></span>";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > they have not accessed the course since the last renewal so Regional Director  </span>";
                        //        strHTMLOld = strHTMLOld + @"</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"</tr> ";
                        //        strHTMLOld = strHTMLOld + @"<tr> ";
                        //        strHTMLOld = strHTMLOld + @"<td colspan = ""3"" >";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >approval is required to continue.</span>";
                        //        strHTMLOld = strHTMLOld + @"</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"</tr> ";
                        //        strHTMLOld = strHTMLOld + @"<tr> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"</tr> ";
                        //        if (Request.Form["Checkbox4"] == null)
                        //        {
                        //            strBody = strBody.Replace(strHTMLOld, "");
                        //        }
                        //        else
                        //        {
                        //            strHTMLNew = @"<tr> ";
                        //            strHTMLNew = strHTMLNew + @"<td colspan = ""3"" >";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > they have not accessed the course since the last renewal so Regional Director  </span>";
                        //            strHTMLNew = strHTMLNew + @"</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"</tr> ";
                        //            strHTMLNew = strHTMLNew + @"<tr> ";
                        //            strHTMLNew = strHTMLNew + @"<td colspan = ""3"" >";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >approval is required to continue.</span>";
                        //            strHTMLNew = strHTMLNew + @"</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"</tr> ";
                        //            strHTMLNew = strHTMLNew + @"<tr> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"</tr> ";
                        //            strBody = strBody.Replace(strHTMLOld, strHTMLNew);
                        //        }
                        //        //////////////Checkbox4//Checkbox4//Checkbox4//Checkbox4//Checkbox4////////////////////////////
                        //        //////////////Checkbox5//Checkbox5//Checkbox5//Checkbox5//Checkbox5////////////////////////////
                        //        strHTMLOld = @"<tr> ";
                        //        strHTMLOld = strHTMLOld + @"<td colspan = ""3"" >";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" ><input id=""Checkbox5"" type=""checkbox"" name=""Checkbox5"" checked=""checked""/></span>";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > they are at </span>";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" ><input id=""Text5"" type=""text"" name=""Text5"" style=""width: 200px; height: 23px"" /></span>";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > % through the course. </span>";
                        //        strHTMLOld = strHTMLOld + @"</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"</tr> ";
                        //        strHTMLOld = strHTMLOld + @"<tr> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"</tr> ";
                        //        if (Request.Form["Checkbox5"] == null)
                        //        {
                        //            strBody = strBody.Replace(strHTMLOld, "");
                        //        }
                        //        else
                        //        {
                        //            strHTMLNew = @"<tr> ";
                        //            strHTMLNew = strHTMLNew + @"<td colspan = ""3"" >";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > they are at </span>";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >***MESSAGE***</span>";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >% through the course. </span>";
                        //            strHTMLNew = strHTMLNew + @"</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td>";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"</tr> ";
                        //            strHTMLNew = strHTMLNew + @"<tr> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"</tr> ";
                        //            strMessage = HttpUtility.HtmlEncode(Request.Form["Text5"].ToString());
                        //            strMessage = strMessage.Replace("\n", "<br/>");
                        //            if (strMessage == "")
                        //            { strMessage = " "; }
                        //            strHTMLNew = strHTMLNew.Replace(@"***MESSAGE***", strMessage.ToString());
                        //            strBody = strBody.Replace(strHTMLOld, strHTMLNew);
                        //        }
                        //        //////////////Checkbox5//Checkbox5//Checkbox5//Checkbox5//Checkbox5////////////////////////////
                        //        //////////////Checkbox6//Checkbox6//Checkbox6//Checkbox6//Checkbox6////////////////////////////
                        //        strHTMLOld = @"<tr> ";
                        //        strHTMLOld = strHTMLOld + @"<td colspan = ""3"" >";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" ><input id=""Checkbox6"" type=""checkbox"" name=""Checkbox6"" checked=""checked""/></span>";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > their highest simulated exam score is </span>";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" ><input id=""Text6"" type=""text"" name=""Text6"" style=""width: 200px; height: 23px"" /></span>";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >  and </span>";
                        //        strHTMLOld = strHTMLOld + @"</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"</tr> ";
                        //        strHTMLOld = strHTMLOld + @"<tr> ";
                        //        strHTMLOld = strHTMLOld + @"<td colspan = ""3"" >";
                        //        strHTMLOld = strHTMLOld + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >should be a minimum of 80 to increase likelihood of passing the state exam.</span>";
                        //        strHTMLOld = strHTMLOld + @"</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"</tr> ";
                        //        strHTMLOld = strHTMLOld + @"<tr> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"<td> &nbsp;</td> ";
                        //        strHTMLOld = strHTMLOld + @"</tr> ";
                        //        if (Request.Form["Checkbox6"] == null)
                        //        {
                        //            strBody = strBody.Replace(strHTMLOld, "");
                        //        }
                        //        else
                        //        {
                        //            strHTMLNew = @"<tr> ";
                        //            strHTMLNew = strHTMLNew + @"<td colspan = ""3"" >";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > their highest simulated exam score is </span>";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >***MESSAGE***</span>";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > and </span>";
                        //            strHTMLNew = strHTMLNew + @"</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td>";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"</tr> ";
                        //            strHTMLNew = strHTMLNew + @"<tr> ";
                        //            strHTMLNew = strHTMLNew + @"<td colspan = ""3"" >";
                        //            strHTMLNew = strHTMLNew + @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >should be a minimum of 80 to increase likelihood of passing the state exam.</span>";
                        //            strHTMLNew = strHTMLNew + @"</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"</tr> ";
                        //            strHTMLNew = strHTMLNew + @"<tr> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"<td> &nbsp;</td> ";
                        //            strHTMLNew = strHTMLNew + @"</tr> ";
                        //            strMessage = HttpUtility.HtmlEncode(Request.Form["Text6"].ToString());
                        //            strMessage = strMessage.Replace("\n", "<br/>");
                        //            if (strMessage == "")
                        //            { strMessage = " "; }
                        //            strHTMLNew = strHTMLNew.Replace(@"***MESSAGE***", strMessage.ToString());
                        //            strBody = strBody.Replace(strHTMLOld, strHTMLNew);
                        //        }
                        //        //////////////Checkbox6//Checkbox6//Checkbox6//Checkbox6//Checkbox6////////////////////////////
                        //        Session["strCompareXML"] = @"<Communication>Message</Communication>";
                        break;
                    default:
                        //strBody = DIV2.InnerHtml.ToString();
                        //Session["strCompareXML"] = @"<Communication>Message</Communication>";

                        //DIV2.InnerHtml = strBody;
                        break;
                }

                if (strEmailCC.ToString() != string.Empty)
                {
                    strEmailTo = strEmailTo.ToString() + "," + strEmailCC.ToString();
                }

                //strEmploymentCommunicationID = GetEmploymentCommunicationID(vInput.EmployeeID, vInput.EmploymentID, strEmailTo.ToString(), _mailFromAddress ?? "", strSubject.ToString() + @" - Ref#" + strEmploymentCommunicationID, strBody.ToString(), strCommunicationID, vInput.UserSOEID ?? "");
                intEmploymentCommunicationID = InsertEmploymentCommunication(vInput.EmployeeID, vInput.EmploymentID, strEmailTo, _mailFromAddress ?? "", strSubject + @" - Ref#" + intEmploymentCommunicationID.ToString() == "0" ? "" : intEmploymentCommunicationID.ToString(), strBody, vInput.CommunicationID, vInput.UserSOEID ?? "");
                strEmploymentCommunicationID = ("000000000000000" + intEmploymentCommunicationID.ToString()).Substring(("000000000000000" + intEmploymentCommunicationID.ToString()).Length - 15).ToString();

                //sendHtmlEmail(_mailFromAddress, strEmailTo.ToString(), strEmailCC.ToString(), strBody.ToString(), "Licensing Department", strSubject.ToString() + @" - Ref#" + strEmploymentCommunicationID, Session["strEmailAttachment"].ToString(), strEmploymentCommunicationID);

                //Session["strEmailAttachment"] = String.Empty;
                //EmailCCLabel.Text = string.Empty;
                //EmailSubjectTextBox.Text = string.Empty;
                //DIV2.InnerHtml = string.Empty;
                //EmailSubjectTextBox.ReadOnly = false;
                //EmailTemplateDropDownList.DataBind();
                //EmailTemplateDropDownList_SelectedIndexChanged(EmailTemplateDropDownList, EventArgs.Empty);

                result.Success = true;
                result.StatusCode = 200;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.Success = false;
                result.ObjData = null;
                result.ErrMessage = ex.Message;
                result.ErrMessage = "Server Error - Please Contact Support [REF# EMAIL-7519-49734].";

                _utilityService.LogError(ex.Message, result.ErrMessage, new { }, null);
            }

            return result;
        }

        //private String GetEmploymentCommunicationID(int vEmployeeID, int vEmploymentID, String strEmailTo, String strMailFromAddress, String strSubject, String strBody, String strCommunicationID, String strUserSOEID)
        //{

        //    SqlConnection conn = null;
        //    SqlCommand cmd = null;
        //    int intEmploymentCommunicationID = 0;
        //    try
        //    {
        //        conn = new SqlConnection(_connectionString);
        //        conn.Open();
        //        cmd = new SqlCommand("uspEmploymentCommunicationInsert", conn);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.Parameters.Add(new SqlParameter("@EmployeeID", vEmployeeID));
        //        cmd.Parameters.Add(new SqlParameter("@EmploymentID", vEmploymentID));
        //        cmd.Parameters.Add(new SqlParameter("@EmailTo", strEmailTo));
        //        cmd.Parameters.Add(new SqlParameter("@MailFromAddress", strMailFromAddress));
        //        cmd.Parameters.Add(new SqlParameter("@Subject", strSubject));
        //        cmd.Parameters.Add(new SqlParameter("@Body", strBody));
        //        //cmd.Parameters.Add(new SqlParameter("@CompareXML", Session["strCompareXML"]));
        //        //cmd.Parameters.Add(new SqlParameter("@EmailAttachment", Session["strEmailAttachment"].ToString()));
        //        cmd.Parameters.Add(new SqlParameter("@CommunicationID", strCommunicationID));
        //        cmd.Parameters.Add(new SqlParameter("@UserSOEID", strUserSOEID));


        //        SqlParameter outPutVal = new SqlParameter("@EmploymentCommunicationID", SqlDbType.Int);
        //        outPutVal.Direction = ParameterDirection.Output;
        //        cmd.Parameters.Add(outPutVal);

        //        cmd.ExecuteNonQuery();


        //        if (outPutVal.Value != DBNull.Value)
        //        {
        //            intEmploymentCommunicationID = Convert.ToInt32(outPutVal.Value);
        //        }
        //        else
        //        {
        //            intEmploymentCommunicationID = 0;
        //        }

        //    }
        //    catch (SqlException mySQLEx)
        //    {
        //        //Response.Write(mySQLEx.Message);
        //        return "0";
        //    }
        //    catch (System.Exception myex)
        //    {
        //        //Response.Write(myex.Message);
        //        return "0";
        //    }

        //    finally
        //    {
        //        if (conn != null)
        //        {
        //            conn.Close();
        //        }
        //        if (cmd != null)
        //        {
        //            cmd = null;
        //        }
        //    }

        //    return intEmploymentCommunicationID.ToString();
        //}
        private int InsertEmploymentCommunication(int vEmployeeID, int vEmploymentID, String strEmailTo, String strMailFromAddress, String strSubject, String strBody, int strCommunicationID, String strUserSOEID)
        {
            // EMFTEST
            return 0;

            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var employmentCommunication = new EmploymentCommunication
                    {
                        EmployeeId = vEmployeeID,
                        EmploymentId = vEmploymentID,
                        EmailTo = strEmailTo,
                        EmailFrom = strMailFromAddress,
                        EmailSubject = strSubject,
                        EmailBodyHtml = strBody,
                        //CompareXml = Session["strCompareXML"],
                        //EmailAttachments = Session["strEmailAttachment"],
                        CommunicationId = strCommunicationID,
                        EmailCreator = strUserSOEID,
                        EmailCreateDate = DateTime.Now,
                        EmailSentDate = null
                    };

                    _db.EmploymentCommunications.Add(employmentCommunication);
                    _db.SaveChanges();

                    int employmentCommunicationID = employmentCommunication.EmploymentCommunicationId; // Assuming 'Id' is the primary key property

                    // Audit logging
                    _utilityService.LogAudit("EmploymentCommunications", employmentCommunicationID, strUserSOEID, "Insert", "EmploymentCommunicationID", employmentCommunicationID.ToString());

                    transaction.Commit();

                    return employmentCommunicationID;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    // Handle or throw exception as needed
                    throw;
                }
            }
        }
        //private void sendHtmlEmail(string from_Email, string to_Email, string cc_Email, string body, string from_Name, string Subject, string strAttachment, string strEmploymentCommunicationID)
        //{
        //    try
        //    {

        //        Global.CreateLog("   Send email for CommunicationID: " + strEmploymentCommunicationID);

        //        if (strSendToTest == "Yes")
        //        {
        //            from_Email = strTestFromEmail;
        //            to_Email = strTestToEmail;
        //            cc_Email = strTestCCEmail;
        //        }

        //        body = body.Replace(@"<img alt = """" src = ""../Pictures/OneMainSolutionsHorizontal.jpg""", @"<img src = cid:myImageID ");

        //        //@"<img alt = """" src = ""../Pictures/OneMain Solutions_Horizontal.jpg""" 
        //        //@"<img src = cid:myImageID>"


        //        //create an instance of new mail message
        //        MailMessage mail = new MailMessage();

        //        //set the HTML format to true
        //        mail.IsBodyHtml = true;

        //        ////create Alrternative HTML view
        //        AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");

        //        //Add Image
        //        var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
        //        var logoimage = Path.Combine(outPutDirectory, "Pictures\\OneMainSolutionsHorizontal.jpg");
        //        string relLogo = new Uri(logoimage).LocalPath;
        //        LinkedResource theEmailImage = new LinkedResource(relLogo);
        //        theEmailImage.ContentId = "myImageID";

        //        //Add the Image to the Alternate view
        //        htmlView.LinkedResources.Add(theEmailImage);
        //        //Add view to the Email Message
        //        mail.AlternateViews.Add(htmlView);

        //        //set the "from email" address and specify a friendly 'from' name
        //        mail.From = new MailAddress(from_Email, from_Name);

        //        //set the "to" email address
        //        mail.To.Add(to_Email);

        //        //CC
        //        if (!(cc_Email == String.Empty))
        //        {
        //            mail.CC.Add(cc_Email);
        //        }

        //        //Bcc to group box for Teleform
        //        mail.Bcc.Add(from_Email);

        //        //set the Email subject
        //        mail.Subject = Subject;


        //        string[] paths = strAttachment.Split('|');
        //        System.Net.Mail.Attachment attachment;
        //        foreach (var path in paths)
        //        {
        //            if (path.Length > 0)
        //            {
        //                attachment = new System.Net.Mail.Attachment(path);
        //                //string test = attachment.Name.Substring(36, attachment.Name.Length - 36);
        //                //attachment.Name = attachment.Name.Substring(36, attachment.Name.Length - 36);
        //                String test = attachment.Name;
        //                mail.Attachments.Add(attachment);
        //            }
        //        }

        //        //System.Net.Mail.Attachment attach6ment;
        //        //attachment = new System.Net.Mail.Attachment(strAttachment);
        //        //string test = attachment.Name;

        //        //mail.Attachments.Add(attachment);

        //        //set the SMTP info
        //        SmtpClient smtp = new SmtpClient(mailServer);

        //        //send the email
        //        smtp.Send(mail);

        //        DIV2.InnerHtml = body;

        //        int intEmploymentCommunicationID = 0;
        //        if (strEmploymentCommunicationID != string.Empty)
        //        {
        //            intEmploymentCommunicationID = Convert.ToInt32(strEmploymentCommunicationID);
        //        }
        //        else
        //        {
        //            intEmploymentCommunicationID = 0;
        //        }

        //        EmailSentUpdate(intEmploymentCommunicationID);

        //    }
        //    catch (System.Exception myex)
        //    {
        //        Response.Write(myex.Message);
        //    }
        //}
        //private void EmailSentUpdate(int intEmploymentCommunicationID)
        //{
        //    SqlConnection conn = null;
        //    SqlCommand cmd = null;
        //    DataSet ds = new DataSet();

        //    try
        //    {
        //        conn = new SqlConnection(_connectionString);
        //        conn.Open();
        //        cmd = new SqlCommand("uspEmploymentCommunicationUpdate", conn);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Add(new SqlParameter("@EmploymentCommunicationID", intEmploymentCommunicationID));
        //        cmd.Parameters.Add(new SqlParameter("@UserSOEID", Session["UserSOEID"].ToString()));

        //        cmd.ExecuteNonQuery();

        //    }
        //    catch (SqlException mySQLEx)
        //    {
        //        //Response.Write(mySQLEx.Message);
        //    }
        //    catch (System.Exception myex)
        //    {
        //        //Response.Write(myex.Message);
        //    }
        //    finally
        //    {
        //        if (conn != null)
        //        {
        //            conn.Close();
        //        }
        //        if (cmd != null)
        //        {
        //            cmd = null;
        //        }
        //    }

        //}
    }
}
