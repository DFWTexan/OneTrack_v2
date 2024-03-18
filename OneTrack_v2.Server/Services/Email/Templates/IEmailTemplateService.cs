namespace OneTrak_v2.Server.Services.Email.Templates
{
    public interface IEmailTemplateService
    {
        public Tuple<string, string, string, string> GetMessageHTML(int vEmploymentID);
    }
}
