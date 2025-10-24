using System.Net.Mail;
using System.Text;

namespace OneTrack_v2.Services.Email
{
    public class AsyncEmailSender
    {
        private readonly EmailConfiguration _config;
        private readonly IUtilityHelpService _utilityService;

        public AsyncEmailSender(EmailConfiguration config, IUtilityHelpService utilityService)
        {
            _config = config;
            _utilityService = utilityService;

            // Log SMTP configuration
            _utilityService.LogInfo(
                $"SMTP Server Configuration: {_config.MailServer}",
                "Email Service Initialization");
        }

        public async Task SendEmailAsync(EmailMessage message)
        {
            using var mailMessage = CreateMailMessage(message);
            using var smtpClient = new SmtpClient(_config.MailServer)
            {
                Port = 25, // Default SMTP port if not specified
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true  // For internal SMTP servers
            };

            try
            {
                await AddEmailAttachmentsAsync(mailMessage, message);
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                _utilityService.LogError($"Failed to send email REF# SEND-8519-197543: {ex.Message} Server: {_config.MailServer}",
                    "Email Send Error", new { message.Subject, SmtpServer = _config.MailServer }, message.UserSOEID);
                throw;
            }
        }

        private MailMessage CreateMailMessage(EmailMessage message)
        {
            var mail = new MailMessage
            {
                IsBodyHtml = true,
                Subject = message.Subject,
                Body = ProcessEmailBody(message.Body)
            };

            // Set From address
            var fromAddress = _config.IsSendToTest ? _config.TestMailFromAddress : message.FromEmail;
            var fromName = _config.IsSendToTest ? "OneTrakV2-TEST" : message.FromName;
            mail.From = new MailAddress(fromAddress, fromName);

            // Set To address
            mail.To.Add(message.ToEmail);

            // Set CC if provided
            var ccEmail = _config.IsSendToTest ? _config.TestMailCCAddress : message.CcEmail;
            if (!string.IsNullOrEmpty(ccEmail))
            {
                mail.CC.Add(ccEmail);
            }

            // Set BCC
            mail.Bcc.Add(_config.IsSendToTest ? _config.TestMailFromAddress : message.FromEmail);

            return mail;
        }

        private string ProcessEmailBody(string body)
        {
            return body.Replace(
                @"<img alt = """" src = ""pictures/OneMainSolutionsHorizontal.jpg""", 
                @"<img src=""cid:myImageID""");
        }

        private async Task AddEmailAttachmentsAsync(MailMessage mail, EmailMessage message)
        {
            if (string.IsNullOrEmpty(message.Attachments))
                return;

            foreach (var path in message.Attachments.Split('|', StringSplitOptions.RemoveEmptyEntries))
            {
                await AttachFileWithRetryAsync(mail, path, message.UserSOEID);
            }

            await AddLogoImageAsync(mail, message);
        }

        private async Task AddLogoImageAsync(MailMessage mail, EmailMessage message)
        {
            var logoPath = GetLogoImagePath();
            if (!File.Exists(logoPath))
            {
                _utilityService.LogError($"Logo image not found at path: {logoPath}",
                    "Email Logo Missing", new { }, message.UserSOEID);
                return;
            }

            using var htmlView = AlternateView.CreateAlternateViewFromString(mail.Body, null, "text/html");
            using var logoStream = new FileStream(logoPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var logoResource = new LinkedResource(logoStream, "image/jpeg") { ContentId = "myImageID" };
            
            htmlView.LinkedResources.Add(logoResource);
            mail.AlternateViews.Add(htmlView);
        }

        private async Task AttachFileWithRetryAsync(MailMessage mail, string path, string userSOEID,
            int maxRetries = 3)
        {
            if (!File.Exists(path))
            {
                _utilityService.LogError($"Attachment not found: {path}",
                    "Email Attachment Missing", new { }, userSOEID);
                return;
            }

            var retryCount = 0;
            var baseDelay = TimeSpan.FromMilliseconds(100);

            while (retryCount < maxRetries)
            {
                try
                {
                    await using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                    using var memoryStream = new MemoryStream();
                    await fileStream.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;

                    var attachment = new Attachment(memoryStream, Path.GetFileName(path));
                    mail.Attachments.Add(attachment);
                    return;
                }
                catch (IOException ex) when (IsFileLocked(ex))
                {
                    retryCount++;
                    if (retryCount >= maxRetries)
                    {
                        _utilityService.LogError($"Failed to attach file after {maxRetries} retries: {path}",
                            "Email Attachment Error", new { }, userSOEID);
                        throw;
                    }

                    var jitter = new Random().Next(50);
                    var delay = TimeSpan.FromMilliseconds(Math.Pow(2, retryCount) * baseDelay.TotalMilliseconds + jitter);
                    await Task.Delay(delay);
                }
            }
        }

        private bool IsFileLocked(IOException ex)
        {
            const int ERROR_SHARING_VIOLATION = 0x20;
            const int ERROR_LOCK_VIOLATION = 0x21;

            var errorCode = ex.HResult & 0x0000FFFF;
            return errorCode == ERROR_SHARING_VIOLATION ||
                   errorCode == ERROR_LOCK_VIOLATION ||
                   ex.Message.Contains("being used by another process");
        }

        private string GetLogoImagePath()
        {
            var possiblePaths = new[]
            {
                Path.Combine(AppContext.BaseDirectory, "wwwroot", "images", "OneMainSolutionsHorizontal.jpg"),
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "OneMainSolutionsHorizontal.jpg"), 
                Path.Combine(AppContext.BaseDirectory, "images", "OneMainSolutionsHorizontal.jpg"),
                Path.Combine(Directory.GetCurrentDirectory(), "images", "OneMainSolutionsHorizontal.jpg")
            };

            var path = possiblePaths.FirstOrDefault(File.Exists);
            
            if (path == null)
            {
                _utilityService.LogError($"Logo image not found in any of the expected paths", 
                    "Email Logo Missing", new { Paths = possiblePaths }, "SYSTEM");
                return possiblePaths[0]; // Return first path as fallback
            }

            return path;
        }
    }

    public class EmailMessage
    {
        public string FromEmail { get; set; } = string.Empty;
        public string ToEmail { get; set; } = string.Empty;
        public string CcEmail { get; set; } = string.Empty;
        public string FromName { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Attachments { get; set; } = string.Empty;
        public string UserSOEID { get; set; } = string.Empty;
        public string CommunicationID { get; set; } = string.Empty;
    }
}