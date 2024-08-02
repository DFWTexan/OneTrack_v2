namespace OneTrak_v2.Server.Services.Email.Templates
{
    public interface IEmailTemplateService
    {
        public Tuple<string, string, string, string> GetMessageHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetCourtDocHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetIncompleteHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetApplNotRecievedHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetLicCopyDisplayGaKyMtWaWyHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetLicenseCopyHTML(int vEmploymentID);
    }
}
