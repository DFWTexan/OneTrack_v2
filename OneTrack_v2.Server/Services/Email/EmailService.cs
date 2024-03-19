using DataModel.Response;
using OneTrack_v2.DbData;
using OneTrak_v2.DataModel;
using OneTrak_v2.Server.Services.Email.Templates;
using OneTrak_v2.Services.Model;

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
                        result.ObjData = appHTML.Item1.ToString();
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

                return result;
            }
            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.ErrMessage = ex.Message;
                return result;
            }
        }
    }
}
