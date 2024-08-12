using Azure;
using Azure.Core;
using DataModel.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        private readonly bool? _isSendtoTest;
        private readonly string? _mailFromAddress;
        private readonly string? _mailToAddress;
        private readonly string? _mailCCAddress;
        private readonly string? _mailBCCAddress;
        private readonly string? _testmailToAddress;
        private readonly string? _testmailCCAddress;
        private readonly string? _testmailFromAddress;
        private string? _strEMAILATTACHMENT = string.Empty;

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
            string isSendtoTestKey = $"EnvironmentSettings:{environment}:isSendtoTest";
            string testmailToAddressKey = $"EnvironmentSettings:{environment}:testmailToAddress";
            string testmailCCAddressKey = $"EnvironmentSettings:{environment}:testmailCCAddress";
            string testmailFromAddressKey = $"EnvironmentSettings:{environment}:testmailFromAddress";

            // Retrieve the values based on the constructed keys
            _mailServer = _config.GetValue<string>(mailServerKey);
            _mailFromAddress = _config.GetValue<string>(mailFromAddressKey);
            _isSendtoTest = _config.GetValue<bool?>(isSendtoTestKey);
            _testmailToAddress = _config.GetValue<string>(testmailToAddressKey);
            _testmailCCAddress = _config.GetValue<string>(testmailCCAddressKey);
            _testmailFromAddress = _config.GetValue<string>(testmailFromAddressKey);
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
            var comms = _db.Communications
                            .Where(c => c.CommunicationId == vCommunicationID)
                            .FirstOrDefault();

            var docAttPath = _config.GetSection("EmailAttachmentDocs:OneTrakDocumentPath").Get<string>() ?? null;
            List<string> _attachments;

            var result = new ReturnResult();
            try
            {
                switch (vCommunicationID)
                {
                    case 1: // "APP-Court Documents"
                        var appCourtDocHTML = _emailTemplateService.GetCourtDocHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appCourtDocHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = comms.DocTypeAbv + " - " + comms.CommunicationName, isTemplateFound = true };
                        break;
                    case 2: // "APP-Incomplete"
                        var incompleteHTML = _emailTemplateService.GetIncompleteHTML(vEmploymentID);
                        result.ObjData = new { Header = incompleteHTML.Item1.ToString(), Footer = incompleteHTML.Item2.ToString(), DocSubType = comms.DocSubType ?? null, Subject = comms.CommunicationName, isTemplateFound = true };
                        break;
                    case 3: // "APP-Application Not Received"
                        var appNotReceivedHTML = _emailTemplateService.GetApplNotRecievedHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appNotReceivedHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = comms.DocTypeAbv + " - " + comms.CommunicationName, isTemplateFound = true };
                        break;
                    case 4: // "APP-License Copy Display (GA,KY,MT,WA,WY)"
                        var appLicenseCopyDisplayGaKyMtWaWyHTML = _emailTemplateService.GetAPPLicCopyDisplayGaKyMtWaWyHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appLicenseCopyDisplayGaKyMtWaWyHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = comms.DocTypeAbv + " - " + comms.CommunicationName, isTemplateFound = true };
                        break;
                    case 5: // "APP-License Copy"
                        var appLicenseCopyHTML = _emailTemplateService.GetAPPLicenseCopyHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appLicenseCopyHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = comms.DocTypeAbv + " - " + comms.CommunicationName, isTemplateFound = true };
                        break;
                    case 6: // "APP-Employment History"
                        var employmentHistoryHTML = _emailTemplateService.GetEmploymentHistoryHTML(vEmploymentID);
                        result.ObjData = new { Header = employmentHistoryHTML.Item1.ToString(), Footer = employmentHistoryHTML.Item2.ToString(), DocSubType = comms.DocSubType ?? null, Subject = comms.CommunicationName, isTemplateFound = true };
                        break;
                    case 7: // "APP-Exam Scheduled"
                        var appExamScheduledHTML = _emailTemplateService.GetExamScheduledHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appExamScheduledHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION EXAM SCHEDULED CONFIRMATION", isTemplateFound = true };
                        break;
                    case 8: // "APP-Exam Scheduled No Cert"
                        var appExamScheduledNoCertHTML = _emailTemplateService.GetExamScheduledNoCertHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appExamScheduledNoCertHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION EXAM SCHEDULED CONFIRMATION", isTemplateFound = true };
                        break;
                    case 9: // "APP-License Copy-CA"
                        var appLicenseCopyCAHTML = _emailTemplateService.GetRENLicenseCopyCAHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appLicenseCopyCAHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "RENEWAL LICENSE COPY_CA", isTemplateFound = true };
                        break;
                    case 10: // "REN-License Copy"
                        var appRENLicenseCopyHTML = _emailTemplateService.GetRENLicenseCopyCAHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appRENLicenseCopyHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "RENEWAL LICENSE COPY", isTemplateFound = true };
                        break;
                    case 11: // "REN-License Copy Display (GA,KY,MT,WA,WY)"
                        var appRENLicenseCopyGaKyMtWaWyHTML = _emailTemplateService.GetRENLicenseCopyGaKyMtWaWyHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appRENLicenseCopyGaKyMtWaWyHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "RENEWAL LICENSE COPY FOR DISPLAY", isTemplateFound = true };
                        break;
                    //case 12: // "APP-ExamFX Registration-Life" 
                    //    var appExamFxHTML = _emailTemplateService.GetRENLicenseCopyCAHTML(vEmploymentID);
                    //    result.ObjData = new { HTMLContent = appExamFxHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "RENEWAL LICENSE COPY FOR DISPLAY", isTemplateFound = true };
                    //    break;
                    case 13: // "CE-Unmonitored"
                        var appCEUnmonitoredHTML = _emailTemplateService.GetUmonitoredHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appCEUnmonitoredHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "CE ORDERED_NOT MONITORED", isTemplateFound = true };
                        break;
                    case 14: // "CE-Monitored"
                        var appCEMonitoredHTML = _emailTemplateService.GetMonitoredHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appCEMonitoredHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "CE ORDERED_MONITORED", isTemplateFound = true };
                        break;
                    case 15: // "CE-Monitored IN"
                        var appCEMonitoredInHTML = _emailTemplateService.GetMonitoredInHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appCEMonitoredInHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "CE ORDERED_MONITORED_IN", isTemplateFound = true };
                        break;
                    case 16: // "Webinar-IL"
                        var appWebinarILHTML = _emailTemplateService.GetWebinarIlHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appWebinarILHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "CE WEBINAR_IL", isTemplateFound = true };
                        break;
                    case 17: // "Life PLS"
                        var appLifePLSHTML = _emailTemplateService.GetLifePLSHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appLifePLSHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION LIFE LICENSE TRAINING PLS", isTemplateFound = true };
                        break;
                    case 18: // "Life PLS Plus"
                        var appLifePLSPlusHTML = _emailTemplateService.GetLifePlsPlusHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appLifePLSPlusHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION LIFE LICENSE TRAINING", isTemplateFound = true };
                        break;
                    case 32: // "OK TO SELL"
                        var appOkToSellHTML = _emailTemplateService.GetOkToSELLHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appOkToSellHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "Ok To Sell", isTemplateFound = true };
                        break;
                    case 33: // "APP-{MESSAGE}"
                        var appHTML = _emailTemplateService.GetMessageHTML(vEmploymentID);
                        result.ObjData = new { Header = appHTML.Item1.ToString(), Footer = appHTML.Item2.ToString(), DocSubType = comms.DocSubType ?? null, Subject = comms.CommunicationName, isTemplateFound = true };
                        break;
                    case 35: // "CE-{MESSAGE}"
                        var ceHTML = _emailTemplateService.GetMessageHTML(vEmploymentID);
                        result.ObjData = new { Header = ceHTML.Item1.ToString(), Footer = ceHTML.Item2.ToString(), DocSubType = comms.DocSubType ?? null, Subject = comms.CommunicationName, isTemplateFound = true };
                        break;
                    case 36: // "REN-{MESSAGE}"
                        var renHTML = _emailTemplateService.GetMessageHTML(vEmploymentID);
                        result.ObjData = new { Header = renHTML.Item1.ToString(), Footer = renHTML.Item2.ToString(), DocSubType = comms.DocSubType ?? null, Subject = comms.CommunicationName, isTemplateFound = true };
                        break;
                    case 37: // "PRO-{MESSAGE}"
                        var proHTML = _emailTemplateService.GetMessageHTML(vEmploymentID);
                        result.ObjData = new { Header = proHTML.Item1.ToString(), Footer = proHTML.Item2.ToString(), DocSubType = comms.DocSubType ?? null, Subject = comms.CommunicationName, isTemplateFound = true };
                        break;
                    case 41: // "Background Release"
                        _attachments = GetAttachments("BackgroundReleaseDocs");
                        var appBackgroundReleaseHTML = _emailTemplateService.GetBackgroundReleaseHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appBackgroundReleaseHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION BACKGROUND RELEASE IS NEEDED", isTemplateFound = true, DocAttachmentPath = docAttPath + "Templates/",  Attachments = _attachments };
                        break;
                    case 42: // "Background Disclosure Link"
                        _attachments = GetAttachments("BackgroundDisclosureLink");
                        var appBackgroundDisclosureLinkHTML = _emailTemplateService.GetBackgroundDisclosureLinkHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appBackgroundDisclosureLinkHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION BACKGROUND DISCLOSURE LINK", isTemplateFound = true, DocAttachmentPath = docAttPath + "Templates/", Attachments = _attachments };
                        break;
                    case 45: // "Exam Scheduled-CREDIT"
                        var appExamScheduledCreditHTML = _emailTemplateService.GetExamScheduledCreditHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appExamScheduledCreditHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION EXAM SCHEDULED CONFIRMATION-CREDIT", isTemplateFound = true };
                        break;
                    case 46: // "Clearance Letter"
                        var appClearanceLetterHTML = _emailTemplateService.GetClearenceLetterHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appClearanceLetterHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "PROFILE CLEARANCE LETTER", isTemplateFound = true };
                        break;
                    case 47: // "Life PLS Plus-IL"
                        var appLifePLsPlusILHTML = _emailTemplateService.GetLifePLsPlusILHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appLifePLsPlusILHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION LIFE LICENSE TRAINING IL", isTemplateFound = true };
                        break;
                    case 48: // "Child Support"
                        var appChildSupportHTML = _emailTemplateService.GetChildSupportHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appChildSupportHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION CHILD SUPPORT DOCUMENTS", isTemplateFound = true };
                        break;
                    case 49: // "Citizenship Document"
                        var appCitizenshipDocumentHTML = _emailTemplateService.GetCitizenDocumentHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appCitizenshipDocumentHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION CITIZENSHIP DOCUMENTS", isTemplateFound = true };
                        break;
                    case 50: // "Notary Missing"
                        var appNotaryMissingHTML = _emailTemplateService.GetNotoryMissingHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appNotaryMissingHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION NOTARY MISSING", isTemplateFound = true };
                        break;
                    case 51: // "Notary Missing TN"
                        var appNotaryMissingTNHTML = _emailTemplateService.GetNotoryMissingTnHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appNotaryMissingTNHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION NOTARY MISSING TN", isTemplateFound = true };
                        break;
                    case 52: // "Application Required-HI"
                        var appApplicationRequiredHIHTML = _emailTemplateService.GetApplicationRequiredHIHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appApplicationRequiredHIHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION REQUIRED HI", isTemplateFound = true };
                        break;
                    case 53: // "Application Required-TN"
                        var appApplicationRequiredTnHTML = _emailTemplateService.GetApplicationRequiredTNHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appApplicationRequiredTnHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION REQUIRED TN", isTemplateFound = true };
                        break;
                    case 54: // "Fingerprint Required-AZ"
                        var appFingerprintRequiredAZHTML = _emailTemplateService.GetFingerprintRequiredAZHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appFingerprintRequiredAZHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION FINGERPRINT REQUIRED_AZ", isTemplateFound = true };
                        break;
                    case 55: // "Fingerprint Required-LA"
                        var appFingerprintRequiredLAHTML = _emailTemplateService.GetFingerprintRequiredLAHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appFingerprintRequiredLAHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION FINGERPRINT REQUIRED_LA", isTemplateFound = true };
                        break;
                    case 56: // "Fingerprint Required-Credit"
                        var appFingerprintRequiredCreditHTML = _emailTemplateService.GetFingerprintRequiredCreditHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appFingerprintRequiredCreditHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION FINGERPRINT REQUIRED", isTemplateFound = true };
                        break;
                    case 57: // "Fingerprint Scheduled-AL"
                        var appFingerprintScheduledALHTML = _emailTemplateService.GetFingerprintScheduledALHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appFingerprintScheduledALHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION FINGERPRINT SCHEDULED_AL", isTemplateFound = true };
                        break;
                    case 58: // "Fingerprint Scheduled-TN"
                        var appFingerprintScheduledTNHTML = _emailTemplateService.GetFingerprintScheduledTNHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appFingerprintScheduledTNHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION FINGERPRINT SCHEDULED_TN", isTemplateFound = true };
                        break;
                    case 59: // "Fingerprint Scheduled-NM"
                        var appFingerprintScheduledNMHTML = _emailTemplateService.GetFingerprintScheduledNMHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appFingerprintScheduledNMHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION FINGERPRINT SCHEDULED_NM", isTemplateFound = true };
                        break;
                    case 60: // "Fingerprint Scheduled-PA"
                        var appFingerprintScheduledPAHTML = _emailTemplateService.GetFingerprintScheduledPAHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appFingerprintScheduledPAHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION FINGERPRINT SCHEDULED_PA", isTemplateFound = true };
                        break;
                    case 61: // "Compliance Certificate"
                        _attachments = GetAttachments("ComplianceCertificate");
                        var appComplianceCertificateHTML = _emailTemplateService.GetComplianceCertificateHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appComplianceCertificateHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION CERTIFICATE OF COMPLIANCE", isTemplateFound = true, DocAttachmentPath = docAttPath + "Templates/", Attachments = _attachments };
                        break;
                    case 62: // "Compliance Certificate-End"
                        _attachments = GetAttachments("ComplianceCertificate-End");
                        var appComplianceCertificateEndHTML = _emailTemplateService.GetComplianceCertificateEndHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appComplianceCertificateEndHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION CERTIFICATE OF COMPLIANCE FOR ENDORSEES", isTemplateFound = true, DocAttachmentPath = docAttPath + "Templates/", Attachments = _attachments };
                        break;
                    case 63: // "Life PLS-IL"
                        var appLifePLsILHTML = _emailTemplateService.GetLifePlsILHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appLifePLsILHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION LIFE LICENSE TRAINING IL", isTemplateFound = true };
                        break;
                    case 64: // "Fingerprint Required-UT"
                        var appFingerprintRequiredUTHTML = _emailTemplateService.GetFingerprintRequiredUTHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appFingerprintRequiredUTHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION FINGERPRINT REQUIRED_UT", isTemplateFound = true };
                        break;
                    case 65: // "AD Banker Registration-IL"
                        var appADBankerRegistrationILHTML = _emailTemplateService.GetADBankerRegistrationILHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appADBankerRegistrationILHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION AD BANKER REGISTRATION CONFIRMATION_LIFE_IL", isTemplateFound = true };
                        break;
                    case 66: // "Exam Scheduled-PA"
                        var appExamScheduledPAHTML = _emailTemplateService.GetExamScheduledPAHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appExamScheduledPAHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION EXAM SCHEDULED CONFIRMATION_PA", isTemplateFound = true };
                        break;
                    case 67: // "Address Change"
                        var appAddressChangeHTML = _emailTemplateService.GetAddressChangeHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appAddressChangeHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "PROFILE ADDRESS CHANGE", isTemplateFound = true };
                        break;
                    case 68: // "Name Change-IL"
                        var appNameChangeILHTML = _emailTemplateService.GetNameChangeILHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appNameChangeILHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "PROFILE NAME CHANGE_IL", isTemplateFound = true };
                        break;
                    case 69: // "Fingerprint Scheduled-GA"
                        var appFingerprintScheduledGAHTML = _emailTemplateService.GetFingerprintScheduledGAHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appFingerprintScheduledGAHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION FINGERPRINT SCHEDULED-GA", isTemplateFound = true };
                        break;
                    case 70: // "Name Change"
                        var appNameChangeHTML = _emailTemplateService.GetNameChangeHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appNameChangeHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "PROFILE NAME CHANGE", isTemplateFound = true };
                        break;
                    case 71: // "Name Change (AZ, LA, MI, NM, WV)"
                        var appNameChangeAzLaMiNmWvHTML = _emailTemplateService.GetNameChangeAzLaMiNmWvHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appNameChangeAzLaMiNmWvHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "PROFILE NAME CHANGE(AZ, LA, MI, NM, WV)", isTemplateFound = true };
                        break;
                    case 72: // "Name Change AL"
                        var appNameChangeALHTML = _emailTemplateService.GetNameChangeALHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appNameChangeALHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "PROFILE NAME CHANGE AL", isTemplateFound = true };
                        break;
                    case 73: // "Expired Certificate-MD"
                        var appExpiredCertificateMDHTML = _emailTemplateService.GetExpiredCertificateMDHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appExpiredCertificateMDHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION PRELICENSING CERTIFICATE EXPIRED_MD", isTemplateFound = true };
                        break;
                    case 74: // "Expired Certificate-AL"
                        var appExpiredCertificateALHTML = _emailTemplateService.GetExpiredCertificateALHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appExpiredCertificateALHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION PRELICENSING CERTIFICATE EXPIRED_AL", isTemplateFound = true };
                        break;
                    case 75: // "Expired Certificate-TN"
                        var appExpiredCertificateTNHTML = _emailTemplateService.GetExpiredCertificateTNHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appExpiredCertificateTNHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION PRELICENSING CERTIFICATE EXPIRED_TN", isTemplateFound = true };
                        break;
                    case 77: // "State Exam Exception"
                        var appStateExamExceptionHTML = _emailTemplateService.GetStateExamExceptionHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appStateExamExceptionHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION STATE EXAM EXCEPTION", isTemplateFound = true };
                        break;
                    case 78: // "Application Required-WI"
                        var appApplicationRequiredWIHTML = _emailTemplateService.GetApplicationRequiredWIHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appApplicationRequiredWIHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION REQUIRED_WI", isTemplateFound = true };
                        break;
                    case 79: // "Exam Scheduled-ND"
                        var appExamScheduledNDHTML = _emailTemplateService.GetExamScheduledNDHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appExamScheduledNDHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION EXAM SCHEDULED CONFIRMATION_ND", isTemplateFound = true };
                        break;
                    case 80: // "Fingerprint Scheduled-WV"
                        var appFingerprintScheduledWVHTML = _emailTemplateService.GetFingerprintScheduledWVHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appFingerprintScheduledWVHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION FINGERPRINT SCHEDULED_WV", isTemplateFound = true };
                        break;
                    case 113: // "Credit Membership Sales Training"
                        var appCreditMembershipSalesTrainingHTML = _emailTemplateService.GetCreditMembershipSalesTrainingHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appCreditMembershipSalesTrainingHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION COMPANY TRAINING", isTemplateFound = true };
                        break;
                    case 114: // "Non-Credit Training"
                        var appNonCreditTrainingHTML = _emailTemplateService.GetNonCreditTrainingHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appNonCreditTrainingHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION COMPANY TRAINING", isTemplateFound = true };
                        break;
                    case 115: // "AD Banker Registration-FL"
                        _attachments = GetAttachments("ADBankerRegistration-FL");
                        var appADBankerRegistrationFLHTML = _emailTemplateService.GetADBankerRegistrationFLHTML(vEmploymentID);
                        result.ObjData = new { HTMLContent = appADBankerRegistrationFLHTML.Item1.ToString(), DocSubType = comms.DocSubType ?? null, Subject = "APPLICATION AD BANKER REGISTRATION CONFIRMATION_LIFE_FL", isTemplateFound = true, DocAttachmentPath = docAttPath + "Templates/", Attachments = _attachments };
                        break;
                    default:
                        result.ObjData = new { htmlContent = @"<div class=""col d-flex justify-content-center mt-5"">
                                                <span class=""material-symbols-outlined"">unknown_document</span>
                                                <div class=""ms-3"">
                                                    <h3>Unknown Document Type</h3>
                                                    <p>Document type not found.</p>
                                                </div>
                                           </div>", DocSubType = string.Empty, Subject = (string)null, isTemplateFound = false };
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
                intEmploymentCommunicationID = InsertEmploymentCommunication(vInput.EmployeeID, vInput.EmploymentID, strEmailTo, _mailFromAddress ?? "", string.Format("{0} - Ref# {1}", strSubject, intEmploymentCommunicationID.ToString() == "0" ? "" : intEmploymentCommunicationID.ToString()), strBody, vInput.CommunicationID, vInput.UserSOEID ?? "");
                strEmploymentCommunicationID = ("000000000000000" + intEmploymentCommunicationID.ToString()).Substring(("000000000000000" + intEmploymentCommunicationID.ToString()).Length - 15).ToString();

                sendHtmlEmail(_mailFromAddress, strEmailTo.ToString(), strEmailCC.ToString(), strBody.ToString(), "Licensing Department", strSubject.ToString() + @" - Ref#" + strEmploymentCommunicationID, _strEMAILATTACHMENT, strEmploymentCommunicationID, vInput.UserSOEID);

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
                        CompareXml = "<Communication>Message</Communication>",
                        EmailAttachments = _strEMAILATTACHMENT,
                        CommunicationId = strCommunicationID,
                        EmailCreator = strUserSOEID,
                        EmailCreateDate = DateTime.Now,
                        EmailSentDate = null
                    };

                    _db.EmploymentCommunications.Add(employmentCommunication);
                    _db.SaveChanges();

                    int employmentCommunicationID = employmentCommunication.EmploymentCommunicationId; // Assuming 'Id' is the primary key property






                    // Audit logging       TBD => COMMENTED DUE TO ERROR in UTILITY SERVICE
                    //_utilityService.LogAudit("EmploymentCommunications", employmentCommunicationID, strUserSOEID, "Insert", "EmploymentCommunicationID", employmentCommunicationID.ToString());







                    transaction.Commit();

                    return employmentCommunicationID;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // Handle or throw exception as needed
                    _utilityService.LogError(ex.Message, "Server Error - Please Contact Support [REF# EMAIL-7511-59434].", new { }, null);

                    throw;
                }
            }
        }
        private void sendHtmlEmail(string vFrom_Email, string vTo_Email, string vCc_Email, string vBody, string vFrom_Name, string vSubject, string vStrAttachment, string vStrEmploymentCommunicationID, string vUserSOEID)
        {
            try
            {

                //Global.CreateLog("   Send email for CommunicationID: " + strEmploymentCommunicationID);
                _utilityService.LogInfo("Send email for CommunicationID: " + vStrEmploymentCommunicationID, null);

                //if ((bool)_isSendtoTest)
                //{
                //    from_Email = _testmailToAddress ?? "";
                //    to_Email = _testmailFromAddress ?? "";
                //    cc_Email = _testmailCCAddress ?? "";
                //}

                vBody = vBody.Replace(@"<img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg""", @"<img src = cid:myImageID ");

                //@"<img alt = """" src = ""../Pictures/OneMain Solutions_Horizontal.jpg""" 
                //@"<img src = cid:myImageID>"


                //create an instance of new mail message
                MailMessage mail = new MailMessage();

                //set the HTML format to true
                mail.IsBodyHtml = true;

                ////create Alrternative HTML view
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(vBody, null, "text/html");

                //Add Image
                //var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                var outPutDirectory = AppContext.BaseDirectory;
                var logoimage = Path.Combine(outPutDirectory, "Pictures\\OneMainSolutionsHorizontal.jpg");
                string relLogo = new Uri(logoimage).LocalPath;
                LinkedResource theEmailImage = new LinkedResource(relLogo);
                theEmailImage.ContentId = "myImageID";

                //Add the Image to the Alternate view
                htmlView.LinkedResources.Add(theEmailImage);
                //Add view to the Email Message
                mail.AlternateViews.Add(htmlView);

                //set the "from email" address and specify a friendly 'from' name
                mail.From = new MailAddress((bool)_isSendtoTest ? _testmailFromAddress != null ? _testmailFromAddress : string.Empty : vFrom_Email, (bool)_isSendtoTest ? "OneTrakV2-TEST" : vFrom_Name);

                //set the "to" email address
                mail.To.Add(vTo_Email);

                //CC
                string strCcEmail = (bool)_isSendtoTest ? _testmailCCAddress != null ? _testmailCCAddress : string.Empty : vCc_Email;
                if (!(strCcEmail == String.Empty))
                {
                    mail.CC.Add(strCcEmail);
                }

                //Bcc to group box for Teleform
                mail.Bcc.Add((bool)_isSendtoTest ? _testmailFromAddress != null ? _testmailFromAddress : string.Empty : vFrom_Email);

                //set the Email subject
                mail.Subject = vSubject;


                string[] paths = vStrAttachment.Split('|');
                System.Net.Mail.Attachment attachment;
                foreach (var path in paths)
                {
                    if (path.Length > 0)
                    {
                        attachment = new System.Net.Mail.Attachment(path);
                        //string test = attachment.Name.Substring(36, attachment.Name.Length - 36);
                        //attachment.Name = attachment.Name.Substring(36, attachment.Name.Length - 36);
                        String test = attachment.Name;
                        mail.Attachments.Add(attachment);
                    }
                }

                //System.Net.Mail.Attachment attach6ment;
                //attachment = new System.Net.Mail.Attachment(strAttachment);
                //string test = attachment.Name;

                //mail.Attachments.Add(attachment);

                //set the SMTP info
                SmtpClient smtp = new SmtpClient(_mailServer);

                //send the email
                smtp.Send(mail);

                // NOT SURE IF NEEDED...
                //DIV2.InnerHtml = vBody;

                int intEmploymentCommunicationID = 0;
                if (vStrEmploymentCommunicationID != string.Empty)
                {
                    intEmploymentCommunicationID = Convert.ToInt32(vStrEmploymentCommunicationID);
                }
                else
                {
                    intEmploymentCommunicationID = 0;
                }

                EmailSentUpdate(intEmploymentCommunicationID, vUserSOEID);

            }
            catch (System.Exception myex)
            {
                //Response.Write(myex.Message);
            }
        }
        private void EmailSentUpdate(int vIntEmploymentCommunicationID, string vUserSOEID)
        {
            //SqlConnection conn = null;
            //SqlCommand cmd = null;
            //DataSet ds = new DataSet();

            //try
            //{
            //    conn = new SqlConnection(_connectionString);
            //    conn.Open();
            //    cmd = new SqlCommand("uspEmploymentCommunicationUpdate", conn);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add(new SqlParameter("@EmploymentCommunicationID", vIntEmploymentCommunicationID));
            //    cmd.Parameters.Add(new SqlParameter("@UserSOEID", vUserSOEID));

            //    cmd.ExecuteNonQuery();

            //}
            //catch (SqlException mySQLEx)
            //{
            //    //Response.Write(mySQLEx.Message);
            //}
            //catch (System.Exception myex)
            //{
            //    //Response.Write(myex.Message);
            //}
            //finally
            //{
            //    if (conn != null)
            //    {
            //        conn.Close();
            //    }
            //    if (cmd != null)
            //    {
            //        cmd = null;
            //    }
            //}

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("uspEmploymentCommunicationUpdate", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@EmploymentCommunicationID", vIntEmploymentCommunicationID));
                    cmd.Parameters.Add(new SqlParameter("@UserSOEID", vUserSOEID));

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

        }
        protected List<string> GetAttachments(string vEmailTemplate)
        {
            switch (vEmailTemplate)
            {
                case "BackgroundReleaseDocs":
                    return _config.GetSection("EmailAttachmentDocs:BackgroundReleaseDocs").Get<List<string>>() ?? new List<string>();
                case "BackgroundDisclosureLink":
                    return _config.GetSection("EmailAttachmentDocs:BackgroundDisclosureLink").Get<List<string>>() ?? new List<string>();
                case "ComplianceCertificate":
                    return _config.GetSection("EmailAttachmentDocs:ComplianceCertificate").Get<List<string>>() ?? new List<string>();
                case "ComplianceCertificate-End":
                    return _config.GetSection("EmailAttachmentDocs:ComplianceCertificate-End").Get<List<string>>() ?? new List<string>();
                case "ADBankerRegistration-FL":
                    return _config.GetSection("EmailAttachmentDocs:ADBankerRegistration-FL").Get<List<string>>() ?? new List<string>();
                default:
                    break;
            }
            return new List<string>();
        }
    }
}
