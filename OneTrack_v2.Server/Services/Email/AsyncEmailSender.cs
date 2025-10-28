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

            // Always embed the logo, regardless of attachments
            await AddLogoImageAsync(mailMessage, message);

            // Then (optionally) add file attachments
            await AddEmailAttachmentsAsync(mailMessage, message);

            using var smtpClient = new SmtpClient(_config.MailServer)
            {
                Port = 25,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true
            };

            try
            {
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

        // Update ProcessEmailBody method to handle more image path patterns
        private string ProcessEmailBody(string body)
        {
            return body.Replace(
                "pictures/OneMainSolutionsHorizontal.jpg",
                "https://omsapps.corp.fin/OneTrakV2/pictures/OneMainSolutionsHorizontal.jpg");
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

        // Update AddLogoImageAsync for better image handling
        private async Task AddLogoImageAsync(MailMessage mail, EmailMessage message)
        {
            var logoPath = GetLogoImagePath();
            if (!File.Exists(logoPath))
            {
                _utilityService.LogError(
                    $"Unable to attach logo image. File not found at path: {logoPath}",
                    "Email Logo Missing", new { LogoPath = logoPath }, message.UserSOEID);
                return;
            }

            try
            {
                // Load bytes so we don’t hold a file lock
                byte[] bytes = await File.ReadAllBytesAsync(logoPath);
                var ms = new MemoryStream(bytes); // DO NOT dispose; MailMessage will own it

                var contentType = new System.Net.Mime.ContentType(GetImageContentType(logoPath));

                // Body already has cid:myImageID because ProcessEmailBody() ran earlier
                var htmlView = AlternateView.CreateAlternateViewFromString(mail.Body, null, "text/html");

                var logoResource = new LinkedResource(ms, contentType)
                {
                    ContentId = "myImageID",
                    TransferEncoding = System.Net.Mime.TransferEncoding.Base64
                };

                htmlView.LinkedResources.Add(logoResource);

                // Replace any previous views with this one
                mail.AlternateViews.Clear();
                mail.AlternateViews.Add(htmlView);
            }
            catch (Exception ex)
            {
                _utilityService.LogError(
                    $"Failed to attach logo image: {ex.Message}",
                    "Email Logo Error",
                    new { LogoPath = logoPath, Exception = ex.ToString() },
                    message.UserSOEID);
            }
        }


        private async Task AttachFileWithRetryAsync(MailMessage mail, string path, string userSOEID, int maxRetries = 3)
        {
            if (!File.Exists(path))
            {
                _utilityService.LogError($"Attachment not found: {path}", "Email Attachment Missing", new { }, userSOEID);
                return;
            }

            int retryCount = 0;
            var baseDelay = TimeSpan.FromMilliseconds(100);

            while (retryCount < maxRetries)
            {
                try
                {
                    byte[] bytes = await File.ReadAllBytesAsync(path);
                    var ms = new MemoryStream(bytes); // DO NOT dispose; MailMessage owns it
                    var attachment = new Attachment(ms, Path.GetFileName(path));
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

        // Update GetLogoImagePath to check more locations
        private string GetLogoImagePath()
        {
            var possiblePaths = new[]
            {
                // Check for the image in pictures folder first (to match the HTML)
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "pictures", "OneMainSolutionsHorizontal.jpg"),
                Path.Combine(Directory.GetCurrentDirectory(), "pictures", "OneMainSolutionsHorizontal.jpg"),
                // Then check the standard wwwroot/images location
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "OneMainSolutionsHorizontal.jpg"),
                Path.Combine(AppContext.BaseDirectory, "wwwroot", "images", "OneMainSolutionsHorizontal.jpg"),
                // Finally check other possible locations
                Path.Combine(Directory.GetCurrentDirectory(), "images", "OneMainSolutionsHorizontal.jpg"),
                Path.Combine(AppContext.BaseDirectory, "images", "OneMainSolutionsHorizontal.jpg")
            };

            var path = possiblePaths.FirstOrDefault(File.Exists);
            
            if (path == null)
            {
                _utilityService.LogError(
                    "Logo image not found in any of the expected paths. Ensure the image exists in wwwroot/pictures or wwwroot/images directory.", 
                    "Email Logo Missing", 
                    new { Paths = possiblePaths }, 
                    "SYSTEM");
                // Return the most likely path as fallback
                return possiblePaths[0];
            }

            return path;
        }

        // Update GetImageContentType to be more specific
        private string GetImageContentType(string imagePath)
        {
            return Path.GetExtension(imagePath).ToLower() switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "image/jpeg" // Default to JPEG for unknown types
            };
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