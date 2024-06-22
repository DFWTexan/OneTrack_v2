using Azure.Core;
using DataModel.Response;
using Microsoft.AspNetCore.Mvc;
using OneTrack_v2.DbData;
using OneTrak_v2.DataModel;
using OneTrak_v2.Server.Services.Email.Templates;
using OneTrak_v2.Services.Model;
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

        public EmailService(AppDataContext db, IConfiguration config, IUtilityHelpService utilityHelpService, IEmailTemplateService emailTemplateService)
        {
            _db = db;
            _config = config;
            _connectionString = _config.GetConnectionString(name: "DefaultConnection");
            _utilityService = utilityHelpService;
            _emailTemplateService = emailTemplateService;
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
                            .Where(c => c.CommunicationId == vInput.EmailTemplateID)
                            .Select(c => c.DocTypeAbv + "-" + c.CommunicationName)
                            .FirstOrDefault();

            var result = new ReturnResult();
            try
            {

                String strSubject = String.Empty;
                String strBody = String.Empty;
                //String strEmailTo = String.Empty;
                //String strEmailCC = String.Empty;
                //String strEmploymentCommunicationID = String.Empty;
                //String strCommunicationID = String.Empty;
                //String strMessage = String.Empty;
                //String strHTMLNew = String.Empty;
                //String strHTMLOld = String.Empty;
                //ccMgrCheckBox.Checked = false;
                //ccDMCheckBox.Checked = false;
                //ccRMCheckBox.Checked = false;
                ////"Ok To Sell - Ref# 000000000000001"
                //strEmailTo = EmailToLabel.Text.ToString();
                //strEmailCC = EmailCCLabel.Text.ToString();
                //strSubject = EmailSubjectTextBox.Text.ToString();
                //strCommunicationID = CommunicationIDLabel.Text.ToString();

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
                    
                        
                        
                        //    case "CE-{MESSAGE}":
                    //        strHTMLNew = @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >***MESSAGE***</span>";
                    //        strHTMLOld = @"<textarea id = ""TextArea1"" cols = ""1"" name = ""TextArea1"" rows = ""20"" style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none; width: 600px; height: 200px""></textarea>";
                    //        strMessage = HttpUtility.HtmlEncode(Request.Form["TextArea1"].ToString());
                    //        strMessage = strMessage.Replace("\n", "<br/>");
                    //        if (strMessage == "")
                    //        { strMessage = " "; }
                    //        strHTMLNew = strHTMLNew.Replace(@"***MESSAGE***", strMessage.ToString());
                    //        strBody = DIV2.InnerHtml.Replace(strHTMLOld, strHTMLNew);
                    //        Session["strCompareXML"] = @"<Communication>Message</Communication>";
                    //        break;
                    //    case "REN-{MESSAGE}":
                    //        strHTMLNew = @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >***MESSAGE***</span>";
                    //        strHTMLOld = @"<textarea id = ""TextArea1"" cols = ""1"" name = ""TextArea1"" rows = ""20"" style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none; width: 600px; height: 200px""></textarea>";
                    //        strMessage = HttpUtility.HtmlEncode(Request.Form["TextArea1"].ToString());
                    //        strMessage = strMessage.Replace("\n", "<br/>");
                    //        if (strMessage == "")
                    //        { strMessage = " "; }
                    //        strHTMLNew = strHTMLNew.Replace(@"***MESSAGE***", strMessage.ToString());
                    //        strBody = DIV2.InnerHtml.Replace(strHTMLOld, strHTMLNew);
                    //        Session["strCompareXML"] = @"<Communication>Message</Communication>";
                    //        break;
                    //    case "PRO-{MESSAGE}":
                    //        strHTMLNew = @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >***MESSAGE***</span>";
                    //        strHTMLOld = @"<textarea id = ""TextArea1"" cols = ""1"" name = ""TextArea1"" rows = ""20"" style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none; width: 600px; height: 200px""></textarea>";
                    //        strMessage = HttpUtility.HtmlEncode(Request.Form["TextArea1"].ToString());
                    //        strMessage = strMessage.Replace("\n", "<br/>");
                    //        if (strMessage == "")
                    //        { strMessage = " "; }
                    //        strHTMLNew = strHTMLNew.Replace(@"***MESSAGE***", strMessage.ToString());
                    //        strBody = DIV2.InnerHtml.Replace(strHTMLOld, strHTMLNew);
                    //        Session["strCompareXML"] = @"<Communication>Message</Communication>";
                    //        break;
                    //    case "APP-Incomplete":
                    //        strHTMLNew = @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >***MESSAGE***</span>";
                    //        strHTMLOld = @"<textarea id = ""TextArea1"" cols = ""1"" name = ""TextArea1"" rows = ""20"" style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none; width: 600px; height: 200px""></textarea>";
                    //        strMessage = HttpUtility.HtmlEncode(Request.Form["TextArea1"].ToString());
                    //        strMessage = strMessage.Replace("\n", "<br/>");
                    //        if (strMessage == "")
                    //        { strMessage = " "; }
                    //        strHTMLNew = strHTMLNew.Replace(@"***MESSAGE***", strMessage.ToString());
                    //        strBody = DIV2.InnerHtml.Replace(strHTMLOld, strHTMLNew);
                    //        Session["strCompareXML"] = @"<Communication>Message</Communication>";
                    //        break;

                    //    case "APP-Employment History":
                    //        strHTMLNew = @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" >***MESSAGE***</span>";
                    //        strHTMLOld = @"<textarea id = ""TextArea1"" cols = ""1"" name = ""TextArea1"" rows = ""20"" style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none; width: 600px; height: 200px""></textarea>";
                    //        strMessage = HttpUtility.HtmlEncode(Request.Form["TextArea1"].ToString());
                    //        strMessage = strMessage.Replace("\n", "<br/>");
                    //        if (strMessage == "")
                    //        { strMessage = " "; }
                    //        strHTMLNew = strHTMLNew.Replace(@"***MESSAGE***", strMessage.ToString());
                    //        strBody = DIV2.InnerHtml.Replace(strHTMLOld, strHTMLNew);
                    //        Session["strCompareXML"] = @"<Communication>Message</Communication>";
                    //        break;

                    //    case "APP-Fingerprint Scheduled-GA":
                    //        strHTMLNew = @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >***MESSAGE***</span>";
                    //        strHTMLOld = @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > <input id = ""Text1"" type = ""text"" name = ""Text1"" style = ""width: 200px; height: 23px"" /></span>";
                    //        strMessage = HttpUtility.HtmlEncode(Request.Form["Text1"].ToString());
                    //        strMessage = strMessage.Replace("\n", "<br/>");
                    //        if (strMessage == "")
                    //        { strMessage = " "; }
                    //        strHTMLNew = strHTMLNew.Replace(@"***MESSAGE***", strMessage.ToString());
                    //        strBody = DIV2.InnerHtml.Replace(strHTMLOld, strHTMLNew);
                    //        Session["strCompareXML"] = @"<Communication>Message</Communication>";
                    //        break;

                    //    case "APP-Expired Certificate-MD":
                    //        strHTMLNew = @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >***MESSAGE***.</span>";
                    //        strHTMLOld = @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > <input id = ""Text1"" type = ""text"" name = ""Text1"" style = ""width: 200px; height: 23px"" />.</span>";
                    //        strMessage = HttpUtility.HtmlEncode(Request.Form["Text1"].ToString());
                    //        strMessage = strMessage.Replace("\n", "<br/>");
                    //        if (strMessage == "")
                    //        { strMessage = " "; }
                    //        strHTMLNew = strHTMLNew.Replace(@"***MESSAGE***", strMessage.ToString());
                    //        strBody = DIV2.InnerHtml.Replace(strHTMLOld, strHTMLNew);
                    //        Session["strCompareXML"] = @"<Communication>Message</Communication>";
                    //        break;

                    //    case "APP-Expired Certificate-AL":
                    //        strHTMLNew = @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >***MESSAGE****.</span>";
                    //        strHTMLOld = @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > <input id = ""Text1"" type = ""text"" name = ""Text1"" style = ""width: 200px; height: 23px"" />.</span>";
                    //        strMessage = HttpUtility.HtmlEncode(Request.Form["Text1"].ToString());
                    //        strMessage = strMessage.Replace("\n", "<br/>");
                    //        if (strMessage == "")
                    //        { strMessage = " "; }
                    //        strHTMLNew = strHTMLNew.Replace(@"***MESSAGE***", strMessage.ToString());
                    //        strBody = DIV2.InnerHtml.Replace(strHTMLOld, strHTMLNew);
                    //        Session["strCompareXML"] = @"<Communication>Message</Communication>";
                    //        break;

                    //    case "APP-Expired Certificate-TN":
                    //        strHTMLNew = @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: bold; font-style: normal; text-decoration: underline; background-color: #FFFF00;"" >***MESSAGE***.</span>";
                    //        strHTMLOld = @"<span style = ""font-family: Arial; color: #000000; font-size: 12pt; font-weight: 400; font-style: normal; text-decoration: none;"" > <input id = ""Text1"" type = ""text"" name = ""Text1"" style = ""width: 200px; height: 23px"" />.</span>";
                    //        strMessage = HttpUtility.HtmlEncode(Request.Form["Text1"].ToString());
                    //        strMessage = strMessage.Replace("\n", "<br/>");
                    //        if (strMessage == "")
                    //        { strMessage = " "; }
                    //        strHTMLNew = strHTMLNew.Replace(@"***MESSAGE***", strMessage.ToString());
                    //        strBody = DIV2.InnerHtml.Replace(strHTMLOld, strHTMLNew);
                    //        Session["strCompareXML"] = @"<Communication>Message</Communication>";
                    //        break;


                    //    case "APP-State Exam Exception":
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
                    //        break;

                    //    case "APP-ExamFX Course Renewal":
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
                    //        break;


                    default:
                        //strBody = DIV2.InnerHtml.ToString();
                        //Session["strCompareXML"] = @"<Communication>Message</Communication>";

                        //DIV2.InnerHtml = strBody;
                        break;
                }

                //if (strEmailCC.ToString() != string.Empty)
                //{
                //    strEmailTo = strEmailTo.ToString() + "," + strEmailCC.ToString();
                //}



                //strEmploymentCommunicationID = GetEmploymentCommunicationID(EmployeeIDLable.Text.ToString(), EmploymentIDLable.Text.ToString(), strEmailTo.ToString(), mailFromAddress.ToString(), strSubject.ToString() + @" - Ref#" + strEmploymentCommunicationID, strBody.ToString(), strCommunicationID, Session["UserSOEID"].ToString());
                //strEmploymentCommunicationID = ("000000000000000" + strEmploymentCommunicationID.ToString()).Substring(("000000000000000" + strEmploymentCommunicationID.ToString()).Length - 15).ToString();

                //sendHtmlEmail(mailFromAddress.ToString(), strEmailTo.ToString(), strEmailCC.ToString(), strBody.ToString(), "Licensing Department", strSubject.ToString() + @" - Ref#" + strEmploymentCommunicationID, Session["strEmailAttachment"].ToString(), strEmploymentCommunicationID);

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
    }
}
