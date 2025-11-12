namespace OneTrack_v2.Services.Email
{
    public class EmailConfiguration
    {
        public string MailServer { get; set; } = string.Empty;
        public bool IsSendToTest { get; set; }
        public string MailFromAddress { get; set; } = string.Empty;
        public string TestMailFromAddress { get; set; } = string.Empty;
        public string TestMailToAddress { get; set; } = string.Empty;
        public string TestMailCCAddress { get; set; } = string.Empty;
        public string AttachmentLocation { get; set; } = string.Empty;

        public EmailConfiguration(IConfiguration config, string environment)
        {
            MailServer = config.GetValue<string>($"EnvironmentSettings:{environment}:MailSettings:mailServer") ?? string.Empty;
            IsSendToTest = config.GetValue<bool>($"EnvironmentSettings:{environment}:MailSettings:isSendToTest");
            MailFromAddress = config.GetValue<string>($"EnvironmentSettings:{environment}:MailSettings:mailFromAddress") ?? string.Empty;
            TestMailFromAddress = config.GetValue<string>($"EnvironmentSettings:{environment}:MailSettings:testmailFromAddress") ?? string.Empty;
            TestMailToAddress = config.GetValue<string>($"EnvironmentSettings:{environment}:MailSettings:testmailToAddress") ?? string.Empty;
            TestMailCCAddress = config.GetValue<string>($"EnvironmentSettings:{environment}:MailSettings:testmailCCAddress") ?? string.Empty;
            AttachmentLocation = config.GetValue<string>($"EnvironmentSettings:{environment}:Paths:AttachmentLoc") ?? string.Empty;
        }
    }
}