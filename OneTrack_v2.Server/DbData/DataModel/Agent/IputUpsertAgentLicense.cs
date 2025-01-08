namespace OneTrak_v2.DataModel
{
    public class IputUpsertAgentLicense
    {
        public int EmployeeID { get; set; }
        public int EmploymentID { get; set; }
        public int EmployeeLicenseID { get; set; } = 0;
        public int? AscEmployeeLicenseID { get; set; } = 0;
        public int? LicenseID { get; set; } = 0;
        public DateTime? LicenseExpireDate { get; set; } = null;
        public string? LicenseStatus { get; set; } = "";
        public string? LicenseNumber { get; set; } = "";
        public bool? Reinstatement { get; set; } = false;
        public bool? Required { get; set; } = false;
        public bool? NonResident { get; set; } = false;
        public DateTime? LicenseEffectiveDate { get; set; } = null;
        public DateTime? LicenseIssueDate { get; set; } = null;
        public DateTime? LineOfAuthorityIssueDate { get; set; } = null;
        public string? LicenseNote { get; set; } = "";
        public string? AppointmentStatus { get; set; } = "";
        public int? CompanyID { get; set; } = 0;
        public DateTime? CarrierDate { get; set; } = null;
        public DateTime SentToAgentDate { get; set; }
        public DateTime? AppointmentEffectiveDate { get; set; } = null;
        public DateTime? AppointmentExpireDate { get; set; } = null;
        public DateTime? AppointmentTerminationDate { get; set; } = null;
        public string UserSOEID { get; set; }
    }
}
