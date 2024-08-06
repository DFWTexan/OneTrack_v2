namespace OneTrak_v2.Server.Services.Email.Templates
{
    public interface IEmailTemplateService
    {
        public Tuple<string, string, string, string> GetMessageHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetCourtDocHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetIncompleteHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetApplNotRecievedHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetAPPLicCopyDisplayGaKyMtWaWyHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetAPPLicenseCopyHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetEmploymentHistoryHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetExamScheduledHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetExamScheduledNoCertHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetRENLicenseCopyCAHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetRENLicenseCopyHTML(int vEmploymentID);
    }
}
