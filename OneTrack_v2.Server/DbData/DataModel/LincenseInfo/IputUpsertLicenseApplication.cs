namespace OneTrak_v2.DataModel
{
    public class IputUpsertLicenseApplication
    {
        public int EmployeeLicenseID { get; set; }
        public int LicenseApplicationID { get; set; }
        public DateTime? SentToAgentDate { get; set; }
        public DateTime? RecFromAgentDate { get; set; }
        public DateTime? SentToStateDate { get; set; }
        public DateTime? RecFromStateDate { get; set; }
        public string? ApplicationStatus { get; set; }
        public string? ApplicationType { get; set; }
        public DateTime? RenewalDate { get; set; }
        public string? RenewalMethod { get; set; }
        public string UserSOEID { get; set; }
    }
}
