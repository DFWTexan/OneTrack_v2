namespace OneTrack_v2.DataModel
{
    public class OputAgentLicenses
    {
        public int? EmployeeLicenseId { get; set; }
        public string? LicenseState { get; set; }
        public string? LineOfAuthority { get; set; }
        public string? LicenseStatus { get; set; }
        public int? EmploymentID { get; set; }
        public int? LicenseID { get; set; }
        public string? LicenseName { get; set; }
        public string? LicenseNumber { get; set; }
        public string? ResNoneRes { get; set; }
        public DateTime? OriginalIssueDate { get; set; }
        public DateTime? LineOfAuthIssueDate { get; set; }
        public DateTime? LicenseEffectiveDate { get; set; }
        public DateTime? LicenseExpirationDate { get; set; }
        public string? LicenseNote { get; set; }
        public bool? Reinstatement { get; set; }
        public bool? Required { get; set; }
        public bool? NonResident { get; set; }
        public int? AscEmployeeLicenseID { get; set; }
        public string? AscLicenseName { get; set; }
    }
}
